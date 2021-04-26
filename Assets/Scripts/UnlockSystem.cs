using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{

    [SerializeField] private GameObject doorObj = null;
    [SerializeField] private Color roomColor = Color.black;
    private List<Pedestal> pedestals = new List<Pedestal>();
    private List<Animator> flameAnims = new List<Animator>();
    private Animator unlockAnim;
    private Animator doorAnim;
    private BoxCollider2D doorCol;
    

    void Start()
    {
        unlockAnim = GetComponent<Animator>();
        flameAnims = getFlameAnimators();
        pedestals = getPedestals();
        doorAnim = doorObj.GetComponent<Animator>();
        doorCol = doorObj.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            unlockAnim.SetBool("BoxKeyActive", true);
            foreach (Pedestal pedestal in pedestals)
            {
                pedestal.changeCrystalColor();
            }
            foreach (Animator anim in flameAnims)
            {
                anim.SetBool("isHappy", true);
            }
            doorAnim.SetBool("Unlock", true);
            doorCol.enabled = false;
        }
    }

    List<Animator> getFlameAnimators()
    {
        List<Animator> anims = new List<Animator>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Flame"))
        {
            if (obj.transform.parent == transform.parent)
            {
                anims.Add(obj.GetComponent<Animator>());
            }
        }
        return anims;
    }

    List<Pedestal> getPedestals()
    {
        List<Pedestal> anims = new List<Pedestal>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pedestal"))
        {
            if (obj.transform.parent == transform.parent)
            {
                anims.Add(obj.GetComponent<Pedestal>());
            }
        }
        return anims;
    }



}
