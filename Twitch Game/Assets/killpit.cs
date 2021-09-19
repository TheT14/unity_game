using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killpit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {

            
            other.gameObject.GetComponent<competitor>().elimiated_by_pit();




        }
    }
}