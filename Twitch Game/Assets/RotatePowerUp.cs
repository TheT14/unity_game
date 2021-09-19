using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    public float rotateSpeed = 3;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed, rotateSpeed, rotateSpeed);
    }
}
