using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpoScript : MonoBehaviour, DamageableEntity {
    SpriteRenderer mySprite;
    Animator myAnim;
    int forward = -1;
    bool Attacking = false;

    int health = 50;
    Rigidbody myRigid;
	// Use this for initialization
	void Start () {
        mySprite = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {


        if(health <= 0)
        {


            Destroy(gameObject);
        }



        //animation
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.right * MyFoward, out hitinfo, 1f))
        {
            if (hitinfo.collider.gameObject.name.Contains("Sera") && Attacking == false)
            {
                myAnim.SetBool("Attacking", true);
                Attacking = true;
            }
        }   
        else
        {
            myAnim.SetBool("Attacking", false);
            Attacking = false;
        }

        
	}
    //mySprite.material.SetFloat("_FlashAmount", amt);
    void OnTriggerEnter(Collider col)
    {


        //Hit By a bullet and flashes white
        if (col.gameObject.name.Contains("Boo"))
        {
            StartCoroutine(Flash(1, 0f));
            StartCoroutine(Flash(0, 0.25f));
            DamageableEntity scorpo = (DamageableEntity)GetComponent<ScorpoScript>();
            scorpo.GetDamaged(gameObject);

        }
    }
    IEnumerator Flash(int amt, float delayTime)
    {
       
        yield return new WaitForSeconds(delayTime);
        // Now do your thing here
        mySprite.material.SetFloat("_FlashAmount", amt);
    }


    void DamageableEntity.GetDamaged(GameObject whatHitMe)
    {

        //reduce health by an amount
        health -= 10;
        //knock back the enemy
        //if (whatHitMe.transform.position.x < transform.position.x)
        //{
        //    myRigid.AddForce(new Vector3(1, 1, 0) * 75);
        //}
        //else
        //{
        //    myRigid.AddForce(new Vector3(-1, 1, 0) * 75);
        //}

    }

    public void Attack()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.right * MyFoward, out hitinfo,1f, 1<<LayerMask.NameToLayer("Player")))
        {
            DamageableEntity player = (DamageableEntity)hitinfo.collider.gameObject.GetComponent<SeraScript>();
            player.GetDamaged(hitinfo.collider.gameObject);
            Debug.Log("Here");
        }
    }


    int MyFoward
    {
        get
        {
            return forward;
        }
        set
        {
            forward = value;
        }
    }

}
