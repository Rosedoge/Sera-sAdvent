using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpoScript : MonoBehaviour {
    SpriteRenderer mySprite;
	// Use this for initialization
	void Start () {
        mySprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //mySprite.material.SetFloat("_FlashAmount", amt);
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hello");
        if (col.gameObject.name.Contains("Boo"))
        {
            StartCoroutine(MyFunction(1, 0f));
            StartCoroutine(MyFunction(0, 0.25f));
        }
    }
    IEnumerator MyFunction(int amt, float delayTime)
    {
       
        yield return new WaitForSeconds(delayTime);
        // Now do your thing here
        mySprite.material.SetFloat("_FlashAmount", amt);
    }
}
