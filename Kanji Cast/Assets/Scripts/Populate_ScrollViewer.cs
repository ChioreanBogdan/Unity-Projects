using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script pt a popula dinamic un GridView
public class Populate_ScrollViewer : MonoBehaviour {

    public GameObject prefab; //This is our prefab object that will be exposed in the inspector
    //Prefabu poate fi orice(butoane,imagini,etc)

    public int numberToCreate; //Number of objects to create,exposed in inspector

	// Use this for initialization
	void Start () {
        Populate();
	              }
	
	// Update is called once per frame
	void Update () {
		
	               }

    void Populate()
    {
        GameObject newObj; //Create GameObject instance

        for (int i=0; i<numberToCreate; i++)
        {
            //transform e obiectul parinte-nu prea inteleg :/
            newObj = (GameObject)Instantiate(prefab, transform); //Create new intances of prefab until we've created as many as we specified
            newObj.GetComponent<Image>().color = Random.ColorHSV(); //Randomize te color of generated image 
        }
    }
                                                    }
