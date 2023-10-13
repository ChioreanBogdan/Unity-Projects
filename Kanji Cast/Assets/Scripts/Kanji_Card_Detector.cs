using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script atasat de slot-ul in partea de jos
//daca e drag-uit un kanji in collision box-ul lui,schimb kanji-ul din kanji container si distrug kanji-ul drag-uit
public class Kanji_Card_Detector : MonoBehaviour {

    //detecteaza daca este un card in zona
    public int unique_id;
    public float snap_distance;
    public bool smallest_distance;
    public bool occupied_slot;
    private bool check_for_kanji_updates;

    //daca e un cast slot nu vrem sa fie folosit pt operatii de fuziune si sa fie luat in considerare pt aceste operatiuni
    public bool is_cast_slot;

	// Use this for initialization
	void Start () {
        Generate_unique_id();
        //BoxCollider2D box = this.GetComponentInChildren<BoxCollider2D>();
        //box.enabled = false;
                  }
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0, 0, 0), 10 * Time.deltaTime);

        //chiar nu exista solutie mai buna decat s-o pun aici? :(((
        //Change_kanji_containers_in_slots(); check_for_kanji_updates = false;
                    }

    // called when a kanji card is dragged
    //NU UITA,ca sa detecteze coliziuni tre sa atasezi un rigidbody de kanji card
    void OnTriggerEnter2D(Collider2D card)
    {
        //if (card.gameObject.tag == "Kanji_Card" && occupied_slot)
        //{
        //    Kanji_Container change_kanji = gameObject.GetComponentInChildren<Kanji_Container>();
        //    change_kanji.Set_contained_kanji(card.GetComponentInChildren<Kanji_Container>().Get_contained_kanji());
        //}

        Reset_distances();
        Change_snap_distances(card.gameObject);

        smallest_distance = This_is_the_smallest_distance();
        //this.occupied_slot = true;

        if (card.gameObject.tag == "Kanji_Card" && This_is_the_smallest_distance()==true && !this.occupied_slot)
        {
            Debug.Log("Ayayaa this slot is="+this.occupied_slot);
            BoxCollider2D box=this.GetComponentInChildren<BoxCollider2D>();

            //Destroy(card.gameObject);

            StartCoroutine(Move_Kanji_card_in_center_of_slot(card.gameObject,gameObject,5000,0.5f));

            //re aranjam slot-urile dupa fiecare plasare de card
            Clear_kanji_slots();
            //occupied_slot = true;
            Change_kanji_containers_in_slots();
        }

        //Fusion_Manager.Get_Fusion_Material();
        //Fusion_Manager.Return_fusionable_kanji();
    }

    //Moves card in ceter of slot at "speed" speed
    public IEnumerator Move_Kanji_card_in_center_of_slot(GameObject card,GameObject slot,int speed,float seconds_to_wait)
    {
        for (int i = 0; i <= 500;)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, slot.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(seconds_to_wait);
            i++;
            if (card.transform.position == slot.transform.position)
            {
                if (!is_cast_slot)
                {
                    Fusion_Manager.Get_Fusion_Material();
                    Fusion_Manager.Return_fusionable_kanji();
                    Fusion_Manager.Check_fusion_possibilities();
                    Debug.Log("Current fusion card is=" + Fusion_Manager.Get_current_fusion_card());
                }
                else if (is_cast_slot) Cast_Manager.Update_Kanji_id_to_cast();
                break;
            }
        }

    }

    //Generez un id unic pt fiecare slot,verific daca id-ul e luat,daca nu maresc cu 1
    public void Generate_unique_id()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
        int i = 0;

        foreach (GameObject kanji_slot in kanji_slots) {
            i++;
            Kanji_Card_Detector card_detector = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
            if (i != card_detector.unique_id)
            {
                unique_id = i; break;
            }
                                                       }
    }

    public void Reset_distances()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach (GameObject kanji_slot in kanji_slots)
        {
            Kanji_Card_Detector kcd = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
            kcd.smallest_distance = false;
            kcd.snap_distance = 0;
        }
    }

    //functie ca,daca am o cartela care se suprapune peste doua sau mai multe kanji slot-uri sa se snap-uiasca doar pe cel de care e cel mai apropiata
    public void Change_snap_distances(GameObject kanji_card)
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach(GameObject kanji_slot in kanji_slots)
        {
            float s_dist = Get_GameObject_distance(kanji_slot, kanji_card);
            Set_Snap_distance(kanji_slot, s_dist);
        }
    }

    //functie care verifica daca snap_distance-ul de are e atasat acest script e cel mai mic dintre toate snap script-urile
    public bool This_is_the_smallest_distance()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        Kanji_Card_Detector starting_minimum = kanji_slots[0].GetComponentInChildren<Kanji_Card_Detector>();
        float minimum = starting_minimum.snap_distance;

        foreach (GameObject kanji_slot in kanji_slots)
        {
            Kanji_Card_Detector card_d = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
            if (card_d.snap_distance < minimum) minimum = card_d.snap_distance;
            if (card_d.snap_distance > minimum) card_d.smallest_distance = false; 
        }

        if (this.snap_distance == minimum) return true;
        else return false;
    }

    //Returnez distanta dintre doua gameobject-uri
    public float Get_GameObject_distance(GameObject kanji_card,GameObject kanji_slot)
    {
        Vector3 kanji_card_position = kanji_card.transform.position;
        Vector3 kanji_slot_position = kanji_slot.transform.position;

        return Mathf.Abs(Vector3.Distance(kanji_card_position, kanji_slot_position));
    }

    public void Set_Snap_distance(GameObject kanji_slot,float new_distance)
    {
        Kanji_Card_Detector card_detector = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
        card_detector.snap_distance = new_distance;
    }

    //Turn boxcollider of GameObject this script is attached to on and off,i=0 off else on
    public void Turn_off_box_collider()
    {
        BoxCollider2D box = this.GetComponentInChildren<BoxCollider2D>();
        box.enabled = false;
    }

    public void Turn_on_box_collider()
    {
        BoxCollider2D box = this.GetComponentInChildren<BoxCollider2D>();
        box.enabled = true;
    }

    //golesc preventiv kanji container-urile si setez bool-ul Occupied_slot la false preventiv
    public void Clear_kanji_slots()
    {
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");

        foreach(GameObject card_slot in kanji_slots)
        {
            foreach (GameObject kanji_card in kanji_cards)
            {
                //golim preventiv toate slot-urile de kanji-uri si le setam ca neocupate,apoi verificam daca sunt kanji-uri in ele
                Kanji_Card_Detector card_detector = card_slot.GetComponentInChildren<Kanji_Card_Detector>();
                Radical_Container card_container = card_slot.GetComponentInChildren<Radical_Container>();
                card_detector.occupied_slot = false;
                //card_container.contained_radical = null;
            }
        }
    }

    public void Change_kanji_containers_in_slots()
    {
        GameObject[] kanji_cards = GameObject.FindGameObjectsWithTag("Kanji_Card");
        GameObject[] kanji_slots = GameObject.FindGameObjectsWithTag("Kanji_Slot");

        foreach (GameObject kanji_slot in kanji_slots)
        {
            foreach (GameObject kanji_card in kanji_cards)
            {
                if (kanji_card.transform.position == kanji_slot.transform.position)
                {
                    Radical_Container radical_card_container = kanji_card.GetComponentInChildren<Radical_Container>();
                    Radical_Container radical_slot_container = kanji_slot.GetComponentInChildren<Radical_Container>();

                    radical_slot_container.Set_contained_radical(radical_card_container.Get_contained_radical());

                    Kanji_Card_Detector kanji_slot_detector = kanji_slot.GetComponentInChildren<Kanji_Card_Detector>();
                    kanji_slot_detector.Set_occupied_slot(true);

                    if (kanji_slot_detector.is_cast_slot) Cast_Manager.Set_Kanji_id_to_cast(radical_card_container.Get_radical_id());
                }
            }
        }
    }

    //verifica daca bool-ul get cast slot e true sau false
    public bool Get_is_cast_slot()
    {
        return is_cast_slot;
    }

    //setez occupied slot sa fie true sau false
    public void Set_occupied_slot(bool oc)
    {
        occupied_slot = oc;
    }


}
