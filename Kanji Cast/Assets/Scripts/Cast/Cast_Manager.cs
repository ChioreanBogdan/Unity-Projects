using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Cast_Manager         {

    //id-ul celui care va incerca sa dea cast la kanji-ul din panoul de "Cast"
    static int Caster_id;
    //id-ul kanji-ului care va fi cast-uit
    static int Kanji_id_to_cast;
    //Kanji-ul care va fi cast-uit
    static Kanji Kanji_to_cast;
    //variabila bool care verifica daca a fost realizata cu succes o incantatie
    static bool succesful_incantation;

    //schimba id-ul Kanji_id_to_cast,de ex in cazul in care mutam o carte kanji din slot
    public static void Update_Kanji_id_to_cast()
    {
        GameObject cast_panel = GameObject.Find("Cast_Panel");
        GameObject cast_slot = cast_panel.transform.Find("Cast Slot").gameObject;
        Kanji_Container cast_container = cast_slot.GetComponentInChildren<Kanji_Container>();
        if (cast_container.Get_contained_kanji() != null)
        {
            Set_Kanji_id_to_cast(cast_container.Get_contained_kanji().id_number);
            Set_Kanji_to_cast(cast_container.Get_contained_kanji());
        }
        else if (cast_container.Get_contained_kanji() == null) Set_Kanji_id_to_cast(0);
    }

    public static bool Get_incantation_status()
    {
        return succesful_incantation;
    }

    public static Kanji Get_Kanji_to_cast()
    {
        return Kanji_to_cast;
    }

    public static int Get_Kanji_id_to_cast()
    {
        return Kanji_id_to_cast;
    }

    public static void Set_incantation_status(bool new_status)
    {
        succesful_incantation = new_status;
    }

    public static void Set_Kanji_to_cast(Kanji new_kanji)
    {
        Kanji_to_cast = new_kanji;
        Debug.Log("Kanji to cast is:" + Kanji_to_cast);
    }

    public static void Set_Kanji_id_to_cast(int new_id)
    {
        Kanji_id_to_cast = new_id;
        Debug.Log("Id of kanji to cast is:" + Kanji_id_to_cast);
    }

                                        }
