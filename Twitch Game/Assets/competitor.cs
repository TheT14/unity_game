using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using UnityEngine.UI;
using TwitchChatConnect.Example.MiniGame;
//hi

public class competitor : MonoBehaviour
{
    public float damageDecrease = 25;
    float hp_regen_timer;
    private float hp = 100;
    public bool Dashing;
    public Scrollbar healthBar;
    public BoxCollider Hitbox;
    public bool testnamechange;
    public float sight_range;
    public GameObject mytext;
    public GameObject mykills;
    public float kills;
    public GameObject raycaster;
    public GameObject extra_raycaster1;
    public GameObject extra_raycaster2;
    public bool testMovement;
    private bool stoped;
    public Transform goal;
    public string my_name;   
    public bool follower;
    public bool in_storm;
    float damage = 25;
    float defense = 0;
    float Regen_amount = 1;
    bool extra_raycasts = true;
        void awake ()
    { /// code that runs on start of the progome Needs to be replaced with on awake
       setRandomloop();
        
        healthBar.size = hp/100;
    }
    
    public void changeName( string Name)
    { /// sets the name of the player and chages the UI
        my_name = Name;
        mytext.GetComponent<text>().text_Change(my_name);
    }
    void changekills()
    { /// sets the name of the player and chages the UI
        string s = string.Format("{0:G}", kills);
        mykills.GetComponent<text>().text_Change(s);
    }

    bool swich_to_nav_when_stoped;
 

