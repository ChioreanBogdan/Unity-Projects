using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ability_Manager {

    public static int selected_ability_id = 0;

    public static int Get_selected_ability_id()
    {
        return selected_ability_id;
    }

    public static void Set_selected_ability_id(int id_to_set)
    {
        selected_ability_id = id_to_set;
    }

    public static void Update_panels_to_flash(int ability_id)
    {
        GameObject interface_manager_object = GameObject.Find("Interface_manager");
        Interface_Manager interface_manager_script = interface_manager_object.GetComponentInChildren<Interface_Manager>();

        switch (ability_id)
        {
            case 0:
                //Interface_Manager.Set_portrait_called_only_once(true);
                //Interface_Manager.Set_panels_to_flash("No one");

                interface_manager_script.Update_panels_to_flash("No one");
                break;
            case 1:
                //Interface_Manager.Set_portrait_called_only_once(true);
                //Interface_Manager.Set_panels_to_flash("Enemies");
                interface_manager_script.Update_panels_to_flash("Enemies");
                break;
            default:
                Debug.Log("");
                break;
        }
    }

    //aplica efectele abiitatii cu id-ul selected_ability_id
    public static void Apply_Ability_Effects(int ability_id,GameObject clicked_object)
    {
        //Battle_Stats b_stats;

            Battle_Stats b_stats = clicked_object.GetComponentInChildren<Battle_Stats>();

        switch (ability_id)
        {
            case 1:
                //if (reading=='o') Debug.Log("Case 1 Onyomi");
                //else if (reading == 'k') Debug.Log("Case 1 Kunyomi");
                //else if (reading == 'r') Debug.Log("Case 1 Radical");
                b_stats.いち_flammable_charges = Effects.Add_flammable_charges(b_stats.いち_flammable_charges, 1);

                b_stats.Current_Health = Effects.Deal_physical_damage(10, b_stats.Current_Health);
                Interface_Manager.Update_Health_Bar(clicked_object,b_stats.Current_Health);
                Battle_manager.Set_used_ability(Battle_manager.Get_selected_character_id(),true);
                Interface_Manager.Update_cast_indicator_colors();
                break;
            case 2:
                Debug.Log("Case 2");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
                                }
