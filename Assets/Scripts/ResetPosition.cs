using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetPosition : MonoBehaviour
{
    public Transform originalPosition;
    
    public void resetPosition() 
    {
        gameObject.transform.position = originalPosition.position;
    }
}
