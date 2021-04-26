using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFlowerTrigger : MonoBehaviour
{
    private List<Animator> flowerAnims;

    void Start()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Flower"))
            flowerAnims.Add(obj.GetComponent<Animator>());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            flowerAnims.ForEach(anim => anim.SetBool("startGrowing",true));
    }
}
