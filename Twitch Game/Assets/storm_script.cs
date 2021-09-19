using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storm_script : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            other.GetComponent<competitor>().in_storm = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            other.GetComponent<competitor>().in_storm = false;
        }
    }
    void awake()
    {
        
    }
    public double grace_period_len;
    bool grace_period_over = false;
    bool Shrink = false;
    public Vector3 end_zone_size;
    double timer = 0;
    Vector3 r = new Vector3(1, 1, 0);
    public double zone_speed;
    // Update is called once per frame
    void Update()
    {
        if(grace_period_over == false)
        {
         timer += Time.deltaTime;
         if(timer >= grace_period_len)
         {
            grace_period_over = true;
            Shrink = true;
            timer = 0;
         }
        
        }
        
        
        
        
        
        if (Shrink == true)
        {
            timer += Time.deltaTime;
            if(timer >= zone_speed)
            {
                timer = 0;
                if (transform.localScale != end_zone_size)
                {
                 transform.localScale -= r; //Consider using a lerp or something
                }
                else
                {
                 transform.localScale = end_zone_size;
                 Shrink = false;
                }
            }
            
  
        }
    }
}
