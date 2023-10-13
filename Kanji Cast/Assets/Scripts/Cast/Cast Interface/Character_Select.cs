using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select : MonoBehaviour {

    //selection fail si deselected o sa clipeasca alternativ daca personajul selectat a folosit o abilitate
    public Material character_selected,character_deselected,character_selection_fail;

	// Use this for initialization
	void Start () {
        //Default_selection();
	              }
	
	// Update is called once per frame
	void Update () {
        //Character_clicked();
                   }

    //public void Character_selected()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("Click Click");
    //    }
    //}

    //cand se da click pe portetul unui personaj vrem ca el sa fie selectat pt a cast-ui
    void OnMouseDown()
    {
        if (Cast_Menu_Selector.Get_cast_panel_status()==true) {
            if (Get_character_used_skill(gameObject) == false && Get_character_current_health(gameObject)>0)
            {
                Change_sprite_material(gameObject, character_selected);
                GameObject selected_charater = gameObject;
                Battle_Stats selected_character_battle_stats = selected_charater.GetComponentInChildren<Battle_Stats>();
                Battle_manager.Set_selected_character_id(selected_character_battle_stats.Get_Unique_id());
                Debug.Log("Selected character id is now=" +Battle_manager.Get_selected_character_id());
            }
            else if (Get_character_used_skill(gameObject) == true)
            {
                //Aici trebuie revenit
                //Sprite_material_blink(gameObject, character_deselected,character_selection_fail);
            }
                Deselect_unselected_characters();
                                                               }
    }

    //deselecteaza personajele in afara de cel atasat de acest script
    void Deselect_unselected_characters()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject character in characters)
        {
            if (character != gameObject) Change_sprite_material(character,character_deselected);
        }
    }

    //deselecteaza personajele in afara de cel atasat de selected_gameobject
    void Deselect_unselected_characters(GameObject selected_gameobject)
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject character in characters)
        {
            if (character != selected_gameobject) Change_sprite_material(character, character_deselected);
        }

        Change_sprite_material(selected_gameobject, character_selected);
    }

    //Deselecteaza toate personajele (le schimba materialele la starea initiala)
    public void Deselect_all_characters()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject character in characters)
        {
          Character_Select char_sel = character.GetComponentInChildren<Character_Select>();
            Debug.Log("Char_sel_desel=" + char_sel.character_deselected);
          Change_sprite_material(character, char_sel.character_deselected);
        }
    }

    //Ia script-ul Battle_stats a unui personaj si vede daca a folosit o abilitate sau nu
    bool Get_character_used_skill(GameObject selected_character)
    {
        Battle_Stats current_stats = selected_character.GetComponentInChildren<Battle_Stats>();
        if (current_stats.used_skill == true) return true;
        else return false;
    }

    //Ia script-ul Battle_stats a unui personaj si vede daca viata curenta e <=0
    int Get_character_current_health(GameObject selected_character)
    {
        Battle_Stats current_stats = selected_character.GetComponentInChildren<Battle_Stats>();
        return current_stats.Get_current_health();
    }

    //Schimba materialul din sprite renderer-ul unui GameObject
    void Change_sprite_material(GameObject character, Material character_sprite_material)
    {
        SpriteRenderer character_outline = character.GetComponentInChildren<SpriteRenderer>();
        character_outline.material = character_sprite_material;
    }


    void Sprite_material_blink(GameObject blinking_object,Material blink1,Material blink2)
    {
        SpriteRenderer character_outline = blinking_object.GetComponentInChildren<SpriteRenderer>();

        int i = 0;
        for (i = 0; i <= 500;)
        {
            if (i % 2 == 0) StartCoroutine(Timed_Sprite_material_change(0.5f, blinking_object,blink1));
            else if (i%2!=0) StartCoroutine(Timed_Sprite_material_change(0.5f, blinking_object,blink2));
            Debug.Log("i=" + i);
            i++;
        }
        //if(i>=5) character_outline.material = character_deselected;
    }

    //FUNCTIE INCOMPLETA TREBUIE REVENIT
    IEnumerator Timed_Sprite_material_change(float sec,GameObject object_to_change,Material new_material)
    {
        Change_sprite_material(object_to_change,new_material);
        yield return new WaitForSeconds(sec);
    }

    //Selecteaza primul personaj care nu a folosit o abilitate
    public void Default_selection()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        List<Battle_Stats> current_battle_stats = new List<Battle_Stats>();

        foreach (GameObject character in characters)
        {
            //current_battle_stats.Add(character.GetComponentInChildren<Battle_Stats>());
            Battle_Stats character_current_battle_stats = character.GetComponentInChildren<Battle_Stats>();
            if (character_current_battle_stats.Get_used_skill() == false)
            {
                Deselect_unselected_characters(character);
                Battle_manager.Set_selected_character_id(character_current_battle_stats.Get_Unique_id());
                break;
            }
            else
            {
                Deselect_all_characters();
            }
        }
    }
}
