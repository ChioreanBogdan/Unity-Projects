using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aici stocam intr-un array toate Fused_Kanji-urile continute in folder-ul "Fused Kanji"
public class Fused_Kanji_Library : MonoBehaviour {

    public Fused_Kanji[] fused_kanji;

	// Use this for initialization
	void Start () {
		
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    public Fused_Kanji[] Return_fused_kanji()
    {
            return fused_kanji;
    }
}
