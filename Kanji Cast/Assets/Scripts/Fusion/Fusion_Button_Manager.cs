using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion_Button_Manager : MonoBehaviour {

    GameObject Fusion_point;

    //verifica daca sunt in miscare kanji-urile pt a amana crearea fusion card-ului in fusion point
    //pana se termina kanji-uruile de miscat
    bool kanji_are_moving = true;

    //aici adaug card-urile kanji folosite in fuziune
    List<GameObject> Fusion_cards = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //Gasim toate card-urile kanji si le mutam intr-o lista 
    public void Get_cards_for_fusion()
    {
        Fusion_point = GameObject.Find("Fusion Point");

        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        //selectez card-urile la care bool-ul "marked_for_fusion" e true
        foreach (GameObject card in kanji_cards)
        {
            Kanji_Image_Manager image_manager = card.GetComponentInChildren<Kanji_Image_Manager>();
            if (image_manager.marked_for_fusion) Fusion_cards.Add(card);
        }
    }

    //Moves cards in list to the fusion point at "speed" speed
    public IEnumerator Move_Kanji_cards_to_fusion_point(GameObject card, GameObject fusion_point, int speed, float seconds_to_wait)
    {
        for (int i = 0; i <= 500;)
        {
            if (card != null)
            {
                card.transform.position = Vector3.MoveTowards(card.transform.position, fusion_point.transform.position, speed * Time.deltaTime);
                yield return new WaitForSecondsRealtime(seconds_to_wait);
                i++;
                if (card != null)
                    if (card.transform.position == fusion_point.transform.position)
                    {
                        Destroy(card);
                        kanji_are_moving = false;
                        Debug.Log("kanji_are_moving=" + kanji_are_moving);
                        break;
                    }
            }
            else break;
        }
    }

    //generaza un Gameobject la pozitia fusion point-ului
    public IEnumerator Generate_fusion_card()
    {
        yield return new WaitForSeconds(1f);
        if (kanji_are_moving == false)
        {
            //cream un card nou si il facem copil al ecranului ca sa poata fi vazut si il punem pe ecran
            GameObject test_card = GameObject.Find("Fusion Card");
            GameObject card_clone = Instantiate(test_card, Fusion_point.transform.position, Quaternion.identity);
            GameObject main_screen = GameObject.Find("Main_screen");
            card_clone.transform.SetParent(main_screen.transform);
            Radical_Container card_clone_container = card_clone.GetComponentInChildren<Radical_Container>();
            card_clone_container.Set_contained_radical(Fusion_Manager.Get_current_fusion_card());
            //dupa ce am generat un fusion card o setam pe null
            Fusion_Manager.Set_current_fusion_card(null);
        }
    }

    //Trebuie ca toate variabilele din Fusion_Manager sa devina din nou nule
    public void Reset_fusion_manager()
    {

    }

    //Functie legata de butonul "Fuse" (presepunem ca avem in slot-uri butoane care pot face fusion)
    public void Fusion_Button_pressed()
    {
        Get_cards_for_fusion();
        foreach (GameObject card in Fusion_cards)
        {
            StartCoroutine(Move_Kanji_cards_to_fusion_point(card, Fusion_point, 1000, 0.1f));
        }
        if (Fusion_Manager.Get_current_fusion_card()!=null) StartCoroutine(Generate_fusion_card());
    }

}
