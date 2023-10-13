using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//obiect atasat de cutiuta care contine descrierea 
public class Kanji_Description_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Change_text_color("insert","blue");
                  }
	
	// Update is called once per frame
	void Update () {
        //Update_kanji_description();
                   }

    //Schimb culoarea string-ului word cu new_color
    public void Change_text_color(string word,string new_color)
    {
        string description_text = this.GetComponentInChildren<Text>().text;
        int description_text_size = description_text.Length;

        if (description_text.Contains(word))
        {
            int index_start = description_text.IndexOf(word);
            int index_finish = index_start + word.Length+"<color=".Length+new_color.Length+">".Length;

            description_text=description_text.Insert(index_start,"<color="+new_color+">");
            description_text=description_text.Insert(index_finish, "</color>");

            this.GetComponentInChildren<Text>().text = description_text;

            Debug.Log("index_start=" + index_start);
            Debug.Log("index_start=" + index_finish);
        }

        Debug.Log("description_text="+ description_text);
    }

    //schimb descrierea kanji-ului din "description box" cu described kanji
    public void Change_description(Kanji described_kanji)
    {

    }
}
