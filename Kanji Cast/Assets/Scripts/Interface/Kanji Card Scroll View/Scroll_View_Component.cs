using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//componenta atasata de GameObject-ul "Kanji Card Scroll View"
public class Scroll_View_Component : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    public Scrollbar horiz_scrollbar;
    float lastValue = 0;

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    void OnEnable()
    {
        //Subscribe to the Scrollbar event

        horiz_scrollbar.onValueChanged.AddListener(scrollbarCallBack);
        lastValue = horiz_scrollbar.value;

    }

    //Will be called when Scrollbar changes
    void scrollbarCallBack(float value)
    {
        if (lastValue > value)
        {
            UnityEngine.Debug.Log("Scrolling Down: " + value);
            //Switch_kanji_card_detection(false);
            //StartCoroutine(WaitAndPrint());
            //Switch_kanji_card_detection(true);
        }
        else
        {
            UnityEngine.Debug.Log("Scrolling Up: " + value);
            //StartCoroutine(WaitAndPrint());
            //Switch_kanji_card_detection(false);
        }
        lastValue = value;
        //Switch_kanji_card_detection(true);
    }

    void OnDisable()
    {
        //Un-Subscribe To Scrollbar Event
        horiz_scrollbar.onValueChanged.RemoveListener(scrollbarCallBack);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    //on pointer exit vreau sa dispara descrierea
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Q-bism");
    }

    //mut box collider-u 2d cand se schimba valorea sroll view-ului astfel incat el sa fie tot timpul deasupra partii vizibile
    public void Move_Collider()
    {
        GameObject reference_collider_object = GameObject.Find("Collider onScreen Reference");
        Collider2D reference_collider = reference_collider_object.GetComponentInChildren<Collider2D>();

        GameObject kanji_card_scroll_view = GameObject.Find("Kanji Card Scroll View");
        Collider2D kanji_card_scroll_view_collider = kanji_card_scroll_view.GetComponentInChildren<Collider2D>();

        kanji_card_scroll_view_collider.transform.position = reference_collider.transform.position;
        //reference_collider.transform.position = kanji_card_scroll_view_collider.transform.position;
    }

    public void Grow_Collider(float grow_value)
    {
        BoxCollider2D collider = gameObject.GetComponentInChildren<BoxCollider2D>();
        collider.size = new Vector2(collider.size.x, collider.size.y+grow_value);
    }

    public void Shrink_Collider(float shrink_value)
    {
        BoxCollider2D collider = gameObject.GetComponentInChildren<BoxCollider2D>();
        collider.size = new Vector2(collider.size.x, collider.size.y - shrink_value);
    }

    //rearanjez toate kanji card-urile care se afla in interiorul "Kanji Card Scroll View"
    //sau sunt copii a acestuia 
    public void Rearrange_kanji_cards()
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");
        int i = 0;

        //astea sunt coordonatele initiale,adaugam inca un card pe axa x daca nu sunt 3 kanji card-uri pe un rand
        //daca avem 3 card-uri pe un rand adaugam un nou card pe un nou rand
        //la x adaugam 77,la y adaugam -128
        Vector3 initial_coordinates = new Vector3(154.0f,444.4f,0.0f);

        BoxCollider2D collider = gameObject.GetComponentInChildren<BoxCollider2D>();
        //asta e parintele unde sunt stocate toate kanji card-urile
        GameObject kanji_scroll_content = GameObject.Find("Kanji Card Scroll View Content");

        foreach (GameObject kanji_card in kanji_cards)
        {
            BoxCollider2D kanji_card_collider = kanji_card.GetComponentInChildren<BoxCollider2D>();

            //if (kanji_card.transform.position != initial_coordinates)
                if(Collider_contains_collider(collider, kanji_card_collider) || GameObject1_is_child_of_GameObject2(kanji_card,kanji_scroll_content))
            {
                    Debug.Log("kanji card parent is:"+kanji_card.transform.parent);
                    Debug.Log("kanji scroll content object is:" + kanji_scroll_content);
                    Debug.Log("kanji card " + kanji_card + " is child=" + GameObject1_is_child_of_GameObject2(kanji_card, kanji_scroll_content));
                kanji_card.transform.position = initial_coordinates;
                if (initial_coordinates.x != 308.0f) initial_coordinates.x = initial_coordinates.x + 77.0f;
                else
                {
                    initial_coordinates.x = 154.0f;
                    initial_coordinates.y = initial_coordinates.y - 128.0f;
                }
                //initial coordinates tre sa isi schimbe x-ul daca nu avem 3 carti pe rand sau y daca am gasit a 3-a carte pe rand
            }

            Debug.Log("kanji card "+kanji_card+" position=" + kanji_card.transform.position);
        }
    }

    //verifica daca boxcollideru containing_collider contine in intregime in coordonatele sale collideru contained_collider
    public bool Collider_contains_collider(BoxCollider2D containing_collider,BoxCollider2D contained_collider)
    {
        if (containing_collider.bounds.Contains(contained_collider.transform.position)) return true;
        else return false;
    }

    //verifia daca child_gameobject e copilul lui parent_gameobject
    public bool GameObject1_is_child_of_GameObject2(GameObject child_Gameobject, GameObject parent_Gameobject)
    {
        //if (child_Gameobject.transform.parent == parent_Gameobject) return true;
        if (child_Gameobject.transform.IsChildOf(parent_Gameobject.transform)) return true;
        else return false;
    }

    //public bool IsChildAliveInParent(Transform parent, GameObject childToCheck)
    //{
    //    bool isChildAlive = false;
    //    for (int i = 0; i < parent.childCount; i++)
    //    {
    //        if (parent.GetChild(i).Equals(childToCheck))
    //        {
    //            isChildAlive = true;
    //            break;
    //        }
    //    }
    //    return isChildAlive;
    //}

    //returneaza componenta BoxCollider2D a gameobjectului de care e atsat acest script
    public BoxCollider2D Get_BoxCollider2D()
    {
        return gameObject.GetComponentInChildren<BoxCollider2D>();
    }
}
