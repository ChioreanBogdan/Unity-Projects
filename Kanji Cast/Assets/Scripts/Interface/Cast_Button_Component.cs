using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//butoane care o sa contina functiile pt butoanele "Cast" din stanga si "Start incantation"
public class Cast_Button_Component : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //intram in cast mode daca e selectat butonul meniul de cast.sau daca e apasat alt buton din meniu
    //tog_act true-meniul devine activ,tog_act false meniul e inactiv
    public void Toggle_Cast_Mode(bool tog_act)
    {
        Cast_Menu_Selector.Set_cast_panel_status(tog_act);
        //daca schimbam meniul de jos deselectam toate personajele
        if (!tog_act)
        {
            Character_Select deselect = new Character_Select();
            deselect.Deselect_all_characters();
        }
    }

    //aducem panoul hiragana in centrul ecranului si in fata
    public void Bring_hiragana_panel_to_screen()
    {
        if (Cast_Manager.Get_Kanji_id_to_cast() != 0)
        {
            GameObject hiragana_panel = GameObject.Find("Hiragana Panel");
            GameObject main_screen = GameObject.Find("Main_screen");

            hiragana_panel.transform.position = main_screen.transform.position;
            hiragana_panel.transform.SetAsLastSibling();
        }
    }

    //public void Get_Kanji_to_Cast()
    //{
    //    if (Cast_Menu_Selector.Return_cast_slot_kanji() != null) Cast_Manager.Set_Kanji_id_to_cast(Cast_Menu_Selector.Return_cast_slot_kanji().id_number);
    //    Debug.Log("Kanji id to cast is:" +Cast_Manager.Get_Kanji_id_to_cast());
    //}
}


