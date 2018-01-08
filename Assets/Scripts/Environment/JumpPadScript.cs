using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.forward, 1f);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Sera"))
        {
            EjectSera(col.gameObject);
        }
    }

    void EjectSera(GameObject Sera)
    {
        Sera.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Sera.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        Sera.GetComponent<SeraScript>().IsJumping = true;
    }
}
