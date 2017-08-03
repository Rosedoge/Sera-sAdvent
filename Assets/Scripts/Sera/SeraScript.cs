using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeraScript : MonoBehaviour {



    #region movement
    bool walking = false,flipped = false;
    bool jumping = false, jumpImpulsed = false;
    int forward = 1;

    Animator myAnimator;
    #endregion

    #region my Parts
    [SerializeField]
    GameObject gunPort;
    [SerializeField]
    GameObject bullet, shell;
    GameObject myGun;

#endregion


    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        myGun = transform.GetChild(1).gameObject;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            myGun.GetComponent<Animator>().SetBool("Shooting", true);
            InvokeRepeating("ShootBullet", 0f, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            myGun.GetComponent<Animator>().SetBool("Shooting", false);
            CancelInvoke();
        }
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            IsWalking = false;
        }

        //Jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsJumping = true;
        }


        
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * 2.0f * Time.deltaTime;
        if (IsJumping)
        {
            if (!jumpImpulsed)
            {
                jumpImpulsed = true;
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Contains("Floor"))
        {
            jumpImpulsed = false;
            IsJumping = false;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                IsWalking = true;

            }
        }
    }

    void FlipChildren(bool tf)
    {
        foreach(Transform child in transform)
        {
            if (child.name != "Main Camera")
            {
                child.gameObject.GetComponent<SpriteRenderer>().flipX = tf;
                if (child.name == "GunBlank")
                {
                    if (tf == true)
                        child.position = new Vector3(child.position.x - 0.5f, child.position.y, child.position.z);
                    else
                        child.position = new Vector3(child.position.x + 0.5f, child.position.y, child.position.z);
                }
            }
        }
    }


    public void ShootBullet()
    {
        Vector3 spawn = new Vector3(transform.position.x + 0.40f * MyFoward, transform.position.y - 0.15f, transform.position.z - 0.45f);
        GameObject bul = Instantiate(bullet, spawn, transform.rotation);
        GameObject eShell = Instantiate(bullet, spawn, transform.rotation);
        bul.transform.parent = null;
        eShell.transform.parent = null;
        if (flipped)
        {
            bul.GetComponent<SpriteRenderer>().flipX = true;
            eShell.GetComponent<SpriteRenderer>().flipX = true;
        }

        eShell.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1)*3, 1, 0), ForceMode.Impulse);
        bul.GetComponent<Rigidbody>().AddForce(new Vector3(1500 * MyFoward, Random.Range(-1,1),0));
        Destroy(bul, 3f);
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
    public bool IsJumping
    {
        get
        {
            return jumping;
        }

        set
        {
            jumping = value;
            IsWalking = false;
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
