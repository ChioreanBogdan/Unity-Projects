using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script care sa faca legatura dintre un obiect de tip kanji si o imagine cu un kanji de pe ecran
public class Kanji_Container : MonoBehaviour {

    public Kanji contained_kanji;

	// Use this for initialization
	void Start () {
        Get_kanji_data();
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //setez text-ul imaginii de care atasat kanji-ul cu simbolul kanji-ului
    public void Get_kanji_data()
    {
        string kanji_symbol = "";
        //Debug.Log("gameObject name="+gameObject.name);
        if (contained_kanji!=null)
        {
            kanji_symbol = contained_kanji.symbol;
            gameObject.GetComponentInChildren<Text>().text = kanji_symbol;
        }
    }

    //iau id-ul obiectului kanji atasat de script-ul kanji container
    public int Get_kanji_id()
    {
        //try
        //{
        //    return contained_kanji.id_number;
        //}

        //catch (NullReferenceException e)
        //{
        //    return 0;
        //}
        if (contained_kanji != null) return contained_kanji.id_number;
        else return 0;
    }

    //iau descrierea obiectului kanji atasat de script-ul kanji container
    public string Get_kanji_description()
    {
        return contained_kanji.description;
    }

    public string[] Get_kanji_onyomi_readings()
    {
        return contained_kanji.onyomi_readings;
    }

    public string[] Get_kanji_kunyomi_readings()
    {
        return contained_kanji.kunyomi_readings;
    }

    public Kanji Get_contained_kanji()
    {
        return contained_kanji;
    }

    public void Set_contained_kanji(Kanji new_kanji)
    {
        contained_kanji = new_kanji;
    }
}
