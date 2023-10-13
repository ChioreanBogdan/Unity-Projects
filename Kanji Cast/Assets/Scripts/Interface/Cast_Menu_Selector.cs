using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aici o sa ma ocup de interfata care devine activa cand in partea de jos e activ panoul de cast
public static class Cast_Menu_Selector {

    static bool cast_panel_is_active;

    public static bool Get_cast_panel_status()
    {
        return cast_panel_is_active;
    }

    //seteaza cast panel-ul sa fie activ,daca e activ se pot selecta personajele
    public static void Set_cast_panel_status(bool set_active)
    {
        cast_panel_is_active = set_active;
    }

    //verific daca meniul de cast e activ si daca e plasat un kanji in slot
    //public static Kanji Return_cast_slot_kanji()
    //{
    //    if (Get_cast_panel_status() == true)
    //    {
    //        GameObject cast_panel = GameObject.Find("Cast_Panel");
    //        GameObject cast_slot = cast_panel.transform.Find("Cast Slot").gameObject;
    //        Kanji_Container cast_container = cast_slot.GetComponentInChildren<Kanji_Container>();
    //        Debug.Log("Kanji to cast=" + cast_container.Get_contained_kanji());
    //        return cast_container.Get_contained_kanji();
    //    }
    //    else return null;
    //}
                                       }
