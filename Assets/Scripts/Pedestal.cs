using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer crystalRenderer = null;

    public static Color color;

    public void changeCrystalColor()
    {
        crystalRenderer.color = color;
    }
}
