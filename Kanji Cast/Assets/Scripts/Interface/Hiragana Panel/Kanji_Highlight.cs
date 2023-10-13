using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.UI;

//Script-ul asta il atasez de "Incantation Kanji" din "Hiragana Panel"
//Dintr-un script de aici aduc GameObject-ul "Kanji Highlight Panel" ca first sibling cand intra cursorul in zona incantation kanji-ului
public class Kanji_Highlight : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    GameObject highlight_panel;
    bool mouse_inside; //detecteaza daca mouse-ul e in collisionbox
    Vector3 description_box_initial_position; //memorez pozitia initiala a description box-ului ca sa stiu unde sa il trimit cand nu se da click pe kanji
    // Use this for initialization
    void Start()
    {
        highlight_panel=GameObject.Find("Kanji Highlight Panel");
        highlight_panel.gameObject.transform.localScale=new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouse_inside)
        {
            //Show_description();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight_panel.gameObject.transform.localScale = new Vector3(1, 1, 1);
        mouse_inside = true;
        Debug.Log("mouse inside=" + mouse_inside);
        Set_kanji_to_cast_description();
        Show_description();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight_panel.gameObject.transform.localScale = new Vector3(0, 0, 0);
        mouse_inside = false;
        Debug.Log("mouse inside=" + mouse_inside);
        Hide_description();
    }

    public void Show_description()
    {
        GameObject description_box = GameObject.Find("Kanji_description");
        GameObject main_screen = GameObject.Find("Main_screen");
        description_box_initial_position = description_box.transform.position;
        Debug.Log("description box=" +description_box); Debug.Log("main screen=" + main_screen);
        description_box.transform.position = main_screen.transform.position;
        description_box.transform.SetAsLastSibling();
    }

    public void Hide_description()
    {
        GameObject description_box = GameObject.Find("Kanji_description");
        description_box.transform.position= description_box_initial_position;
        description_box.transform.SetAsFirstSibling();
    }

    //inlocuieste descrierea din "description_box" cu descrierea kanji-ului ce va fi cast-uit
    public void Set_kanji_to_cast_description()
    {
        //setam descrierea kanji-ului selectat in cutiuta de descriere
        GameObject description_box = GameObject.FindGameObjectWithTag("Kanji_description_box");
        description_box.GetComponentInChildren<Text>().text = Cast_Manager.Get_Kanji_to_cast().description;

        //schimbam culoarea tuturor citirilor kun'yomi in albastru si a celor on'yomi in rosu
        foreach (string s in Cast_Manager.Get_Kanji_to_cast().onyomi_readings)
        {
            description_box.GetComponentInChildren<Kanji_Description_Manager>().Change_text_color(s, "red");
        }

        foreach (string s in Cast_Manager.Get_Kanji_to_cast().kunyomi_readings)
        {
            description_box.GetComponentInChildren<Kanji_Description_Manager>().Change_text_color(s, "blue");
        }
        Debug.Log("Muie PSD");
    }

}