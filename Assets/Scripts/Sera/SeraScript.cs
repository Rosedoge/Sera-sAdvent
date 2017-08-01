using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeraScript : MonoBehaviour {



    #region movement
    bool walking = false,flipped = false;
    int forward = 1;
    Animator myAnimator;
    #endregion


    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Inpot();

        if (MyFoward > 0 && flipped)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            flipped = false;
            FlipChildren(false);
        }
        else if (MyFoward < 0 && !flipped)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            flipped = true;
            FlipChildren(true);
        }
    }



    void Inpot()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            IsWalking = true;
            if (Input.GetKeyDown(KeyCode.A))
            {
                MyFoward = -1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MyFoward = 1;
            }


        }
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            IsWalking = false;
        }

    }

    void FlipChildren(bool tf)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().flipX = tf;
            if(child.name == "GunBlank")
            {
                if(tf == true)
                    child.position = new Vector3 (child.position.x - 0.5f, child.position.y, child.position.z);
                else
                    child.position = new Vector3(child.position.x + 0.5f, child.position.y, child.position.z);
            }
        }
    }


#region GetSet
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
    bool IsWalking
    {
        get
        {
            return walking;
        }

        set
        {
            walking = value;
            if (value)
            {
                myAnimator.SetBool("Walking", true);
            }
            else
            {
                myAnimator.SetBool("Walking", false);
            }
        }


    }

#endregion
}
