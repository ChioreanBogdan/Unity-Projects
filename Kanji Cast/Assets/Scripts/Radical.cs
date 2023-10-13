using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Radical", menuName = "Radical")]
public class Radical : ScriptableObject
{

    public int id_number;
    public string symbol, english_name, description;
    public string radical_name;
    public Sprite icon;

}

