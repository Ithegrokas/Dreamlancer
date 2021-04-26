﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 movement;
    private Rigidbody2D playerRB;
    private SpriteRenderer playerSPR;
    private bool inputEnabled = true;
    private ResetPosition playerReset;
    private Animator playerAnim;
    private BoxCollider2D[] playerCols;
    [SerializeField] private GameObject onDeathVfx = null;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSPR = GetComponent<SpriteRenderer>();
        playerReset = GetComponent<ResetPosition>();
        playerAnim = GetComponent<Animator>();
        playerCols = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        if (inputEnabled)
        {
            movement.y = Input.GetAxis("Vertical");
            movement.x = Input.GetAxis("Horizontal");

            playerRB.velocity = movement * speed;

            if (movement.x < 0 && playerSPR.flipX)
                playerSPR.flipX = false;

            else if (movement.x > 0 && !playerSPR.flipX)
                playerSPR.flipX = true;
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }
        playerAnim.SetFloat("horizontalSpeed", Mathf.Abs(movement.x));
        playerAnim.SetFloat("verticalSpeed", movement.y);

    }

    public void enableInput() => inputEnabled = true;

    public void disableInput() => inputEnabled = false;

    void Dying()
    {
        inputEnabled = false;
        onDeathVfx.SetActive(true);
        foreach (BoxCollider2D col in playerCols)
        {
            col.enabled = false;
        }
    }

    void Respawn()
    {
        playerReset.resetPosition();
        onDeathVfx.SetActive(false);
        inputEnabled = true;
        foreach (BoxCollider2D col in playerCols)
        {
            col.enabled = true;
        }
    }

    public IEnumerator Death()
    {
        Dying();
        yield return new WaitForSeconds(2f);
        Respawn();
    }
}
