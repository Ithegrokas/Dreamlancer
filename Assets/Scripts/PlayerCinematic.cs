using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematic : MonoBehaviour
{
    [SerializeField] private Transform firstCheckpoint = null;
    [SerializeField] private GameObject cinematicVfx = null;
    [SerializeField] private float speed = 2f;
    private BoxCollider2D[] playerCols;

    // Start is called before the first frame update
    void Start()
    {
        playerCols = GetComponents<BoxCollider2D>();
    }

    public IEnumerator playCinematic()
    {
        yield return new WaitForSeconds(1f);

        foreach(BoxCollider2D col in playerCols)
        {
            if(!col.isTrigger)
                col.enabled = false;
        }      
            
        while (transform.position != firstCheckpoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, firstCheckpoint.position, speed * Time.deltaTime);
            
            yield return null;
        }

        cinematicVfx.SetActive(false);

        foreach(BoxCollider2D col in playerCols)
        {
            if(!col.isTrigger)
                col.enabled = true;
        }

        GetComponent<Animator>().SetBool("cinematicEnded", true);
        //GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);

        GetComponent<PlayerController>().enableInput();
        
    }

}
