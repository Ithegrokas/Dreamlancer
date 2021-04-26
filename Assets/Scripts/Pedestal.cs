using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer crystalRenderer = null;

    public void changeCrystalColor(Color color)
    {
        crystalRenderer.color = color;
    }
}
