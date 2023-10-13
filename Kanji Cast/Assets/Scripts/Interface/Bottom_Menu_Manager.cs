using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bottom_Menu_Manager : MonoBehaviour {

    public GameObject[] menu_panels;

    GameObject[] kanji_cards;
    GameObject[] kanji_slots;

    Vector3 initial_position;
    //public GameObject menu_to_show;

    //memoram pozitia in care se afla panoul de jos ca sa stim la ce pozitie revine panoul de meniu selectat cand schimbam meniurile
    private void Awake()
    {
        GameObject initial_menu = GameObject.Find("Start_Panel");
        initial_position = initial_menu.transform.position;
        Show_menu(initial_menu);
    }

    // Use this for initialization
    void Start () {
        
                }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //afiseaza meniul show menu si le asunde pe celelalte setand scara la 0
    public void Show_menu(GameObject menu_to_show)
    {
       // menu_to_show.transform.localScale= new Vector3(1, 1, 1);
        Change_parents(menu_to_show);

        menu_to_show.transform.position = initial_position;

        foreach (GameObject menu_panel in menu_panels)
        {
            //if(menu_panel.GetComponentInChildren) 
            if (menu_panel != menu_to_show)
            {
                Change_parents(menu_panel);

                //menu_panel.transform.localScale = new Vector3(0.00025f, 0.00025f, 0.0025f);
                menu_panel.transform.position =new Vector3(menu_panel.transform.position.x, menu_panel.transform.position.y+1500, menu_panel.transform.position.z);
            }
        }
    }

    public void Change_parents(GameObject parent_menu)
    {
        kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");
        kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach (GameObject kanji_slot in kanji_slots)
        {
            foreach (GameObject kanji_card in kanji_cards)
            {
                if (kanji_card.transform.position == kanji_slot.transform.position && kanji_slot.transform.parent.name == parent_menu.transform.name)
                {
                    kanji_card.transform.parent = parent_menu.transform;
                    //kanji_card.transform.position = kanji_slot.transform.position;
                }
            }
        }
    }

    //Blocheaza butonul a carui meniu e selectat/afisat si le deblocheaza pe celelalte
    public void Block_button()
    {
        //gasesc gameobject-urile care contin butoanele din meniul de jos,le pun componentele intr-o lista apoi blochez butonul selectat si le deblochez pe celelalte
        GameObject[] Bottom_menu_button_objects = GameObject.FindGameObjectsWithTag("Bottom_menu_button");
        List<Button> Bottom_menu_buttons = new List<Button>();
        //int i = 0;
        foreach (GameObject Bottom_menu_object in Bottom_menu_button_objects)
        {
            Bottom_menu_buttons.Add(Bottom_menu_object.GetComponentInChildren<Button>());
            //Debug.Log("Bottom menu object[" + i + "]=" + Bottom_menu_object);
            //i++;
        }

        //Debug.Log("Selected event=" + EventSystem.current.currentSelectedGameObject.name);

        //i = 0;
        foreach (Button Bottom_menu_button in Bottom_menu_buttons)
        {
            Bottom_menu_button.interactable = true;
            //Debug.Log("Bottom_menu_button[" + i + "]=" + Bottom_menu_button);
            //i++;
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == Bottom_menu_button.name) Bottom_menu_button.interactable = false;
            if (EventSystem.current.currentSelectedGameObject != null && Bottom_menu_button.name=="Cast_Button") Debug.Log("Nigga");

            //Aici ai ramas la butoane
            //if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Cast_Button")
            //{
            //    Cast_Menu_Selector.Set_cast_panel_is_active(true);
            //    Debug.Log("Cast button is selected=" + Cast_Menu_Selector.Get_cast_panel_is_active());
            //    Debug.Log("Current event=" + EventSystem.current.currentSelectedGameObject);
            //}
            //else
            //{
            //    Cast_Menu_Selector.Set_cast_panel_is_active(false);
            //    Debug.Log("Cast button is selected=" + Cast_Menu_Selector.Get_cast_panel_is_active());
            //    Debug.Log("Current event=" + EventSystem.current.currentSelectedGameObject);
            //}
        }
    }

    public void Check_cast_button()
    {
        Debug.Log("Game object name=" + gameObject);
    }

    //distruge kanji card-ul din slot-ul kanji_slot de cast in cazul unei incantatii nereusite sau daca s-a folosit o abilitate
    public void Destroy_kanji_card_in_slot(string kanji_slot_name)
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");
        GameObject deletion_slot = GameObject.Find(kanji_slot_name);

        foreach (GameObject kanji_card in kanji_cards)
        {
            if (kanji_card.transform.position == deletion_slot.transform.position) Destroy(kanji_card);

        }

    }
}
