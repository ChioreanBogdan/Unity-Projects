using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : System.Object
{
    bool isFaceUp = false;
    bool isMatched = false;
    string img;
    int id;

    public Card(string new_img,int id)
    {

        this.img = new_img;
        this.id = id;
    }

    public string get_img()
    {
        return img;
    }

    public bool get_is_face_up()
    {
        return isFaceUp;
    }

    public string img_address
    {
        get { return img; }
        set { img = value; }
    }

    public bool FaceUp 
    {
        get { return isFaceUp; }
        set { isFaceUp = value; }
    }

    public bool Matched
    {
        get { return isMatched; }
        set { isMatched = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
}
