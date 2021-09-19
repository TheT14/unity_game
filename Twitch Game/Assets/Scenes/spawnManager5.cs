using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
public class spawnManager5 : MonoBehaviour
{
   public Transform spawn_1;
   public Transform spawn_2; 
   public Transform spawn_3;
   public Transform spawn_4;
   public Transform spawn_5;

   public GameObject player;
    competitor thing;
    
   int spawn; 
    public void spawn_players(Dictionary<string, TwitchUser > plzworkdude, Dictionary < TwitchChatConnect.Data.TwitchUser, competitor> alive_player_list )
    {
     
     var spawn_points = new List<Transform>()
        {
         spawn_1,
         spawn_2,
         spawn_3,
         spawn_4,             
         spawn_5,
        };
         spawn = 0;
        foreach (KeyValuePair<string, TwitchUser> user in plzworkdude)
        {

          thing = GameObject.Instantiate(player, spawn_points[spawn]).GetComponent<competitor>();
          spawn += 1;
          alive_player_list[user.Value] = thing;
          thing.changeName(user.Key);
        }
        
        
    }




}
