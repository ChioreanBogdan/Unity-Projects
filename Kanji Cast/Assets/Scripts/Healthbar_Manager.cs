using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Manager : MonoBehaviour {

    public int character_id;
    private Slider health_bar;

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //sets character_id to id
    public void Set_character_id(int id)
    {
        this.character_id = id;
    }

    public void Set_slider_Max_value(int max)
    {
        health_bar = gameObject.GetComponentInChildren<Slider>();
        health_bar.maxValue = max;
    }

    //incarc valorile sanatatii din script-ul Stats in healthbar
    public void Load_health(GameObject character, GameObject healthbar)
    {
        Battle_Stats char_battle_stats = character.GetComponentInChildren<Battle_Stats>();
        Slider healthbar_slider = healthbar.GetComponentInChildren<Slider>();
        Text healthbar_text = healthbar.GetComponentInChildren<Text>();

        healthbar_slider.maxValue = char_battle_stats.Max_health;
        healthbar_slider.value = char_battle_stats.Current_Health;
        healthbar_text.text = char_battle_stats.Current_Health + "/" + char_battle_stats.Max_health;
    }
}
