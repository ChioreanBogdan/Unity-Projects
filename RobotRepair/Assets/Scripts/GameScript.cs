using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

class GameScript : MonoBehaviour
{
        public GUISkin customSkin;
    public Texture blank_txt;

    //the number of columns in the card grid
    static int cols = 4;
    //the number of rows in the card grid
    static int rows = 4;
    //I think the answer is 16 but i was never good at math
    int totalCards = 0;
    //If there are 16 cards,the player needs 8 matches to claer the board
    int matchesNeededtoWin = 0;
    //At the outset,the payer has not made any matches
    int matchesMade = 0;
    //each card's width and height is 100 pixels
    int cardW = 100; int cardH = 100;
    //We'll store all the cards we create in this Array
    Card[] aCards=null;
    //We'll store the robots that appear in silhouette at the bottom 
    string[] aDefectiveRobots = null;
    //We'll store the number of robots in silhouette that have not been repaired yet
    string[] aDefectiveRobotsLeft = null;

    int credit_screens = 0;

    //This array will keep track of the shuffled dealt cards
    Card[,] aGrid=new Card[cols,rows];
    //This array will store the two cards that the player flips over
    List<Card> aCardsFlipped=new List<Card>();
    //we'll use this flag to prevent the player from clicking buttons when we don't want them to
    bool playerCanClick;
    //Store whether or not the player has won,should probably start out false :)
    bool playerHasWon = false;

    //bool ca sa verifice daca nr de secunde de la functia Wait_for_a_number_of_seconds
    bool seconds_have_passed = false;

