using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manageriaza ce se intampla cand se schimba o tura
public class Turn_Manager:MonoBehaviour {

    //Numarul turei (initial e 1)
    public int turn_number = 1;

    //creste tura cu 1
    public void Increment_turn()
    {
        turn_number++;
    }

    //reseteaza bool-urile din script-urile battle_stats a tuturor personajelor la false
    public void Reset_used_skills()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        foreach(GameObject character in characters)
        {
            Battle_Stats char_battle_stats = character.GetComponentInChildren<Battle_Stats>();
            char_battle_stats.Set_used_skill(false);
        }
    }

    //reseteaza culorile cast indicatoarelor tuturor personajelor
    public void Reset_cast_indicators()
    {
        //GameObject interface_manager_gameobject=GameObject.Find("Interface_manager");
        //Interface_Manager interface_manager=interface_manager_gameobject.GetComponentInChildren<Interface_Manager>();
        Interface_Manager.Update_cast_indicator_colors();
    }

    public void Close_the_Kanji_Card_Scroll_View()
    {
        GameObject kanji_scroll_button = GameObject.Find("Kanji scroll button");
        Scroll_View_Collapse_Button_Component scroll_button_component = kanji_scroll_button.GetComponentInChildren<Scroll_View_Collapse_Button_Component>();
        scroll_button_component.Close_the_Scroll_View();
    }

    public void Change_turn()
    {
        Increment_turn();
        Reset_used_skills();
        Reset_cast_indicators();
        Close_the_Kanji_Card_Scroll_View();
    }
                                         }
