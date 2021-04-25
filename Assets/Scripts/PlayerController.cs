using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 movement;
    private Rigidbody2D playerRB;
    private SpriteRenderer playerSPR;

    private bool isFlipped;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSPR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");


        playerRB.velocity = movement * speed;

        if (movement.x < 0 && !playerSPR.flipX)
            playerSPR.flipX = true;
        
        else if (movement.x >=0 && playerSPR.flipX) 
            playerSPR.flipX = false;
        
            
    }
}
