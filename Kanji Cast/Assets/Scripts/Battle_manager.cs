using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//memoreaza kanji-ul selectat si ii aplica efectele
public static class Battle_manager {

    //la inceput nu avem niciun personaj selectat
    public static int selected_character_id=0;

    //la inceput nu avem niciun id selectat
    public static int selected_kanji_id=0;
    public static string selected_kanji_description = ""; public static string selected_kanji_symbol="";

    public static Radical Selected_Radical;
    public static Kanji Selected_Kanji;

    //bool care se activeaza daca o incantantie de kanji a fost realizata cu succes
    public static bool ability_activated;

    //returneaza variabila selected_character_id
    public static int Get_selected_character_id()
    {
        return selected_character_id;
    }

    //returneaza variabila Selected_Radical
    public static Radical Get_selected_radical()
    {
        return Selected_Radical;
    }

    //returneaza variabila Selected_Kanji
    public static Kanji Get_selected_kanji()
    {
        return Selected_Kanji;
    }

    //returneaza variabila selected_kanji_id
    public static int Get_selected_kanji_id()
    {
        return selected_kanji_id;
    }

    //returneaza simbolul kanji-ului selectat
    public static string Get_selected_kanji_symbol()
    {
        return Selected_Kanji.symbol;
    }

    //returneaza variabila selected_kanji_description
    public static string Get_selected_kanji_description()
    {
        return selected_kanji_description;
    }

    public static bool Get_ability_activated()
    {
        return ability_activated;
    }

    //sets id passed in this function as selected character id
    public static void Set_selected_character_id(int char_id)
    {
        selected_character_id = char_id;
    }

    //sets radical passed in this function as Selected_Radical
    public static void Set_selected_Radical(Radical radical)
    {
        Selected_Radical = radical;
    }

    //sets kanji passed in this function as Selected_Kanji
    public static void Set_selected_Kanji(Kanji kanji)
    {
        Selected_Kanji = kanji;
    }

    public static void Set_selected_kanji_symbol(string symbol)
    {

    }

    //seteaza id-ul kanji-ului ca id-ul selectat
    public static void Set_selected_id(int id)
    {
        selected_kanji_id = id;
    }

    //seteaza text ca descrierea kanji-ului selectat
    public static void Set_selected_description(string text)
    {
        selected_kanji_description = text;
    }

    //seteaza daca o abilitate este activata
    public static void Set_activated_ability(bool act)
    {
        ability_activated = act;
    }

    //seteaza parametrul used_skill a personajului cu id-ul char_id cu valoarea set_used_ab
    public static void Set_used_ability(int char_id,bool set_used_ab)
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject character in characters)
        {
            Battle_Stats bs = character.GetComponentInChildren<Battle_Stats>();
            if (bs.Get_Unique_id() == char_id) bs.Set_used_skill(set_used_ab);
        }
    }

    //verifica daca personajul cu id-ul char_id a folosit sau nu o abilitate si returneaza true daca a folosit si false daca nu
    public static bool Check_used_ability(int char_id)
    {
        bool check_result = false;

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject character in characters)
        {
            Battle_Stats bs = character.GetComponentInChildren<Battle_Stats>();
            if (bs.Get_Unique_id() == char_id) check_result=bs.Get_used_skill();
        }
        return check_result;
    }

    //verifica daca kanji_id_to_cast din cast_manager e diferit de 0
    public static bool Check_if_there_is_a_kanji_ready_to_cast()
    {
        if (Cast_Manager.Get_Kanji_id_to_cast() != 0 && Cast_Manager.Get_Kanji_to_cast() != null) return true;
        else return false;
    }
}
