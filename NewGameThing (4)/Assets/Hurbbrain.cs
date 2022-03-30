using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Hurbbrain : MonoBehaviour
{
   
    public GameObject maleTep;
    public int extrafood = 0;
    public float hunger = 0;
    public float thurst = 0;
    public string LastWant;
    public float thinkT = 0;
    public bool pregnet = false;
    float pregnetT = 0;
    bool notmoving = false;
    public Scrollbar Hbar;
    public Scrollbar Tbar;

    GameObject targit;
    public List<GameObject> see = new List<GameObject>();
    public List<GameObject> Rsee = new List<GameObject>();
    NavMeshAgent Nav;
    bool fix;
    
    // Start is called before the first frame update
    void Awake()
    {
       Nav = gameObject.GetComponent<NavMeshAgent>();
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(10, 10, 10);
        hunger = 0;
        thurst = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        hunger += Time.deltaTime *  2 / 60;
        thurst += Time.deltaTime * 1.5f / 60;
       
        if(hunger > 1 || thurst > 1 )
        {
            Destroy(gameObject);
        }
        if(extrafood >= 10)
        {
            pregnetT = 0;
            pregnet = true;
            extrafood = 0;
        }
        Hbar.size = hunger;
        Tbar.size = thurst;
       
        thinkT += Time.deltaTime;
        if(thinkT > 1)
        {
            Think();
            thinkT = 0;
        }
        if (pregnet)
        {
            UpdatePregnet();
        }
        
    }
    void UpdatePregnet()
    {
        pregnetT += Time.deltaTime;

        if(pregnetT > 10)
        {
            Birth();
            Birth();
            Birth();
            pregnetT = 0;
            pregnet = false;
        }
    }
    void Birth()
    {
        
        
        
      Instantiate(maleTep, transform.position, transform.rotation);
       
    }
    void OnTriggerEnter(Collider other)
    {
        see.Add(other.gameObject);
       
    }
    void OnTriggerExit(Collider other)
    {
        see.Remove(other.gameObject);
    }
    void Think()
    {
        Rsee.Clear();
        foreach(GameObject i in see)
        {
            if (i == null || i.Equals(null))
            {
                Rsee.Add(i);
            }
        }
        foreach (GameObject i in Rsee)
        {
            
            
            see.Remove(i);
            
        }
        if (Nav.velocity == new Vector3(0, 0, 0))
        {
            notmoving = true;
        }
        else
        {
            notmoving = false;
        }
        fix = false;
        if (hunger > thurst  && (LastWant != "food" || targit == null))
        {
            
            foreach (GameObject i in see)
            {
                if (i.tag == "food")
                {
                    Nav.SetDestination(i.transform.position);
                    LastWant = "food";
                    fix = true;
                    targit = i;
                    break;
                    
                }
            }
        }
        if (hunger < thurst && (LastWant != "water" || targit == null))
        {
            
            foreach (GameObject i in see)
            {
                if (i.tag == "water")
                {
                    Nav.SetDestination(i.transform.position);
                    LastWant = "water";
                    fix = true;
                    targit = i;
                    break;
                }
            }
        }
        
        if (notmoving  && targit == null)
        {
            Nav.destination = RandomNavSphere(transform.position, 25f, -1);
        }
        



    }
   
    // picks a random spot DO NOT MESS WITH
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void intract(Collider collision)
    {
        if (collision.gameObject == targit)
        {
            Debug.Log("uhoh");
            if (targit.tag == "food")
            {
                Destroy(collision.gameObject);
                hunger -= 0.35f;
                if (hunger < 0)
                {
                    hunger = 0;
                }
                extrafood++;
                targit = null;

            }
            else if (targit.tag == "water")
            {

                thurst = 0;
                targit = null;
            }
           
        }

    }


}