    // Start is called before the first frame update
    void Start()
    {
        totalCards = cols * rows;
        matchesNeededtoWin = Convert.ToInt32(totalCards * 0.5);

        //We should let the player play,don't you think?
        playerCanClick = true;

        BuildDeck();

        //Initialize the aray asempty list
        for (int i= 0; i<rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int someNum = UnityEngine.Random.Range(0,aCards.Length);

                Card card = aCards[someNum];

                aGrid[i,j] = card;

                aCards = aCards.Where(e => e != card).ToArray();
                //Debug.Log("aGrid["+i+","+j+"]:" + aGrid[i,j]);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        BuildGrid();
        BuildGrid2();
        BuildCreditPrompt();
        if (playerHasWon)
            BuildWinPrompt();
        GUI.Button(new Rect(0, 0, 100, 20), new GUIContent("Click me!"));
        GUILayout.EndArea();
        //print("building grid!");
    }

    void BuildGrid()
    {
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        for (int i = 0; i < rows; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int j = 0; j < cols; j++)
            {
                Card card = aGrid[i,j];
                string img;

                if(card.Matched)
                {
                    img = "blank";
                }
                else
                if(card.FaceUp)
                {
                    img = card.img_address;
                }
                else
                {
                    img = "wrench";
                }

                var button_image = Resources.Load<Texture>(img);

                GUI.enabled = !card.Matched;
                if (GUILayout.Button(button_image,GUILayout.Width(cardW)) && playerCanClick)
                    {
                    //ca sa poti da click pe un buton doar cand nu e un credit screen pe ecran
                    if (credit_screens<=0) {
                        //if(playerCanClick)
                        FlipCardFaceUp(card);
                        Debug.Log(card.get_img());
                        Debug.Log("playerCanClick=" + playerCanClick);
                    }
                    }
                GUI.enabled = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    void BuildGrid2()
    {
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        //for (int i = 0; i < rows; i++)
        //{
        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //for (int j = 0; j < cols; j++)
        //{
        //    Card card = aGrid[i, j];
        //    string img;

        //    if (card.Matched)
        //    {
        //        img = "blank";
        //    }
        //    else
        //    if (card.FaceUp)
        //    {
        //        img = card.img_address;
        //    }
        //    else
        //    {
        //        img = "wrench";
        //    }

        for (int j = 0; j < aDefectiveRobots.Length; j++)
        {
            //Debug.Log("aDefectiveRobots[" + j + "]=" + aDefectiveRobots[j]);
            var label_image = Resources.Load<Texture>(aDefectiveRobots[j]);
            var blank_label = Resources.Load<Texture>("blank");

            GUILayout.BeginArea(new Rect(Screen.width / 2-j*102+100, Screen.height / 2 + 200, 100, 100));
            GUILayout.Label(label_image);
            GUILayout.EndArea();

            //GUILayout.BeginArea(new Rect(Screen.width / 2 - j * 102 + 100, Screen.height / 2 + 200, 50, 50));
            //GUILayout.Label(blank_label);
            //GUI.backgroundColor = Color.yellow;
            //GUILayout.TextField("!");
            //textstyle e folosit pentru literle care apar in stanga jos langa poza cu siluetele
            GUIStyle TextStyle=new GUIStyle();
            TextStyle.fontSize = 25;
            TextStyle.normal.textColor = Color.black;

            //for(int i= 0; i < aDefectiveRobotsLeft.Length; i++)
            //{
            //ca sa nu se afiseze "0"
            if(aDefectiveRobotsLeft[j].Last().ToString()!="0")
            GUI.TextField(new Rect(Screen.width / 2 - j * 102 + 185, Screen.height / 2 + 275, 50, 50), aDefectiveRobotsLeft[j].Last().ToString(), TextStyle);
            //}
            //GUILayout.EndArea();
        }

        //int h = 0;
        //foreach(string defective_robot in aDefectiveRobots)
        //{
        //    Debug.Log("Defective robot[" + h + "]=" + defective_robot);
        //    h++;
        //}

        //Debug.Log("screen.width=" + Screen.width);
        //var test_label_image = Resources.Load<Texture>(aDefectiveRobots[1]);
        //GUILayout.BeginArea(new Rect(Screen.width/2, Screen.height/2+200, 100, 100));
        //GUILayout.Label(test_label_image);
        //GUILayout.EndArea();

        //var button_image = Resources.Load<Texture>("blank");

        //GUI.enabled = !card.Matched;
        //if (GUILayout.Button(button_image, GUILayout.Width(cardW)))
        //{
        //    if (playerCanClick)
        //        FlipCardFaceUp(card);
        //    Debug.Log(card.get_img());
        //    Debug.Log("playerCanClick=" + playerCanClick);
        //}
        //GUI.enabled = true;
        //}
        //GUILayout.FlexibleSpace();
        //GUILayout.EndHorizontal();
        //}
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    void BuildDeck()
    {
        //we've got 4 robots to work with
        int totalRobots = 4;
        //this stores a reference to a card
        UnityEngine.Object card;
        int id = 0;

        int k = 0;
        List<Card> aCard_list = new List<Card>();
        List<string> aDefectiveRobots_list = new List<string>();
        List<string> aDefectiveRobotsLeft_list = new List<string>();
        for (int i=0; i<totalRobots; i++)
        {
            string[] aRobotParts = { "Head", "Arm", "Leg" };

            for (int j=0; j<2; j++)
            {
                int someNum = UnityEngine.Random.Range(0,aRobotParts.Length);
                string theMissingPart = aRobotParts[someNum];

                aRobotParts = aRobotParts.Where(e => e != theMissingPart).ToArray();

                Card new_card = new Card("robot"+(i+1)+"Missing"+theMissingPart,id);
                Card new_card2 = new Card("robot"+(i + 1)+theMissingPart,id);
               string aDefectiveRobots="robot" + (i + 1)+"Silhouette";
                //Stim ca sunt 2 roboti din fiecare tip
               string aDefectiveRobotsLeft = "Number of defective robot" + (i + 1)+" left:"+2;

                //aCard_list = new List<Card>();
                //List<Card> aCard_list =aCards.ToList<Card>();
                aCard_list.Add(new_card);
                aCard_list.Add(new_card2);

                aDefectiveRobots_list.Add(aDefectiveRobots);
                aDefectiveRobotsLeft_list.Add(aDefectiveRobotsLeft);
                id++;
            }
        }

        //ca sa nu avem roboti duplicati
        aDefectiveRobots_list = aDefectiveRobots_list.Distinct().ToList();
        aDefectiveRobots = aDefectiveRobots_list.ToArray<string>();

        aDefectiveRobotsLeft_list = aDefectiveRobotsLeft_list.Distinct().ToList();
        aDefectiveRobotsLeft = aDefectiveRobotsLeft_list.ToArray<string>();

        aCards = aCard_list.ToArray<Card>();
        foreach (Card elm in aCards)
        {
            Debug.Log("aCards[" + k + "]=" + elm.img_address);
            Debug.Log("aCards_id[" + k + "]=" + elm.Id);
            k++;
        }
        Debug.Log("aCards length=" + aCards.Length);
    }

    void BuildCreditPrompt()
    {
        //GUI.skin.box = customSkin.name;
        GUI.skin.button = customSkin.name;
        //g_style.fontSize = 20;
        //g_style.font.material.color = Color.red;
        //if (credit_screens <= 0)
        //{
        int winPromptW = 1000;
            int winPromptH = 900;

            float halfScreenW = Screen.width / 2;
            float halfScreenH = Screen.height / 2;

            int halfPromptW = winPromptW / 2;
            int halfPromptH = winPromptH / 2;

        //blank_txt.height = 2000;
        //blank_txt.width = 2000;

        GUI.BeginGroup(new Rect(halfScreenW - halfPromptW, halfScreenH - halfPromptH, winPromptW, winPromptH));
        GUI.Box(new Rect(0, 0, winPromptW, winPromptH), "Credits: \n -Me \n -Grandma", GUI.skin.GetStyle(customSkin.name));
        //GUI.Box(new Rect(0, 0, winPromptW, winPromptH), blank_txt);

        //GUIStyle
        //aici am ramas 26/11/20
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.blue;
        GUILayout.Button( "Label", style);

        GUIStyle g_style;


        if (GUI.Button(new Rect(200, 200, 100, 100), "Okay")) //, GUI.skin.GetStyle(customSkin.name)))
        {
            Debug.Log("Bump");
            //GUI.backgroundColor = Color.yellow;
        }
        //GUI.color = Color.red;
        //Application.LoadLevel("Title");
        GUI.EndGroup();
            if(credit_screens<=1)credit_screens++;
        //}
    }

    void BuildWinPrompt()
    {
        int winPromptW = 100;
        int winPromptH = 90;

        float halfScreenW = Screen.width / 2;
        float halfScreenH = Screen.height / 2;

        int halfPromptW = winPromptW / 2;
        int halfPromptH = winPromptH / 2;

        GUI.BeginGroup(new Rect(halfScreenW - halfPromptW, halfScreenH - halfPromptH, winPromptW, winPromptH));
        GUI.Box(new Rect(0, 0, winPromptW, winPromptH), "A Winner is You!");

        if (GUI.Button(new Rect(10, 40, 80, 20), "Play Again!")) Application.LoadLevel("Title");
        GUI.EndGroup();
    }

    void FlipCardFaceUp(Card card)
    {
        card.FaceUp = true;
        //aCardsFlipped_list = aCardsFlipped.ToList<Card>();
        Debug.Log("aCardsFlipped=" + aCardsFlipped);

        //daca aCardsFlipped nu contine ard IndexOf returneaza -1 (linia asta e pt a preveni dubluc click pe o carte)
        if (aCardsFlipped.IndexOf(card) < 0)
        {
            Debug.Log("aCardsFlipped.IndexOf("+card+")=" + aCardsFlipped.IndexOf(card));
            aCardsFlipped.Add(card);
            Debug.Log("Card robot flipped is:" + card.get_img());
            Debug.Log("Card robot id is:" + card.Id);

            int h = 0;
            foreach(string defective_robot in aDefectiveRobots )
            {
                Debug.Log("defective_robot[" + h + "]=" + defective_robot);
                h++;
            }

            int h1 = 0;
            foreach (string defective_robot_left in aDefectiveRobotsLeft)
            {
                Debug.Log("defective_robot_left[" + h1 + "]=" + defective_robot_left);
                h1++;
            }
            Debug.Log("Card pressed=" +card.get_img());
                Debug.Log("Card pressed id=" + card.Id);

            if (aCardsFlipped.Count == 2)
            {
                playerCanClick = false;

                StartCoroutine(Wait_for_a_number_of_seconds(1));

                if (aCardsFlipped[0].Id == aCardsFlipped[1].Id)
                {
                    //Match!
                    aCardsFlipped[0].Matched = true; aCardsFlipped[1].Matched = true;

                    //Inlocuim siluetele intunecate din pozele de jos cu robotul corespunzator
                    for (int i = 0; i < aDefectiveRobots.Length; i++)
                    {
                        //if(aDefectiveRobots[i].Length > 6) e ca sa nu imi dea eroare (ca sa nu se uite la aDefectiveRobots care nu au Silhouette in capat pt ca alea au length<=6 si da eroare)
                        if (aDefectiveRobots[i].Length > 6)
                        if ((aCardsFlipped[0].get_img().Remove(6) == aDefectiveRobots[i].Remove(6)))
                        {
                            if (aDefectiveRobotsLeft[i].EndsWith("2"))
                            {
                                aDefectiveRobotsLeft[i] = aDefectiveRobotsLeft[i].Remove(aDefectiveRobotsLeft[i].Length-1);
                                aDefectiveRobotsLeft[i] = aDefectiveRobotsLeft[i] + "1";
                            }
                            else if (aDefectiveRobotsLeft[i].EndsWith("1"))
                            {
                                Debug.Log("defective_robot=" + aDefectiveRobots[i].Remove(6));

                                aDefectiveRobotsLeft[i] = aDefectiveRobotsLeft[i].Remove(aDefectiveRobotsLeft[i].Length - 1);
                                aDefectiveRobotsLeft[i] = aDefectiveRobotsLeft[i] + "0";
                                //asta trebuie modificata sa se schimba numai cand amandoi robotii sunt reparati
                                aDefectiveRobots[i] = aDefectiveRobots[i].Remove(6);
                            }
                        }
                    }

                    aCardsFlipped = new List<Card>();

                    matchesMade++;
                    if (matchesMade >= matchesNeededtoWin) playerHasWon = true;
                }
                else
                    //Executes Flip_cards_back after 1 second
                    Invoke("Flip_cards_back", 1);

                //aCardsFlipped = new List<Card>();

                //astept o secunda inainte sa fac playerCanClick true din nou
                Invoke("Player_can_click_again", 1);
            }
        }
    }

    void Flip_cards_back()
    {
        if (aCardsFlipped.ElementAt(0) != null && aCardsFlipped.ElementAt(1) != null)
        {
            aCardsFlipped[0].FaceUp = false; aCardsFlipped[1].FaceUp = false;
            aCardsFlipped = new List<Card>();
        }
       // aCardsFlipped.RemoveAt(0); aCardsFlipped.RemoveAt(1);
    }

    void Player_can_click_again()
    {
        playerCanClick = true;
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator Wait_for_a_number_of_seconds(int nr_of_seconds_to_wait)
    {
        float counter = 0;
        while (counter < nr_of_seconds_to_wait)
        {
            yield return new WaitForSeconds(1);
            counter += 1;
        }

        seconds_have_passed = true;
        Debug.Log("Seconds have passed=" + seconds_have_passed);
        Debug.Log("Counter=" +counter);
    }

    IEnumerator Wait_for_1_second()
    {
            yield return new WaitForSeconds(1);
    }

    IEnumerator Do_last()
    {
        while (!seconds_have_passed)
            yield return new WaitForSeconds(0.1f);
        print("Do stuff.");
    }
}
