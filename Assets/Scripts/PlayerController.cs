using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform currentCheckpointTransform;
    private Vector2 movement;
    private Rigidbody2D playerRB;
    private Rigidbody2D boxRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        
        playerRB.velocity = movement * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Floor") && !col.gameObject.CompareTag("Box"))
            gameObject.GetComponent<ResetPosition>().resetPosition();

        if(col.gameObject.CompareTag("Box")){
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(playerRB.velocity);
        }
    }
}
