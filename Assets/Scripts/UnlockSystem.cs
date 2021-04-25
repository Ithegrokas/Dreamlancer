using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{

    [SerializeField] private LayerMask unlockLayer;
    private Animator unlockAnim;

    private void Start() 
    {
        unlockAnim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Key")) 
        {
            col.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            unlockAnim.SetBool("BoxKeyActive", true);
        }
            

    }

}
