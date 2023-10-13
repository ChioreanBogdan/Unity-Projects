using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//asta e un fel de Kanji Card detector dar atasat de "Content" din "Kanji Card Scroll View"
public class Kanji_Card_Keeper : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Detect_Kanji_cards_in_contents();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //detecteaza toate kanji card-urile din "contents"
    public void Detect_Kanji_cards_in_contents()
    {
        //GameObject content = GameObject.Find("Content");
        //GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");
        //BoxCollider2D box_coll = content.GetComponentInChildren<BoxCollider2D>();
        //int i = 0;

        //foreach (GameObject kanji_card in kanji_cards)
        //{
        //    BoxCollider2D kanji_card_collider = kanji_card.GetComponentInChildren<BoxCollider2D>();
        //    Debug.Log("Kanji card=" + kanji_card);
        //    Debug.Log("Kanji card collider=" + kanji_card_collider);
        //    //if (box_coll.bounds.Contains(kanji_card_collider.bounds.min) && box_coll.bounds.Contains(kanji_card_collider.bounds.max))
        //    //{
        //    //    Debug.Log("Avem " + i + " kanji card-uri in kanji card scroll view");
        //    //}
        //}
    }



    public void Collider_test()
    {
        //Debug.Log("Kemono michi");
    }
}
