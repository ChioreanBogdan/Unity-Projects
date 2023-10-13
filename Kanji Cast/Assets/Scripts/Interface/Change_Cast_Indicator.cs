using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script carese ataseaza de bulina din dreptul fiecarui personaj si schimba culoarea (rosu personajul a folosit o abilitate)
//galben personajul nu a folosit nicio abilitate
public class Change_Cast_Indicator : MonoBehaviour {

    public Color unused_skill_color;
    public Color used_skill_color;

    // Use this for initialization
    void Start () {
        Update_cast_indicator();
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    //schimba culoarea din sprite rendererul gameObjectului de care e atsat script-ul
    public void Change_cast_indicator_color(Color new_color)
    {       
        SpriteRenderer sprite_component = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite_component.color = new_color;
    }

    public void Update_cast_indicator()
    {
        Battle_Stats character_stats = gameObject.GetComponentInParent<Battle_Stats>();
        if (character_stats.Get_used_skill() == false) Change_cast_indicator_color(unused_skill_color);
        else if (character_stats.Get_used_skill() == true) Change_cast_indicator_color(used_skill_color);
    }
                                        }
