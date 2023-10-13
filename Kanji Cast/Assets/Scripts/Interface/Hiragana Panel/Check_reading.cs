using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//verific citirile onyomi si kunyomi
public class Check_reading : MonoBehaviour {

    public bool checks_onyomi;

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //
    public void Is_reading_correct(InputField input_field)
    {
        Text onyomi_input_filed_text = input_field.GetComponentInChildren<Text>();
        bool there_is_a_correct_match = false;

        foreach (string s in Cast_Manager.Get_Kanji_to_cast().kunyomi_readings)
        {
            if (s == onyomi_input_filed_text.text) there_is_a_correct_match = true;
        }
        Debug.Log("There is a correct match:" + there_is_a_correct_match);
    }
}
