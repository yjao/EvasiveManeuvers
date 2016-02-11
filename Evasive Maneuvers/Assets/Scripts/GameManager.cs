﻿using UnityEngine;
using System.Collections;


/* MonoBehaviour basically means it inherits from Unity's special class (GameObject)
 * with it you can access functions and variables within Unity's scenes, and all GameObjects inside it.
 * Nobody really knows what it is, and we don't have to know :) 
        
    */
public class GameManager : MonoBehaviour
{

    //public variables
    public GameObject projectile;
    public GameObject formation;

    public int bulletBillMoveSpeed;

    //private variables
    private GameObject player;
    private Vector2 direction; 

    // Awake is called when this script is first activated, kind of like the Init()
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Use this for initialization, it happens after Awake()
	void Start()
    {
	    
	}
	
    /* Update is called once per frame,
     * there are different kinds of Update functions (within the MonoBehaviour class),
     * such as FixedUpdate() for physics calculations, and LateUpdate() after everything's done.
     * For now, we can just stick with Update(), and change our needs as we go.
     * Be careful, Update() is called often, so it slows down the game with too much in there. */
	void Update()
    {

    }

    public void FireProjectile()
    {
        //find mouse position and translate that into a Vector with 3 floats (x,y,z)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Quaternion is just a fancy thing to say a Vector with a direction. 
        //Gets the rotation for the object (projectile) to point at
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePos - player.transform.position);
        
        //Create the object as a GameObject so we can still change values of it  Quarternion.identity
        GameObject firedProjectile = Instantiate(projectile, player.transform.position, rotation ) as GameObject;
        
        //Get the vector of the direction projectil is going to go in
		direction = mousePos - player.transform.position;

        //Normalize it (make it into units of 1's)
		direction.Normalize();

        //Move the projectile in front of the player object so it doesn't just create it on top of the player
        //trust me, it looks wonky as hell. if you do'nt believe me, get rid of the *2's in the next line and then try it
        firedProjectile.transform.position += new Vector3 (direction.x*2, direction.y*2, 0);

        //increase the projectile velocity so it actually, you know, projectiles
        firedProjectile.GetComponent<Rigidbody2D>().velocity = direction * bulletBillMoveSpeed;

        //logistical thing, set the parent of the object to the GameManager so we do'nt flood the hierarchy screen
        firedProjectile.transform.parent = transform;
        
    }
}
