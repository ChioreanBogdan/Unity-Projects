using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Icon_Manager
{

    public static void Update_icons(int ability_id, GameObject affected_character)
    {
        //Gasesc GameObject-ul Effects Icon si il mut in dreptul personajului/inamicului afectat
        GameObject Effect_Icon_Object = GameObject.Find("Effects Icon");
        Effect_icon effect_icon = Effect_Icon_Object.GetComponentInChildren<Effect_icon>();
        Battle_Stats b_stats = affected_character.GetComponentInChildren<Battle_Stats>();

        switch (ability_id)
        {
            case 1:

                //Verific daca exista un icon pt efectul respectiv la personajul respectiv (ca sa nu creez mai multe icon-uri aiurea)
                if (!Check_icon_existence(b_stats.Get_Unique_id()) && Battle_manager.Check_if_there_is_a_kanji_ready_to_cast()==true)
                {
                   GameObject new_effect_icon=effect_icon.Create_Icon(b_stats.Get_Unique_id());
                    Effect_icon new_effect_icon_script = new_effect_icon.GetComponentInChildren<Effect_icon>();
                    new_effect_icon_script.Set_new_icon_position(affected_character, b_stats.Get_Unique_id());
                    Change_effect_id(b_stats.Get_Unique_id(), 1);
                    new_effect_icon_script.Change_counter_text(b_stats.いち_flammable_charges.ToString());
                    //Battle_manager.Set_selected_Kanji();
                    new_effect_icon_script.Change_text(Cast_Manager.Get_Kanji_to_cast().symbol);

                    //ascund countdownbox-ul deoarece nu avem nicun efect de countdown pt id ab 1
                    new_effect_icon_script.Hide_countdown_box();

                    //schimb descrierea efectului
                    new_effect_icon_script.Change_description_text("Target will recieve "+b_stats.いち_flammable_charges*10+" more fire affliction damage when a fire ability is used");
                
                    Debug.Log("effect icon=" + new_effect_icon);
                }
                else
                {
                    Change_icon_text(b_stats.Get_Unique_id(),1, b_stats.いち_flammable_charges.ToString());
                    //ascund countdownbox-ul deoarece nu avem nicun efect de countdown pt id ab 1
                    effect_icon.Hide_countdown_box();
                    //facem update la
                    Change_icon_description(b_stats.Get_Unique_id(), 1,"Target will recieve " + b_stats.いち_flammable_charges*10 + " more fire affliction damage when a fire ability is used");
                }
                //        Interface_Manager.Set_panels_to_flash("No one");
                break;
                //    case 1:
                //        Interface_Manager.Set_panels_to_flash("Enemies");
                //        break;
                //    default:
                //        Debug.Log("");
                //        break;
                //}
        }

    }

    //seteaza parametrul effect_id din script-ul effect_icon la valoarea new_effect_id
    public static void Change_effect_id(int char_id,int new_effect_id)
    {
        GameObject[] effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");

        foreach (GameObject effect_icon in effect_icons)
        {

            Effect_icon effect_icon_script = effect_icon.GetComponentInChildren<Effect_icon>();

            if (effect_icon_script.Get_character_id() == char_id) effect_icon_script.Set_effect_id(new_effect_id);
        }
    }

    //schimba textul tuturor icon-urilor care au character_id-ul identic cu unique id-ul unui script battle_stats
    //atasat de un personaj/inamic
    public static void Change_icon_text(int char_id,int effect_id, string new_text)
    {
        GameObject[] effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");

        foreach (GameObject effect_icon in effect_icons)
        {
            Effect_icon effect_icon_script = effect_icon.GetComponentInChildren<Effect_icon>();

            if (effect_icon_script.Get_character_id() == char_id && effect_icon_script.Get_effect_id() == effect_id)
                effect_icon_script.Change_counter_text(new_text);
        }
    }

    //schimba/updateaza descrierea tuturor icon-urilor care au character_id-ul identic cu unique id-ul unui script battle_stats
    //atasat de un personaj/inamic
    public static void Change_icon_description(int char_id, int effect_id, string new_text)
    {
        GameObject[] effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");

        foreach (GameObject effect_icon in effect_icons)
        {
            Effect_icon effect_icon_script = effect_icon.GetComponentInChildren<Effect_icon>();

            if (effect_icon_script.Get_character_id() == char_id && effect_icon_script.Get_effect_id() == effect_id)
                effect_icon_script.Change_description_text(new_text);
        }
    }

    //verifica daca exista un icon in dreptul unui personaj
    public static bool Check_icon_existence(int char_id)
    {
        Debug.Log("Existence comparison:" +char_id);
        GameObject[] effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");
        bool icon_exists=false;
        int i = 0;

        foreach (GameObject effect_icon in effect_icons)
        {
            Effect_icon effect_icon_script = effect_icon.GetComponentInChildren<Effect_icon>();
            if (effect_icon_script.Get_character_id() == char_id) icon_exists = true;
            Debug.Log("Loop[" +i+"]="+ effect_icon_script.Get_character_id());
            i++;
        }
        return icon_exists;
    }
}
