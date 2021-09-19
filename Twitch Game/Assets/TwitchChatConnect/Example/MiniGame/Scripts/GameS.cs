using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TwitchChatConnect.Example.MiniGame
{
    public class GameS : MonoBehaviour
    {

        public GameObject PlayerText;
    
        public string playername;
        static bool scene_is_wait;
        public GameObject test;
        public GameObject Attack;
        public GameObject Basic;
        public GameObject Run;
        public GameObject Center;
        public GameObject PowerUp;
        public GameObject Heal;

        public static string user1test;
        private static string JOIN_COMMAND = "!join";
        private static string START_COMMAND = "!start";
        private static string MOVE_COMMAND = "!move";
        private static string ATTACK_COMMAND = "!attack";
        private static string RUN_COMMAND = "!run";
        private static string CENTER_COMMAND = "!center";
        private static string POWERUP_COMMAND = "!powerup";
        private static string HEAL_COMMAND = "!heal";

        public static Dictionary<string, TwitchUser > plzworkdude;
        private Dictionary<string, Vector3> directions;
        private Dictionary<TwitchUser, UserInfo> Users;
        private Dictionary < TwitchChatConnect.Data.TwitchUser, competitor> alive_player_list;

        void Start()
        {
            
           
            
            TwitchChatClient.instance.Init(() =>
                {
                    scene_is_wait = true;
                    plzworkdude = new Dictionary<string, TwitchUser>();
                    TwitchChatClient.instance.onChatMessageReceived += OnChatMessageReceived;
                    TwitchChatClient.instance.onChatCommandReceived += OnChatCommandReceived;
                    TwitchChatClient.instance.onChatRewardReceived += OnChatRewardReceived;

                    
                    
                },
                message =>
                {
                    // Error when initializing.
                    Debug.LogError(message);
                });
        }

        void OnChatCommandReceived(TwitchChatCommand chatCommand)
        {
            
            if(chatCommand.Command == JOIN_COMMAND)
            {
                string name = chatCommand.User.Username;
                plzworkdude[name] = chatCommand.User;

            }
            if (chatCommand.Command == ATTACK_COMMAND)
            {
                
                Attack.SetActive(true);
                Heal.SetActive(false);
                Run.SetActive(false);
                Basic.SetActive(false);
                PowerUp.SetActive(false);
                Center.SetActive(false);
            }
            if (chatCommand.Command == HEAL_COMMAND)
            {
                Attack.SetActive(false);
                Heal.SetActive(true);
                Run.SetActive(false);
                Basic.SetActive(false);
                PowerUp.SetActive(false);
                Center.SetActive(false);
            }
            if (chatCommand.Command == RUN_COMMAND)
            {
                Attack.SetActive(false);
                Heal.SetActive(false);
                Run.SetActive(true);
                Basic.SetActive(false);
                PowerUp.SetActive(false);
                Center.SetActive(false);
            }
            if (chatCommand.Command == POWERUP_COMMAND)
            {
                Attack.SetActive(false);
                Heal.SetActive(false);
                Run.SetActive(false);
                Basic.SetActive(false);
                PowerUp.SetActive(true);
                Center.SetActive(false);
            }
            if (chatCommand.Command == CENTER_COMMAND)
            {
                Attack.SetActive(false);
                Heal.SetActive(false);
                Run.SetActive(false);
                Basic.SetActive(false);
                PowerUp.SetActive(false);
                Center.SetActive(true);
            }
            

           
        }

        void OnChatRewardReceived(TwitchChatReward chatReward)
        {
        }

        void OnChatMessageReceived(TwitchChatMessage chatMessage)
        {
            
            playername = chatMessage.User.Username;
            if (chatMessage.User.Username == "mavrick760")
            {
                test.SetActive(true);
            }

        }

        

        bool Start5playergame;
        static float timer;
        bool end_game;
        public void Update()
        {

            if ("Begin Scene" == SceneManager.GetActiveScene().name)
            {
                timer += Time.deltaTime;

                if (timer >= 5)
                {
                    GameObject r = GameObject.Find("GameUImanager");

                    r.GetComponent<UImanager>().updateUserList(plzworkdude);



                }
            }
            if (Start5playergame && "5 player" == SceneManager.GetActiveScene().name)
            {
                Start5playergame = false;
                alive_player_list = new Dictionary<TwitchUser, competitor>();
                GameObject r = GameObject.Find("spawn_manager");
                r.GetComponent<spawnManager5>().spawn_players(plzworkdude, alive_player_list);
            }
            if ("5 player" == SceneManager.GetActiveScene().name && alive_player_list.Count <= 1)
            {
                timer += Time.deltaTime;

                if (timer <= 10)
                {
                    end_game = false;
                    SceneManager.LoadScene(0);
                    timer = 0;


                }
            }
        }

        public void startGame()
        {
            float Len = plzworkdude.Count;
         
            if(plzworkdude.Count <= 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Start5playergame = true;
                timer = 0;
            }
            else if(plzworkdude.Count >= 10)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
        public void playerdied(competitor deadplayer )
        {
            
            foreach (KeyValuePair<TwitchUser, competitor> thing in alive_player_list)
            {

                if(thing.Value == deadplayer)
                {
                    var deadplayerKey = thing.Key;
                    alive_player_list.Remove(deadplayerKey);
                    
                    break;
                }

            }
            
        }


    }


   

}