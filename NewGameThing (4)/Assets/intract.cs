using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intract : MonoBehaviour
{
    public Hurbbrain brain;

    
    void OnTriggerEnter(Collider other)
    {
        brain.intract(other);
    }
}
