using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Se ataseaza de fiecare portret si aplica abilitati functie de ce abilitate se afla in Ability_Manager
public class Ability_Applier : MonoBehaviour//, IPointerClickHandler, IPointerEnterHandler
{
    //public GameObject characters;
    public Battle_Stats b_stats;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
        //if (Input.GetMouseButtonDown(1)) Debug.Log("Im killing loneliness");

    }

    void OnMouseDown()
    {
        Debug.Log("Character has used an ability:"+Battle_manager.Check_used_ability(Battle_manager.Get_selected_character_id()));
        Debug.Log("Selected character="+ Battle_manager.Get_selected_character_id());

        if (!Battle_manager.Check_used_ability(Battle_manager.Get_selected_character_id()) && Battle_manager.Check_if_there_is_a_kanji_ready_to_cast())
        {
            Debug.Log("That tickles");
            Ability_Manager.Apply_Ability_Effects(Ability_Manager.Get_selected_ability_id(), gameObject);
            Battle_manager.Set_used_ability(Battle_manager.Get_selected_character_id(), true);

            //creez un icon in dreptul portetului personajului/inamicului asupra caruia s-a folosit abilitatea
            Icon_Manager.Update_icons(Ability_Manager.Get_selected_ability_id(), gameObject);

            //if(Battle_manager)
            Ability_Manager.Update_panels_to_flash(0);

            //Deselectez personajul care a rostit gresit (sau nu) incantatia
            GameObject character = GameObject.FindGameObjectWithTag("Character");
            Character_Select char_select = character.GetComponentInChildren<Character_Select>();
            char_select.Default_selection();

            //distrug card-ul din slot-ul de cast daca acesta a fost folosit cu succes
            GameObject bottom_menu_manag = GameObject.Find("Bottom_Menu_Manager");
            Bottom_Menu_Manager bottom_menu_manager = bottom_menu_manag.GetComponentInChildren<Bottom_Menu_Manager>();
            bottom_menu_manager.Destroy_kanji_card_in_slot("Cast Slot");

            //setez kanji container-ul si kanji_card_container la null,respectiv 0 si dau update
            Cast_Manager.Set_Kanji_id_to_cast(0); Cast_Manager.Set_Kanji_to_cast(null);

            //setam incantation_is_succesful la false pt a nu fi posibilia folosirea unei abilitati de mai multe ori cu acelasi efect
            Cast_Manager.Set_incantation_status(false);
        }
    }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    //Ability_Manager.Apply_Ability_Effects(Ability_Manager.Get_selected_ability_id(), gameObject);
        //    Debug.Log("Im killing");
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    Debug.Log("Im killing loneliness");
        //}
    }
