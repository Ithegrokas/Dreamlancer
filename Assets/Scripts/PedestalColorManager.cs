using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalColorManager : MonoBehaviour
{
    [SerializeField] private Color color  = Color.black;

    // Start is called before the first frame update
    void Start() => Pedestal.color = color;
}
