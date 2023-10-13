using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script atasat de GameObject-ul "Hiragana Panel"
public class Hiragana_Panel_Manager : MonoBehaviour {

    public Text kanji_text;

    //public InputField

    public GameObject Onyomi_Title;
    public GameObject Onyomi_Field;
    public GameObject Onyomi_Cast_Button;

    public GameObject Kunyomi_Title;
    public GameObject Kunyomi_Field;
    public GameObject Kunyomi_Cast_Button;

    private void Awake()
    {
        //GameObject hir_pan = GameObject.Find("Hiragana Panel");
        //Text kanji_text = inc_kanji.GetComponentInChildren<Text>();
        
    }

    // Use this for initialization
    void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //schimb textul in hiragana panel,schimb kanji-ul
    //si ascund componentele pt onyomi si kunyomi(daca e cazul)
    public void Load_kanji_in_panel()
    {
        //Schimbam simbolul in "Hiragana panel"
        if (Cast_Manager.Get_Kanji_to_cast() != null)
        {
            kanji_text.text = Cast_Manager.Get_Kanji_to_cast().symbol;
            Hide_kunyomi_components();
        }
    }

    //Ascunde textbox-ul,text-ul si butonul pt componenta kunyomi a kanji-ului
    public void Hide_kunyomi_components()
    {
     if(Cast_Manager.Get_Kanji_to_cast().kunyomi_readings.Length==0)
        {
            Kunyomi_Title.transform.localScale = new Vector3(0, 0, 0);
            Kunyomi_Field.transform.localScale = new Vector3(0, 0, 0);
            Kunyomi_Cast_Button.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    //reading to check va verifica citirea onyomi (reading_to_check are valoarea "onyomi")
    //citirea kunyomi (reading_to_check are valoarea "kunyomi")
    //citirea radical (reading_to_check are valoarea "radical")
    public void Reading_is_correct(string reading_to_check)
    {
       // Text onyomi_input_filed_text = input_field.GetComponentInChildren<Text>();
       bool there_is_a_correct_match = false;

        GameObject hiragana_converter = GameObject.Find("Hiragana_Converter");
        //try
        //{
        Hiragana_Converter hiragana_conv = hiragana_converter.GetComponentInChildren<Hiragana_Converter>();
        //}
        //catch
        //{

        //}

        if (reading_to_check == "onyomi" && hiragana_conv.Get_contains_only_hiragana())
        {
            GameObject onyomi_field = GameObject.Find("Onyomi field");
            InputField onyomi_input_field = onyomi_field.GetComponentInChildren<InputField>();
            Debug.Log("Onyomi in input field=" + onyomi_input_field.text);

            //if (hiragana_conv.Get_contains_only_hiragana())
            foreach (string s in Cast_Manager.Get_Kanji_to_cast().onyomi_readings)
            {
                GameObject hiragana_panel = GameObject.Find("Hiragana Panel");

                if (s == onyomi_input_field.text && onyomi_input_field.text != "")
                {
                    there_is_a_correct_match = true;
                    Ability_Converter.Generate_abiities_from_kanji(Cast_Manager.Get_Kanji_id_to_cast(),'o');
                    Debug.Log("Armed ability is:" + Ability_Manager.Get_selected_ability_id());
                    Battle_manager.Set_activated_ability(true);
                    Debug.Log("Set activated ability="+Battle_manager.Get_ability_activated());

                    hiragana_panel.transform.position = new Vector3(13,814,0);
                    Ability_Manager.Update_panels_to_flash(Ability_Manager.Get_selected_ability_id());
                    onyomi_input_field.text = "";

                    //setam bool-ul succesful_incantation din cast_manager la true (ca sa nu se foloseasca o abilitate can dam click pe un personaj/inamic)
                    //fara sa fi dat definitia corecta a kanji-ului mai intai
                    Cast_Manager.Set_incantation_status(true);
                }
                else
                {
                    hiragana_panel.transform.position = new Vector3(13, 814, 0);
                    onyomi_input_field.text = "";

                    GameObject bottom_menu_manag=GameObject.Find("Bottom_Menu_Manager");
                    Bottom_Menu_Manager bottom_menu_manager=bottom_menu_manag.GetComponentInChildren<Bottom_Menu_Manager>();
                    bottom_menu_manager.Destroy_kanji_card_in_slot("Cast Slot");

                    Battle_manager.Set_used_ability(Battle_manager.Get_selected_character_id(), true);

                    //fac update la bulinele colorate din dreptul capetelor personajelor
                    Interface_Manager.Update_cast_indicator_colors();

                    //Deselectez personajul care a rostit gresit incantatia
                   GameObject character=GameObject.FindGameObjectWithTag("Character");
                    Character_Select char_select = character.GetComponentInChildren<Character_Select>();
                    char_select.Default_selection();

                    Cast_Manager.Set_incantation_status(false);
                }
            }
        }
        else if (reading_to_check == "kunyomi")
        {
            GameObject kunyomi_field = GameObject.Find("Kunyomi field");
            InputField kunyomi_input_field = kunyomi_field.GetComponentInChildren<InputField>();
            Debug.Log("Kunyomi in input field=" + kunyomi_input_field.text);

            foreach (string s in Cast_Manager.Get_Kanji_to_cast().onyomi_readings)
            {
                if (s == kunyomi_input_field.text && kunyomi_input_field.text!="") there_is_a_correct_match = true;
            }
        }
        else if (reading_to_check == "radical")
        {
            //Aici trebuie sa mai completam cand o sa am input field/meniu pt radicali de kanji
        }
        else if (hiragana_conv.Get_contains_only_hiragana()) there_is_a_correct_match = false;

        //daca nu e corecta invocatia nu putem folosi o abilitate
        if (!there_is_a_correct_match && hiragana_conv.Get_contains_only_hiragana())
        {
            Cast_Manager.Set_Kanji_id_to_cast(0); Cast_Manager.Set_Kanji_to_cast(null);
        }

        Debug.Log("There is a correct match:" + there_is_a_correct_match);
        Ability_Converter.Generate_abiities_from_kanji(1, 'o');
    }
}
