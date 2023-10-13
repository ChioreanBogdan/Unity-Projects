using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hiragana_Converter : MonoBehaviour {

    //ia textul tastat in romaji in textboxt si il transforma in hiragana (daca e posibil)

    GameObject Onyomi_Placeholder;
    string input_text="";
    bool change_text;

    //variabila care verifica daca ce e in textbox e doar hiragana
    public bool contains_only_hiragana;

    List<char> hiragana_characters = new List<char>() { 'か', 'さ', 'た', 'な', 'は', 'ま', 'や', 'ら', 'わ', 'が', 'ざ', 'だ', 'ば', 'ぱ', 'あ', 'き', 'し', 'ち', 'に', 'ひ', 'み', 'り', 'ぎ', 'じ', 'ぢ', 'び', 'ぴ', 'い', 'く', 'す', 'つ', 'ぬ', 'ふ', 'む', 'ゆ', 'る', 'ぷ', 'ぐ', 'ず', 'づ', 'ぶ', 'う', 'け', 'せ', 'て', 'ね', 'へ', 'め', 'れ', 'げ', 'ぜ', 'で', 'べ', 'ぺ', 'え', 'こ', 'そ', 'と', 'の', 'ほ', 'も', 'よ', 'ろ', 'を', 'ご', 'ぞ', 'ど', 'ぼ', 'ぽ', 'お', 'ゔ', 'ん', 'ゃ', 'ゅ', 'ょ', 'っ' };


    // Use this for initialization
    void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {

                   }

    //Inlocuieste ce scrii in romaji cu hiragana
    public void Replace_hiragana(GameObject incantation_field)
    {
        //a field conversion
        InputField ifield = incantation_field.GetComponentInChildren<InputField>();

        input_text = ifield.text.Replace("ka", "か"); input_text = input_text.Replace("sa", "さ").Replace("ta", "た");
        input_text = input_text.Replace("na", "な").Replace("ha", "は").Replace("ma", "ま").Replace("ya", "や");
        input_text = input_text.Replace("ra", "ら").Replace("wa", "わ").Replace("ga", "が").Replace("za", "ざ");
        input_text = input_text.Replace("da", "だ").Replace("ba", "ば").Replace("pa", "ぱ").Replace("a", "あ");

        input_text = input_text.Replace("ki", "き").Replace("shi", "し").Replace("chi", "ち").Replace("ni", "に");
        input_text = input_text.Replace("hi", "ひ").Replace("mi", "み").Replace("ri", "り").Replace("gi", "ぎ");
        input_text = input_text.Replace("ji", "じ").Replace("dji", "ぢ").Replace("bi", "び").Replace("pi", "ぴ").Replace("i", "い");

        input_text = input_text.Replace("ku", "く").Replace("su", "す").Replace("tsu", "つ").Replace("nu", "ぬ");
        input_text = input_text.Replace("fu", "ふ").Replace("mu", "む").Replace("yu", "ゆ").Replace("ru", "る").Replace("pu", "ぷ");
        input_text = input_text.Replace("gu", "ぐ").Replace("zu", "ず").Replace("dzu", "づ").Replace("bu", "ぶ").Replace("u", "う");

        input_text = input_text.Replace("ke", "け").Replace("se", "せ").Replace("te", "て").Replace("ne", "ね");
        input_text = input_text.Replace("he", "へ").Replace("me", "め").Replace("re", "れ").Replace("ge", "げ");
        input_text = input_text.Replace("ze", "ぜ").Replace("de", "で").Replace("be", "べ").Replace("pe", "ぺ").Replace("e", "え");

        input_text = input_text.Replace("ko", "こ").Replace("so", "そ").Replace("to", "と").Replace("no", "の");
        input_text = input_text.Replace("ho", "ほ").Replace("mo", "も").Replace("yo", "よ").Replace("ro", "ろ");
        input_text = input_text.Replace("wo", "を").Replace("go", "ご").Replace("zo", "ぞ").Replace("do", "ど");
        input_text = input_text.Replace("bo", "ぼ").Replace("po", "ぽ").Replace("o", "お").Replace("v", "ゔ").Replace("n", "ん");

        input_text = input_text.Replace("kya", "きゃ").Replace("kyu", "きゅ").Replace("kyo", "きょ").Replace("mya", "みゃ");
        input_text = input_text.Replace("myu", "みゅ").Replace("myo", "みょ").Replace("sha", "しゃ").Replace("shu", "しゅ");
        input_text = input_text.Replace("sho", "しょ").Replace("rya", "りゃ").Replace("ryu", "りゅ").Replace("ryo", "りょ");
        input_text = input_text.Replace("cha", "さゃ").Replace("chu", "さゅ").Replace("cho", "さょ").Replace("gya", "ぎゃ");
        input_text = input_text.Replace("gyu", "ぎゅ").Replace("gyo", "ぎょ").Replace("nya", "にゃ").Replace("nyu", "にゅ");
        input_text = input_text.Replace("nyo", "にょ").Replace("ja", "じゃ").Replace("ju", "じゅ").Replace("jo", "じょ");
        input_text = input_text.Replace("hya", "ひゃ").Replace("hyu", "ひゅ").Replace("hyo", "ひょ").Replace("bya", "びゃ");
        input_text = input_text.Replace("byu", "びゅ").Replace("byo", "びょ").Replace("pya", "ぴゃ").Replace("pyu", "ぴゅ").Replace("pyo", "ぴょ");

        ifield.text = input_text;
    }

    //detecteaza daca avem doua consoane consecutive si o inlocuieste pe prima cu っ
    public void Double_consonant_replacer(GameObject incantation_field)
    {
        //a field conversion
        InputField ifield = incantation_field.GetComponentInChildren<InputField>();
        string itext = ifield.text;

        //int i = 0;
        for(int i=0; i< itext.Length; i++)
        {
            if(i!=0)
            if(itext[i]==itext[i-1] && Is_Consonant(itext[i]))
            {
                char[] array = itext.ToCharArray();
                array[i-1] = 'っ';
                itext = new string(array);
            }
        }

        ifield.text = itext;
    }

    //verific daca char-ul cons e o consoana,nu am pus q si v pt ca nu exista in limba japoneza
    bool Is_Consonant(char cons)
    {
        char[] consonants=new char[17]{'w','r','t','p','s','d','f','g','h','j','k','l','z','c','b','n','m'};

        for (int i = 0; i < consonants.Length; i++)
        {
            if (consonants[i] == cons)
            {
                return true;
                break;
            }
        }
        return false;
    }

    //Verificam daca text-ul din textbox e tot in hiragana (returneaza true)
    //Altfel returneaza false
    public bool Text_is_all_in_hiragana(InputField field_to_check)
    {
        string field_text = field_to_check.text;
        //daca exista un caracter non hiragana se va returna true,altfel se returneaza false
        //bool non_hiragana_character_exists;
        //numara cate caractere hiragana sunt in textbox
        //daca sunt mai putin de nr caracterelor dintextbox inseamna ca avem caractere care nu sunt hiragana
        int number_of_hiragana_characters_in_textbox = 0;

        for (int i = 0; i < field_text.Length; i++)
        {
            foreach (char hiragana in hiragana_characters)
            {
                if (field_text[i] == hiragana)
                {
                    number_of_hiragana_characters_in_textbox++;
                }
            }
        }
        //if (number_of_hiragana_characters_in_textbox == field_text.Length) return true;
        //else return false;
        //if (number_of_hiragana_characters_in_textbox == field_text.Length) Debug.Log("Text is all in hiragana true");
        //else Debug.Log("Text is all in hiragana false");
        if (number_of_hiragana_characters_in_textbox == field_text.Length) return true;
        else return false;
    }

    //scutur textbox-ul daca se apasa butonul cast dar in textbox am caractere care nu sunt hiragana
    public void Check_hiragana_textbox(GameObject textbox)
    {
        try
        {
            InputField textbox_field = textbox.GetComponentInChildren<InputField>();
            if (!Text_is_all_in_hiragana(textbox_field))
            {
                StartCoroutine(Shake_Gameobject(textbox)); Set_contains_only_hiragana(false);
            }
            else if(Text_is_all_in_hiragana(textbox_field)) Set_contains_only_hiragana(true);
        }
        catch
        {
            
        }
    }

    public IEnumerator Shake_Gameobject(GameObject object_to_shake)
    {
        Vector3 object_initial_position;
        object_initial_position = object_to_shake.transform.position;

        for (int i = 0; i <= 10;)
        {
            object_to_shake.transform.position = new Vector3(object_initial_position.x + 10, object_initial_position.y, object_initial_position.z);
            yield return new WaitForSeconds(0.1f);
            object_to_shake.transform.position = new Vector3(object_initial_position.x - 10, object_initial_position.y, object_initial_position.z);
            yield return new WaitForSeconds(0.1f);
            object_to_shake.transform.position = new Vector3(object_initial_position.x, object_initial_position.y, object_initial_position.z);
            i++;
        }
    }

    public bool Get_contains_only_hiragana()
    {
        return contains_only_hiragana;
    }

    public void Set_contains_only_hiragana(bool hiragana_status)
    {
        contains_only_hiragana = hiragana_status;
    }
}
