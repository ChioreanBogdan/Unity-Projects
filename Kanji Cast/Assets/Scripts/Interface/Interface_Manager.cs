using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface_Manager : MonoBehaviour {

    public Fused_Kanji fk;

    //avem nevoie de bool-u cesta ca StartCoroutine(Portrait_flash()) sa se apeleze o singura data
    //(nu de mai multe ori pe frame)
    static bool portrait_called_only_once = true;

    //panourile suprapuse peste panourile aliatilor/inamicilor care vrem sa clipoceasca
    static string panels_to_flash;

    // Use this for initialization
    void Start ()   {      
        Generate_unique_IDs();
        //Resize_Collision_Boxes();

        Hide_Selection_Panels("Character_Select_Panel");
        Hide_Selection_Panels("Enemy_Select_Panel");
        //Debug.Log("Fk=" + fk.description);
        //Debug.Log("Fusion was succesful:" + fk.Fuse(comp));
        //Functie care face sa clipoceasca poretul unui personaj
        //O sa o folosesc atunci cand e selectat meniul Cast
        //InvokeRepeating("Flashing_portait", 1f, 1f);
        //InvokeRepeating("Portrait_Flash_Off", 1f, 2f);
        //StartCoroutine(Flashing_Portraits());
    }
	
	// Update is called once per frame
	void Update () {
        //if (Battle_manager.Get_ability_activated() == true) //&& portrait_called_only_once == true)
        //                                                    //{
        //    if (portrait_called_only_once == true)
        //    {
        //        StartCoroutine(Portrait_flash("Character_Select_Panel",500));
        //        StartCoroutine(Portrait_flash("Enemy_Select_Panel",500));
        //        portrait_called_only_once = false;
        //    }
        //if(Battle_manager.selected_kanji_id!=0 && Battle_manager.selected_kanji_id!=0)
        //    Update_panels_to_flash(panels_to_flash);
                   }

    //Dam fiecarui personaj si inamic un id unic si le legam de bara de viata
    //luam viata maxima si viata curenta din scriptu' battle_stats si le afisam in textul si slideru atasate de healthbar
    public void Generate_unique_IDs()
    {
        int i = 0;
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Health_bars = GameObject.FindGameObjectsWithTag("Healthbar");
        Healthbar_Manager hm = new Healthbar_Manager();

        foreach (GameObject character in characters)
        {
            //Debug.Log("Personajul " + character);
      
            //Debug.Log("i= " + i);
            i++;
            character.GetComponentInChildren<Battle_Stats>().Set_Unique_id(i);
            Health_bars[i-1].GetComponentInChildren<Healthbar_Manager>().Set_character_id(i);

            hm.Load_health(character, Health_bars[i - 1]);
        }

        foreach (GameObject enemy in enemies)
        {
            //Debug.Log("Inamicul " + enemy);
           
            i++;
            Health_bars[i-1].GetComponentInChildren<Healthbar_Manager>().Set_character_id(i);
            enemy.GetComponentInChildren<Battle_Stats>().Set_Unique_id(i);

            hm.Load_health(enemy, Health_bars[i - 1]);
            //Debug.Log("i= " + i);
        }

        foreach (GameObject health_bar in Health_bars)
        {
            //Debug.Log("Bara viata " + health_bar);
        }
    }

    //asteapta timp de se secunde
    IEnumerator Flashing_Portrait_On(float sec)
    {
        GameObject panel = GameObject.Find("Active_Panel");

        panel.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(sec);
    }

    public IEnumerator Flashing_Portrait_Off(float sec)
    {
        GameObject panel = GameObject.Find("Active_Panel");

        yield return new WaitForSeconds(sec);
        panel.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Flashing_portait()
    {
        int i = 0;
        for (i = 0; i <= 500; i++)
        {
            if (i % 2 == 0) StartCoroutine(Flashing_Portrait_On(0.5f));
            else if (i % 2 != 0) StartCoroutine(Flashing_Portrait_Off(0.5f));
        }
    }

    public void Portrait_Flash_On()
    {
        Debug.Log("Flash on");
        GameObject panel = GameObject.Find("Flashing_Panel");

        panel.transform.localScale = new Vector3(1, 1, 1);
    }

    int i = 0;
    public void Portrait_Flash_Off()
    {
        Debug.Log("Flash off="+i);
        GameObject panel = GameObject.Find("Flashing_Panel");

        panel.transform.localScale = new Vector3(0, 0, 0);
        i++;
    }

    //asta e functia care nu depinde de InvokeRepeating
    public static IEnumerator Portrait_flash(string tag_name,int flash_duration)
    {
        GameObject[] panels_to_flash = GameObject.FindGameObjectsWithTag(tag_name);

            for (int i = 0; i <= flash_duration;)
            {
            foreach (GameObject panel_to_flash in panels_to_flash) panel_to_flash.transform.localScale = new Vector3(1, 1, 1);
                yield return new WaitForSeconds(1f);
            foreach (GameObject panel_to_flash in panels_to_flash) panel_to_flash.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1f);

            i++;
            }
    }
    //}

    //Functie care ascunde panourile de selectie a GameObject-urilor cu tag-ul "panels_to_hide"
    public void Hide_Selection_Panels(string tag_name)
    {
        GameObject[] panels_to_hide = GameObject.FindGameObjectsWithTag(tag_name);

        foreach (GameObject panel_to_hide in panels_to_hide)
        {
            panel_to_hide.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    //updateaza health bar-ul atasat de GameObject-ul obj
    public static void Update_Health_Bar(GameObject obj,int new_current_health)
    {
        try
        {
            Battle_Stats bs = obj.GetComponentInChildren<Battle_Stats>();

            GameObject[] healthbars = GameObject.FindGameObjectsWithTag("Healthbar");
            foreach (GameObject healthbar in healthbars)
            {
                Healthbar_Manager hm = healthbar.GetComponentInChildren<Healthbar_Manager>();
                if (hm.character_id == bs.unique_id)
                {
                    bs.Set_Current_Health(new_current_health);
                    hm.Load_health(obj,healthbar);
                }
            }
        }
        catch(MissingComponentException)
        {

        }
    }

    public void Resize_Collision_Boxes()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject character in characters)
        {
            BoxCollider2D character_box=character.GetComponentInChildren<BoxCollider2D>();
            character_box.transform.position = character.transform.position;
            character_box.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        foreach (GameObject enemy in enemies)
        {
            BoxCollider2D character_box = enemy.GetComponentInChildren<BoxCollider2D>();
            character_box.transform.position = enemy.transform.position;
            character_box.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    //Face update la toate cast indicator-urile presonajelor
    //schimba culorile bulelor din dreptul portretelor personajelor daca acestea au folosit sau nu o abilitate
    public static void Update_cast_indicator_colors()
    {
        GameObject[] cast_indicators = GameObject.FindGameObjectsWithTag("Cast_Indicator");

        foreach(GameObject cast_indicator in cast_indicators)
        {
            Change_Cast_Indicator color_change_component = cast_indicator.GetComponentInChildren<Change_Cast_Indicator>();
            color_change_component.Update_cast_indicator();
        }
    }

    //seteaza ce panouri sa clipoceasca,functie de cine afecteaza abilitatea (personaje/inamici,etc)

    public void Update_panels_to_flash(string afftected_parties)
    {
        if (Battle_manager.Get_ability_activated() == true) //&& portrait_called_only_once == true)
                                                            //{
            if (portrait_called_only_once == true || afftected_parties == "No one")
            {
                if (afftected_parties == "Enemies")
                {
                    StartCoroutine(Portrait_flash("Enemy_Select_Panel", 500));
                    Debug.Log("Enemies should blink");
                }
                else if (afftected_parties == "Allies")
                {
                    StartCoroutine(Portrait_flash("Character_Select_Panel", 500));
                    Debug.Log("Allies should blink");
                }
                else if (afftected_parties == "No one")
                {
                    StopAllCoroutines();
                    Hide_Selection_Panels("Character_Select_Panel");
                    Hide_Selection_Panels("Enemy_Select_Panel");
                    Debug.Log("Blinking should stop");
                }

                portrait_called_only_once = false;
            }

        //panourile tre puse pe local scale 0
        //if (afftected_parties == "No one")
        //{
        //    //StopCoroutine(Portrait_flash("Character_Select_Panel", 500));
        //    //StopCoroutine(Portrait_flash("Enemy_Select_Panel", 500));
        //    StopAllCoroutines();
        //    Debug.Log("rise");
        //    //StartCoroutine(Portrait_flash("Enemy_Select_Panel",0));

        //}
    }

    public static string Get_panels_to_flash()
    {
        return panels_to_flash;
    }

    public static bool Get_portrait_called_only_once()
    {
        return portrait_called_only_once;
    }

    public static void Set_panels_to_flash(string afftected_parties)
    {
        panels_to_flash=afftected_parties;
    }

    public static void Set_portrait_called_only_once(bool new_value)
    {
        portrait_called_only_once = new_value;
    }
}
