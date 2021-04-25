using System.Collections;
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
    [SerializeField] private GameObject onDeathVfx;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSPR = GetComponent<SpriteRenderer>();
        playerReset = GetComponent<ResetPosition>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            movement = Vector2.zero;
            movement.y = Input.GetAxis("Vertical");
            movement.x = Input.GetAxis("Horizontal");

            playerAnim.SetFloat("horizontalSpeed", Mathf.Abs(movement.x));
            playerAnim.SetFloat("verticalSpeed", movement.y);

            playerRB.velocity = movement * speed;

            if (movement.x < 0 && playerSPR.flipX)
                playerSPR.flipX = false;

            else if (movement.x > 0 && !playerSPR.flipX)
                playerSPR.flipX = true;
        }

    }

    public void enableInput() => inputEnabled = true;

    public IEnumerator Death() 
    {
        inputEnabled = false;
        playerRB.velocity = Vector2.zero;
        onDeathVfx.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerReset.resetPosition();
        onDeathVfx.SetActive(false);
        inputEnabled = true;
    }
}
