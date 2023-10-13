using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radical_Container : MonoBehaviour {

    public Radical contained_radical;

	// Use this for initialization
	void Start () {
        Get_radical_data();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //setez text-ul imaginii de care atasat radical-ul cu simbolul radical-ului
    public void Get_radical_data()
    {
        string radical_symbol = "";
        //Debug.Log("gameObject name="+gameObject.name);
        if (contained_radical != null)
        {
            radical_symbol = contained_radical.symbol;
            gameObject.GetComponentInChildren<Text>().text = radical_symbol;
        }
    }

    //iau id-ul obiectului kanji atasat de script-ul kanji container
    public int Get_radical_id()
    {
        if (contained_radical != null) return contained_radical.id_number;
        else return 0;
    }

    //iau descrierea obiectului kanji atasat de script-ul kanji container
    public string Get_radical_description()
    {
        return contained_radical.description;
    }

    public Radical Get_contained_radical()
    {
        return contained_radical;
    }

    public void Set_contained_radical(Radical new_radical)
    {
        contained_radical = new_radical;
    }
}
