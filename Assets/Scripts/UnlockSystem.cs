using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class UnlockSystem : MonoBehaviour
{

    [SerializeField] private GameObject doorObj = null;
    [SerializeField] private Color roomColor = Color.black;
    private List<Pedestal> pedestals = new List<Pedestal>();
    private List<Animator> flameAnims = new List<Animator>();
    private List<Light2D> flameLights = new List<Light2D>();
    private List<SpriteRenderer> flameSprites = new List<SpriteRenderer>();
    private List<ParticleSystem> flameEmbers = new List<ParticleSystem>(); 
    private Animator unlockAnim;
    private Animator doorAnim;
    private BoxCollider2D doorCol;
    

    void Start()
    {
        unlockAnim = GetComponent<Animator>();
        pedestals = getPedestals();
        getFlamesComponents();

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Door"))
            if(obj.transform.parent == transform.parent)
                doorObj = obj;

        if(doorObj != null)
        {
            doorAnim = doorObj.GetComponent<Animator>();
            doorCol = doorObj.GetComponent<BoxCollider2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            unlockAnim.SetBool("BoxKeyActive", true);

            pedestals.ForEach(pedestal => pedestal.changeCrystalColor(roomColor));
            flameAnims.ForEach(anim => anim.SetBool("isHappy",true));
            flameSprites.ForEach(sprite => sprite.color = roomColor);
            flameLights.ForEach( light => light.color = roomColor);
            //add flame embers

            if(doorObj != null)
            {
                doorAnim.SetBool("Unlock", true);
                doorCol.enabled = false;
            }
            
        }
    }

    void getFlamesComponents()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Flame"))
        {
            if (obj.transform.parent == transform.parent)
            {
                flameAnims.Add(obj.GetComponent<Animator>());
                flameSprites.Add(obj.GetComponent<SpriteRenderer>());
                flameLights.Add(obj.GetComponent<Light2D>());
                flameEmbers.Add(obj.GetComponent<ParticleSystem>());
            }
        }
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
