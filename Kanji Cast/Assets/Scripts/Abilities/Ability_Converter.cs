using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//converteste kanji-ul selectat intr-o abilitate care poate fi apoi folosita asupra inamicilor/aliatilor
public static class Ability_Converter {

   static int kanji_id; 

    public static void Generate_abiities_from_kanji(int kanji_id,char reading)
    {
        switch (kanji_id)
        {
            case 1:
                //if (reading=='o') Debug.Log("Case 1 Onyomi");
                //else if (reading == 'k') Debug.Log("Case 1 Kunyomi");
                //else if (reading == 'r') Debug.Log("Case 1 Radical");
                if (reading == 'o')
                {
                    Ability_Manager.Set_selected_ability_id(1);
                }
                break;
            case 2:
                Debug.Log("Case 2");
                break;
            default:
                Debug.Log("D");
                break;
        }
    }
                                      }