 float rigidbody_cooldown = 0;
 float destination_cooldown = 0;
void Update()
    {   /// runs update code for the nav mesh state or the rigidbody state
        if (GetComponent<NavMeshAgent>().enabled)
        {
            Updatee_Navmesh();
        }
        else
        {
            Updatee_rigidbody();
        }
        
        hp_regen_timer += Time.deltaTime;
        if(hp_regen_timer >= 1  && in_storm == true)
        {
          hp -= 3;
          hp_regen_timer = 0;
        }
        else if(hp_regen_timer >= 2 && hp <= 99)
        {
            hp += Regen_amount;
            hp_regen_timer = 0;
        }

        healthBar.size = hp / 100;
        if(hp <= 0)
        {
            elimiate_self();
        }

    }
bool start_nav_mesh_cowntdown;
void Updatee_rigidbody()
    { /// ridibody state update code
        if (swich_to_nav_when_stoped)
        {
            if (stoped)
            {
                swich_to_nav();
                swich_to_nav_when_stoped = false;
            }
        }
        /// checks if the rigid body is stopped
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 1)
        {
            stoped = true;
            Hitbox.enabled = false;
        }
        else
        {
            stoped = false;
        }
        /// times the gap inbetwen going back to nav mesh
        if (start_nav_mesh_cowntdown)
        {
            rigidbody_cooldown += Time.deltaTime;
        }
        if (rigidbody_cooldown >= 4)
        {
            swich_to_nav_when_stoped = true;
            start_nav_mesh_cowntdown = false;
            rigidbody_cooldown = 0;
            Hitbox.enabled = false;
            Dashing = false;
        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude <=2)
        {
            Hitbox.enabled = false;
        }

    }
    bool start_new_destinashon_cowntdown;
    void Updatee_Navmesh()
    {
        /// helps with conting the time from the last destination change
        destination_cooldown += Time.deltaTime;

        /// chages the targit after a amont of deltaTime
        if(in_storm && destination_cooldown >= 3)
        {
            go_to_00();
        }
        if (destination_cooldown >= 3)
        {
            testMovement = false;
            setRandomloop();
        }
        if (start_new_destinashon_cowntdown)
        {
            rigidbody_cooldown += Time.deltaTime;
        }
        if (destination_cooldown >= 3)
        {
            setRandomloop();
            start_new_destinashon_cowntdown = false;
            destination_cooldown = 0;
            
        }
        /// do i need to write what this is :/
        raycast();

    }
    void dash (GameObject other , Collider other2 ) 
    {
        // picks a random ofset of trasform.foward and dashes at it
        
        swich_to_rigidbody();
        
        System.Random rnd = new System.Random();
        int bruh = rnd.Next(-20, 20);
        int bruh2 = rnd.Next(-20, 20);
        Vector3 bad = Quaternion.Euler(bruh, 1, bruh2) * other.transform.forward;
        gameObject.GetComponent<Rigidbody>().AddForce( bad * 30f, ForceMode.Impulse );
        Hitbox.enabled = true;
        start_nav_mesh_cowntdown = true;
        Dashing = true;
    }

    void swich_to_nav()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    void swich_to_rigidbody()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    // it looks for a object and if its tag is player it calls dash 
    private RaycastHit hit;
    void raycast()
    {
     if (Physics.Raycast(raycaster.transform.position, raycaster.transform.forward, out hit, 5f))
     {
        if(hit.collider.tag == "Player")
        {
            dash(raycaster, hit.collider);
        }
     }
     if(extra_raycasts && Physics.Raycast(extra_raycaster1.transform.position, extra_raycaster1.transform.forward, out hit, 5f))
     {
        Vector3 anana = (extra_raycaster1.transform.forward).normalized * 5f;
            
        Debug.DrawRay(extra_raycaster1.transform.position, anana, Color.red);
        
        if(hit.collider.tag == "Player")
        {
            
            dash(extra_raycaster1, hit.collider);
        }
     }
     if(extra_raycasts && Physics.Raycast(extra_raycaster2.transform.position, extra_raycaster2.transform.forward, out hit, 5f))
     {
        Vector3 nana = (extra_raycaster2.transform.forward).normalized * 5f;
            
        Debug.DrawRay(extra_raycaster2.transform.position, nana, Color.red);
        
        if(hit.collider.tag == "Player")
        {
            dash(extra_raycaster2, hit.collider);
        }
     }
     Vector3 banana = (raycaster.transform.forward).normalized * 5f;
            
     Debug.DrawRay(raycaster.transform.position, banana, Color.red);
  
    }

    
    // sets a random destination
 public void setRandomloop()
    {
    GetComponent<NavMeshAgent>().destination = RandomNavSphere (transform.position, 15f, -1);

    start_new_destinashon_cowntdown = true;
   }   
   // picks a random spot DO NOT MESS WITH
    public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
     Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
           
     randomDirection += origin;
           
     NavMeshHit navHit;
           
     NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);
           
     return navHit.position;
    }
        
private void OnTriggerEnter(Collider other)
{
    if(other.tag == "Player")
    {
        other.GetComponent<NavMeshAgent>().enabled = false;
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.GetComponent<competitor>().start_nav_mesh_cowntdown = true;
        
    }
}
private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" && other.collider.GetComponent<competitor>().Dashing == true)
        {
           
            hp -= damage;
            hp += defense;
            healthBar.size = hp / 100;
            if(hp <= 0)
            {
                elimiated_by_player(other);
            }
   
     
              
        }
    }

    void elimiated_by_player(Collision other)
    {
        other.collider.GetComponent<competitor>().hp += 25;
        other.collider.GetComponent<competitor>().kills += 1;
        other.collider.GetComponent<competitor>().changekills();

       elimiate_self();
    }

    void attack()
    {

    }


    public void elimiated_by_pit()
    {
        elimiate_self();
    }
    public void go_to_00()
    {
      GameObject r = GameObject.Find("center");  
      GetComponent<NavMeshAgent>().destination = r.transform.position;
      destination_cooldown = -3; 
    }

    void elimiate_self()
    {
       Destroy(gameObject);
       GameObject r = GameObject.Find("Game");

       r.GetComponent<GameS>().playerdied(gameObject.GetComponent<competitor>());
        
    
    }



}   

