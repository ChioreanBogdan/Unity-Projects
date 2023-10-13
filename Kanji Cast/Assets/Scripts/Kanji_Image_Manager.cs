using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Script pe care il atasez de o imagine cu un kanji pt action on click,drag and drop,etc
public class Kanji_Image_Manager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //verificam daca e pointer-ul in imagine
    bool pointer_inside;

    //marchez pt fuziune un card kanji daca are id-ul potrivit
    public bool marked_for_fusion;

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
        Move_Image();
        Activate_slot_collisions();
	               }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Schimbam id-ul kanji-ului selectat in Battle_manager
        //Debug.Log("Id_kanji=" + Battle_manager.Get_selected_kanji_id());

        //Schimb parintele obiectului de care e atasat script-ul
        //noul obiect va avea ca parinte "Main_screen" (daca inainte avea cumva "Kanji card scroll view" ca sa nu se
        //miste toate kanji card-urile
        GameObject main_object = GameObject.Find("Main_screen");
        Change_object_parent(main_object, gameObject);

        Set_radical_from_container();
        Set_radical_id_from_containter();
        Set_radical_description_from_containter();

        //Debug.Log("Id_kanji=" +Battle_manager.Get_selected_kanji_id());
        //Debug.Log("Descriere_kanji=" + Battle_manager.Get_selected_kanji_description());
        //Debug.Log("Kanji-ul selectat=" + Battle_manager.Selected_Kanji);
    }

    //void OnMouseUp()
    //{
    //    Turn_on_all_slot_collisions();
    //    Debug.Log("Mouse up");
    //}

    //detectez daca acest kanji card se loveste de mai multe slot-uri
    //void OnTriggerEnter2D(Collider2D card)
    //{
    //    GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
    //    int i = 0;

    //    foreach(GameObject kanji_slot in kanji_slots)
    //    {
    //        Kanji_Card_Detector card_detector = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
    //        //if (card_detector.unique_id)
    //    }
    //}

    //on pointer enter vreau sa apara o descriere
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointer_inside = true;
        //Debug.Log("pointer inside ="+pointer_inside);
        GameObject main_object = GameObject.Find("Main_screen");
        Change_object_parent(main_object, gameObject);
    }

    //on pointer exit vreau sa dispara descrierea
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!Input.GetMouseButton(0)) pointer_inside = false;
        //Debug.Log("pointer inside =" + pointer_inside);
    }

    //seteaza kanji-ul din Kanji_container-ul imaginii selectate
    public void Set_radical_from_container()
    {
        Radical_Container contained_radical = this.GetComponentInChildren<Radical_Container>();
        if (contained_radical.Get_contained_radical() != null) Battle_manager.Set_selected_Radical(contained_radical.Get_contained_radical());
    }

    //ia id-ul obiectului kanji din script-ul "Kanji_Container" (daca are un obiect Kanji atasat) si il setam ca id selectat in Battle_manager
    public void Set_radical_id_from_containter()
    {
        Radical_Container contained_radical = this.GetComponentInChildren<Radical_Container>();
        if (contained_radical.Get_radical_id() != 0) Battle_manager.Set_selected_id(contained_radical.Get_radical_id());
    }

    public void Set_kanji_symbol_from_container()
    {

    }

    //ia descrierea obiectului kanji din script-ul "Radical_Container" (daca are un obiect Radical atasat) si o setam ca descriere selectata in Battle_manager
    public void Set_radical_description_from_containter()
    {
        //setam descrierea kanji-ului selectat in Battle_manager
        Radical_Container contained_radical = this.GetComponentInChildren<Radical_Container>();
        if (contained_radical.Get_radical_id() != 0) Battle_manager.Set_selected_description(contained_radical.Get_radical_description());

        //setam descrierea kanji-ului selectat in cutiuta de descriere
        GameObject description_box = GameObject.FindGameObjectWithTag("Kanji_description_box");
        description_box.GetComponentInChildren<Text>().text = Battle_manager.Get_selected_radical().description;

        if (Battle_manager.Get_selected_kanji() != null)
        {
            //schimbam culoarea tuturor citirilor kun'yomi in albastru si a celor on'yomi in rosu
            foreach (string s in Battle_manager.Get_selected_kanji().onyomi_readings)
            {
                description_box.GetComponentInChildren<Kanji_Description_Manager>().Change_text_color(s, "red");
            }

            foreach (string s in Battle_manager.Get_selected_kanji().kunyomi_readings)
            {
                description_box.GetComponentInChildren<Kanji_Description_Manager>().Change_text_color(s, "blue");
            }
        }
    }

    //daca e tinut apasat click stanga si suntem deasupra imaginii mutam imaginea
    public void Move_Image()
    {
        if (Input.GetMouseButton(0) && pointer_inside)
        {
            //Debug.Log("Pressed left click.");
            this.transform.position = Input.mousePosition;

            //pt ca obiectul tarat sa apara in fata tuturor celorlalte obiecte
            this.transform.SetAsLastSibling();

            Turn_off_all_slot_collisions();

            //refresh-uiesc parintele gamObject-ului de care e atasat script-ul
            Refresh_kanji_parent(gameObject);
        }
    }

    public void Activate_slot_collisions()
    {
        if (Input.GetMouseButtonUp(0) && pointer_inside)
        {
            Turn_on_all_slot_collisions();

            Refresh_kanji_in_slots();

            //Fused_Kanji tsu = new Fused_Kanji();
            //Fusion_Manager.Get_Fusion_Material();
            //Fusion_Manager.Return_fusionable_kanji();
            //Debug.Log("Tsu is:"+tsu.Fuse(Fusion_Manager.Get_fusionable_kanji_ids()));
            //Change_kanji_containers_in_slots();
        }
    }

    //dezactivam boxcollider2d-urile tuturor slot-urilor cat timp e tinut apasat click si se misca cutia
    public void Turn_off_all_slot_collisions()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach (GameObject kanji_slot in kanji_slots)
        {
            Kanji_Card_Detector kcd = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
            kcd.Turn_off_box_collider();
        }

        //Refresh_kanji_in_slots();
    }

    //activam boxcollider2d-urile tuturor slot-urilor cand nu mai e click pe mouse
    public void Turn_on_all_slot_collisions()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach (GameObject kanji_slot in kanji_slots)
        {
            Kanji_Card_Detector kcd = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
            kcd.Turn_on_box_collider();
        }

        Refresh_kanji_in_slots();
    }

    //Updatez care slot-uri sunt ocupate si ce kanji-uri contin de fiecare data cand e mutat un kanji
    public void Refresh_kanji_in_slots()
    {
        GameObject random_slot = GameObject.FindGameObjectWithTag("Kanji_Slot");
        Kanji_Card_Detector card_detector = random_slot.GetComponentInChildren<Kanji_Card_Detector>();
        card_detector.Clear_kanji_slots();
        card_detector.Change_kanji_containers_in_slots();
        Cast_Manager.Update_Kanji_id_to_cast();
    }

    //schimbam parintii kanji_card-urilor selectat daca acesta a fost mutat
    public void Refresh_kanji_parent(GameObject moved_kanji)
    {
        GameObject[] kanji_slots =GameObject.FindGameObjectsWithTag("Kanji_Slot");
        bool has_been_removed_from_slot=true;
        GameObject main_screen = GameObject.Find("Main_screen");

        foreach(GameObject kanji_slot in kanji_slots)
        {
            if (moved_kanji.transform.position == kanji_slot.transform.position) has_been_removed_from_slot = false;
        }

        if (has_been_removed_from_slot) moved_kanji.transform.parent = main_screen.transform;
    }

    //marchez daca un kanji card de care e atasat script-ul e marcat pt fuziune 
    public void Set_fusion_marker(bool new_marker)
    {
        this.marked_for_fusion = new_marker;
    }

    //schimbam parintele GameObject-ului child_object
    public void Change_object_parent(GameObject new_panet_object,GameObject child_object)
    {
        child_object.transform.SetParent(new_panet_object.transform);
    }
}
