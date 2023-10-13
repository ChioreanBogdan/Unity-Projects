using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aici vreau sa scriu centralizatorul care aplica toate formulele de combinare a kanji-urilor
public static class Fusion_Manager {

    static GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
    static int[] fusionable_kanji_ids = new int[kanji_slots.Length];
    static int i = 0;
    static Fused_Kanji[] fused_kanji;

    //true daca se poate face o fuziune cu kanji card-urile prezente in slot-uri,false daca nu.
    static bool A_fusion_is_available;
    //capata nr de kanji-uri dintr-o reteta,daca se gaseste o reteta cu un nr mai mare de kanji-uri in librarie
    //setam id-urile kanji-urilor respective pt fuziune
    static int Longest_fusion=0;

    //card-ul care se poate obtine din kanji-urile prezente in slot-uri
    static Fused_Kanji current_fusion_card;

    //iau materialu' de fuziune,adica id-urile de kanji din toate slot-urile
    public static void Get_Fusion_Material()
    {
        i = 0;

        //kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
        //fusionable_kanji_ids = new int[kanji_slots.Length];

        foreach (GameObject slot in kanji_slots)
        {
            Radical_Container radical_container = slot.GetComponentInChildren<Radical_Container>();
            Kanji_Card_Detector kanji_card_detector = slot.GetComponentInChildren<Kanji_Card_Detector>();
            if(!kanji_card_detector.Get_is_cast_slot()) fusionable_kanji_ids[i] = radical_container.Get_radical_id();
            i++;
        }
    }

    public static void Return_fusionable_kanji()
    {
        Debug.Log("Hai!Saido Chesto");
        i = 0;
        foreach (int fusionable_kanji_id in fusionable_kanji_ids)
        {
            Debug.Log("fusion[" + i + "]=" + fusionable_kanji_id);
            i++;
        }
    }

    //ASTA MAI TREBUIE MODIFICAT
    public static void Check_fusion_possibilities()
    {
        GameObject Fusion_Library = GameObject.Find("Fused Kanji Library");
        Fused_Kanji_Library library = Fusion_Library.GetComponentInChildren<Fused_Kanji_Library>();

        foreach (Fused_Kanji fk in library.Return_fused_kanji())
        {
            Debug.Log("Kanji recipe for " + fk + "can be fused:" + fk.Fuse(fusionable_kanji_ids));

            //AICI SIGUR TRE SA REVII
            if (fk.Fuse(fusionable_kanji_ids))
            {
                if (fk.Get_number_of_component_radicals() >= Longest_fusion)
                {
                    Longest_fusion = fk.Get_number_of_component_radicals();
                    Mark_kanji_cards_for_fusion(fk.Get_component_kanji_ids());
                    A_fusion_is_available = true;
                    current_fusion_card = fk;
                }

                break;
                //VEZI DACA FUNCTIONEAZA CHESTIA ASTA CAND AI MAI MULTE KANJI-URI IN FUSED_KANJI_LIBRARY
                //TREBUIE GASITA O ALTA SOLUTIE PT KANJI 三 SI 二 DE EXEMPLU,CA SA NU SE OPREASCA LA 二
            }
            else A_fusion_is_available = false;
        }

        if (!A_fusion_is_available)
        {
            Remove_fusion_mark_from_cards(); current_fusion_card = null;
        }
    }

    //S-AR PUTEA SA TREBUIASCA SA REVII LA ASTA
    //verific kanji-urile continute in toate slot-urile,daca id-ul lor e necesar pt reteta 
    //vad pozitia carui kanji coincide cu pozitia slot-ului
    //si marcam bool-ul "Marked_for_fusion" din script-ul Kanji_Image_Manager cu true
    public static void Mark_kanji_cards_for_fusion(int[] fusion_component_ids)
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach (GameObject slot in kanji_slots)
        {
            Radical_Container contained_radical= slot.GetComponentInChildren<Radical_Container>();

            for (int i = 0; i < fusion_component_ids.Length; i++)
            {
                if (contained_radical.Get_radical_id() == fusion_component_ids[i])
                {
                    foreach (GameObject card in kanji_cards)
                    {
                        if (card.transform.position == slot.transform.position)
                        {
                            Kanji_Image_Manager image_manager = card.GetComponentInChildren<Kanji_Image_Manager>();
                            Kanji_Card_Detector card_detector = slot.GetComponentInChildren<Kanji_Card_Detector>();
                            //vrem ca doar slot-urile care nu sunt folosite pt cast-ing sa poata fi folosite in fuziune
                            if(!card_detector.Get_is_cast_slot()) image_manager.Set_fusion_marker(true);
                        }
                    }
                }
            }
        }
    }

    //setez toate bool-urile "Marked_for_fusion" la false daca nu sunt indeplinite conditiile unei retete
    public static void Remove_fusion_mark_from_cards()
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach (GameObject card in kanji_cards)
        {
            Kanji_Image_Manager image_manager = card.GetComponentInChildren<Kanji_Image_Manager>();
            image_manager.Set_fusion_marker(false);
        }
    }

    public static int[] Get_fusionable_kanji_ids()
    {
        return fusionable_kanji_ids;
    }

    public static Fused_Kanji Get_current_fusion_card()
    {
        return current_fusion_card;
    }

    public static void Set_current_fusion_card(Fused_Kanji new_fusion_card)
    {
        current_fusion_card = new_fusion_card;
    }
}
