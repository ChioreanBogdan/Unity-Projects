using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollBar_Component : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    // Use this for initialization
    void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    public void OnPointerClick(PointerEventData eventData)
    {
        Bring_Masking_Panel_in_front();
        Debug.Log("Scrollbar clicked");

        GameObject kanji_card_scroll_view = GameObject.Find("Kanji Card Scroll View");
        Scroll_View_Component svc = kanji_card_scroll_view.GetComponentInChildren<Scroll_View_Component>();
        svc.Rearrange_kanji_cards();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Bring_Masking_Panel_in_front();
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Switch_kanji_card_detection(false);
    //}

    //on pointer exit vreau sa dispara descrierea
    public void OnPointerExit(PointerEventData eventData)
    {
        //Send_the_Gameobject_in_the_back();
    }

    //dau disable la componentele Collider2D a tuturor kanji card-urilor pentru a nu se prinde de pointer cand dau scroll in jos
    public void Switch_kanji_card_detection(bool new_value)
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach (GameObject kanji_card in kanji_cards)
        {
            Kanji_Image_Manager kim = kanji_card.GetComponentInChildren<Kanji_Image_Manager>();
            kim.enabled = new_value;
        }
    }

    //aduc panoul de mascare in fata ca sa nu se lipeasca aiurea cardu de mouse pointer de fiecare data cand intru/dau click
    //in zona de scroll
    public void Bring_Masking_Panel_in_front()
    {
        GameObject masking_panel=GameObject.Find("Masking_Panel");
        GameObject kc_svc = GameObject.Find("Kanji Card Scroll View Content");
        GameObject kc_sv = GameObject.Find("Kanji Card Scroll View");
        Scroll_View_Component svc = kc_sv.GetComponentInChildren<Scroll_View_Component>();

        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach (GameObject kanji_card in kanji_cards)
        {
            //kanji_card.transform.parent = kc_svc.transform;
            if(svc.Collider_contains_collider(svc.Get_BoxCollider2D(),kanji_card.GetComponentInChildren<BoxCollider2D>())) kanji_card.transform.parent = kc_svc.transform;
        }

        //kc_sv.transform.SetSiblingIndex(1);
        masking_panel.transform.SetAsLastSibling();
    }

    //daca ies din zona verticala a scrollbar-ului
    public void Send_the_Gameobject_in_the_back()
    {
        gameObject.transform.SetAsFirstSibling();

        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach (GameObject kanji_card in kanji_cards)
        {
            kanji_card.transform.SetAsLastSibling();
        }
    }
}
