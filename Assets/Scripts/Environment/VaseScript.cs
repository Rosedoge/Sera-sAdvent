using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaseScript : MonoBehaviour {

    [SerializeField]
    Sprite[] mySprites;
    [SerializeField]
    GameObject itemToDrop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BreakObject()
    {
        GetComponent<SpriteRenderer>().sprite = mySprites[1];
        GameObject itemDrop = Instantiate(itemToDrop.gameObject, transform.position,transform.rotation);
    }

    void OnTriggerEnter(Collider col)
    {
        //Hit By a bullet and flashes white
        if (col.gameObject.name.Contains("Boo"))
        {
            BreakObject();
        }
    }
}
