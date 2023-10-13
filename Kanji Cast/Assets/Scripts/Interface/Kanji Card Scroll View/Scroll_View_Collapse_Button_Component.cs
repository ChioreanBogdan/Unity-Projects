using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//script-ul atasat de imaginea care va toggle-ui aparitia/disparitia scroll view-ului cu kanji carduri de pe ecran
public class Scroll_View_Collapse_Button_Component : MonoBehaviour {

    //daca is_collapsed==true se va vedea scroll view-ul continand kanji card-uri
    bool is_collapsed;
    public Sprite collapsed_image; public Sprite uncollapsed_image;
    //memorez pozitia initiala a GameObject-ului "Kanji card scroll view"
    float Scroll_view_init_x; float Scroll_view_init_y;
    //aici memoram GameObject-ulde tip "Scroll view":"Kanji card scroll view"
    GameObject scroll_view;

    // Use this for initialization
    void Start()
    {
        scroll_view = GameObject.Find("Kanji Card Scroll View");
        Scroll_view_init_x = scroll_view.transform.position.x; Scroll_view_init_y = scroll_view.transform.position.y;
        Debug.Log("Scroll init pos x=" + Scroll_view_init_x); Debug.Log("Scroll init pos y=" + Scroll_view_init_y);
    }

    //// Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("2 10 2 10");
        switch (is_collapsed)
        {
          case true:
            gameObject.GetComponent<Image>().sprite = uncollapsed_image;
            is_collapsed = false;
                Change_GameObject_Position(scroll_view, Scroll_view_init_x, Scroll_view_init_y);
                Debug.Log("is collapsed:" +is_collapsed);
                //schimb parintii tutror kanji card-urilor care se afla in spatiul GameObject-ului "Kanji Card Scroll View"
                //Change_Kanji_Card_Parent();
            break;

            case false:
              gameObject.GetComponent<Image>().sprite = collapsed_image;
              is_collapsed = true;
                Change_GameObject_Position(scroll_view, 234f, 420.5f);
                Debug.Log("is collapsed:" + is_collapsed);
                break;
        }
    }

    //(aici trebuie revenit) :(
    void OnMouseEnter()
    {
        GameObject con = GameObject.Find("Content");
        Debug.Log("kon=" + con);
        Kanji_Card_Keeper kck = con.GetComponentInChildren<Kanji_Card_Keeper>();
        if (kck != null)
        {
            Debug.Log("kck=" + kck);
            //kck.Detect_Kanji_cards_in_contents();
        }

        if (is_collapsed) Change_Kanji_Card_Parent();
    }

    //aduce Gameobject-ul gameobject_name la coordonate x si y specificate
    public void Change_GameObject_Position(GameObject game_object,float x_pos,float y_pos)
    {
        game_object.transform.position = new Vector3(x_pos, y_pos,game_object.transform.position.z);
    }

    //schimba parintele kanji_card-urilor cu "Kanji Card Scroll View Content"
    //daca un kanji_card se afla in intregime in interiourl boxcollider-ului 2d al "Kanji Card Scroll View"
    public void Change_Kanji_Card_Parent()
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        GameObject kc_sv = GameObject.Find("Kanji Card Scroll View");
        GameObject kc_svc = GameObject.Find("Kanji Card Scroll View Content");
        //asta e un GameObject gol care are un boxcollider 2d ce se suprapune pe locul unde apare GameObject-ul "Kanji Card Scroll View" atunci cand e collapsat
        GameObject kc_svo = GameObject.Find("Card Scroll View Overlapper");
        BoxCollider2D overlap_collider = kc_svo.GetComponentInChildren<BoxCollider2D>();

        Scroll_View_Component svc = kc_sv.GetComponentInChildren<Scroll_View_Component>();

        foreach(GameObject kanji_card in kanji_cards)
        {
            BoxCollider2D kanji_card_collider = kanji_card.GetComponentInChildren<BoxCollider2D>();
            if (svc.Collider_contains_collider(overlap_collider,kanji_card_collider))
            kanji_card.transform.parent = kc_svc.transform;
            Debug.Log("scv.Get_BoxCollider2D()=" + svc.Get_BoxCollider2D());
        }
    }

    //scoate game object-ul de care e atasat script=ul de pe ecranul principal
    public void Close_the_Scroll_View()
    {
        gameObject.GetComponent<Image>().sprite = uncollapsed_image;
        is_collapsed = false;
        Change_GameObject_Position(scroll_view, Scroll_view_init_x, Scroll_view_init_y);
        Change_Kanji_Card_Parent();
    }
}
