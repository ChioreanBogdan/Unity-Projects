using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect_icon : MonoBehaviour {

    //afiseaza cronometrul pt unanumit efect (daca exista)
    public GameObject countdown_box;
    //afiseaza numarul de countere pe care le are un efect (daca exista)
    public GameObject counter_box;
    //ia valoarea id-ului unic a personajului/inamicului pe care acest icon il descrie
    public int character_id;
    //contine id-ul efect-ului/abilitatii continut de icon
    public int effect_id;
    //textul care descrie efectul icon-ului respectiv
    string effect_description;

    //creaza o clona a pictogramei si returneaza GameObject-ul nou creat
    public GameObject Create_Icon(int char_id)
    {
        //GameObject Effect_Icon_Object = GameObject.Find("Effects Symbol");
        //gameObject.transform.SetAsLastSibling();

        var cloned_icon = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        Effect_icon cloned_icon_script = cloned_icon.GetComponentInChildren<Effect_icon>();
        cloned_icon_script.Set_character_id(char_id);

        cloned_icon.transform.parent = GameObject.Find("Canvas").transform;

        return cloned_icon;
    }

    //seteaza pozitia icon-ului creat functie de poza personajului afectat de abilitate
    //astfel incat icon-ul efectului sa apara langa poza personajului/inamicului afectat
    //gasim pe cine afecteaza dupa id-ul unic din script-ul Battle_stats
    public void Set_new_icon_position(GameObject character, int char_unique_id)
    {
        GameObject[] effect_icons;
        effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");
        float add_to_enemy_icon_pos = 0;

        switch (char_unique_id)
        {
            case 4:
            case 5:
            case 6:
                //gameObject.transform.position = new Vector3(1.982f,3.492f,0);
                //foreach(GameObject effect_icon in effect_icons)
                //    gameObject.transform.position = new Vector3(character.transform.position.x-1.3f- add_to_enemy_icon_pos1, character.transform.position.y-0.8f, 0);
                //    add_to_enemy_icon_pos1 = -0.09f;
                //    break;
                //case 5:
                int i = 0;

                //verific daca exista un icon la pozitia respectiva ca sa il mut mai in stanga/dreapta
                Vector3 existing_icon_position = new Vector3(character.transform.position.x - 1.2f - add_to_enemy_icon_pos, character.transform.position.y - 0.8f, 0);

                foreach (GameObject effect_icon in effect_icons)
                {
                    Debug.Log("effect_icons[" + i + "]=" + effect_icons[i]); i++;
                    if (effect_icon.transform.position == existing_icon_position)
                    {
                        Debug.Log("Position coincides");
                        add_to_enemy_icon_pos = add_to_enemy_icon_pos + 0.9f;
                        existing_icon_position = new Vector3(character.transform.position.x - 1.2f - add_to_enemy_icon_pos, character.transform.position.y - 0.8f, 0);
                    }
                }
                //effect_icons[effect_icons.Length - 1].transform.position = existing_icon_position;
                gameObject.transform.position = existing_icon_position;
                break;
            default:
                gameObject.transform.position = new Vector3(0, 0, 0);
                break;
        }
    }

    //schimba text-ul din "Effects symbol" atasat de obiectul "Effects icon" pictogramei cu string-ul new_text
    public void Change_text(string new_text)
    {
        Text icon_text = gameObject.GetComponentInChildren<Text>();
        icon_text.text = new_text;
    }
    
    //schimba text-ul icon-urilor unui personaj cu id-ul comun unic char_id
    public void Change_text(int char_id, string new_text)
    {
        GameObject[] effect_icons = GameObject.FindGameObjectsWithTag("Effect_Icon");

        Debug.Log("character_id is=" + Get_character_id());
        Debug.Log("char_id is=" + char_id);
        foreach (GameObject effect_icon in effect_icons)
        {

            Effect_icon effect_icon_script = effect_icon.GetComponentInChildren<Effect_icon>();
            Debug.Log("Effect scipt=" + effect_icon_script.Get_character_id());
            if (effect_icon_script.Get_character_id() == char_id)
            {
                Text icon_text = effect_icon.GetComponentInChildren<Text>();
                icon_text.text = new_text;
            }
        }
    }

    //aici am ramas 27.11.2019
    public void Change_description_text(string new_text)
    {
        effect_description = new_text;
    }

    //ascunde counter box-ul setand-ul ca first sibling
    public void Hide_counter_box()
    {
        counter_box.transform.SetAsFirstSibling();
    }

    //ascunde countdown box-ul setand-ul ca first sibling
    public void Hide_countdown_box()
    {
        countdown_box.transform.localScale=new Vector3(0,0,0);
    }

    //schimba valoarea counter-ului cu new_text
    public void Change_counter_text(string new_text)
    {
        Text counter_text = counter_box.GetComponentInChildren<Text>();
        counter_text.text = new_text;
    }

    //cand dau click pe icon vreau sa fie adus in fata decription panel-ul in fata in partea de jos
    //si acolo jos sa se schimbe descrierea cu efectul abilitatii
    void OnMouseDown()
    {
        Debug.Log("Icon clicked");
        GameObject bottom_menu_manager = GameObject.Find("Bottom_Menu_Manager");
        GameObject description_panel = GameObject.Find("Description_Panel");
        Text description_panel_text = description_panel.GetComponentInChildren<Text>();

        description_panel_text.text = effect_description;

        Bottom_Menu_Manager bottom_menu_manager_script = bottom_menu_manager.GetComponentInChildren<Bottom_Menu_Manager>();
        bottom_menu_manager_script.Show_menu(description_panel);
        //deblochez butoanele de la meniul de jos cand se da click pe un icon
        bottom_menu_manager_script.Block_button();
    }

    public int Get_character_id()
    {
        return character_id;
    }

    public int Get_effect_id()
    {
        return effect_id;
    }

    public void Set_character_id(int new_character_id)
    {
        character_id = new_character_id;
    }

    public void Set_effect_id(int new_id)
    {
        effect_id = new_id;
    }
}

                                       
