using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stats-uri care se pasteaza si dupa lupta
public class Stats : MonoBehaviour {

    public int Max_health, Current_Health;
    public bool poisoned;

    public void Set_Current_Health(int new_health)
     {
       if(new_health>=0) Current_Health = new_health;
       else Current_Health = 0;
    }
}
