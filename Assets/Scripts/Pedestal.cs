using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer crystalRenderer = null;
    [SerializeField] private Light2D crystalLight = null;
    

    public void changeCrystalColor(Color color)
    {
        crystalRenderer.color = color;
        crystalLight.color = color;
    }
}
