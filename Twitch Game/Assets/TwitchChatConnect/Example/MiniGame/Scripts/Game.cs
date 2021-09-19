using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TwitchChatConnect.Example.MiniGame
{
    public class Game : MonoBehaviour
    {

        public GameObject PlayerText;
    
        public string playername;
        bool scene_is_wait;
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

        public Dictionary<string, TwitchUser > plzworkdude;
        private Dictionary<string, Vector3> directions;
        private Dictionary<TwitchUser, UserInfo> Users;
       

        void Start()
        {
            
           
            
            TwitchChatClient.instance.Init(() =>
                {
                    scene_is_wait = true;
                    plzworkdude = new Dictionary<string, TwitchUser>();
                    TwitchChatClient.instance.onChatMessageReceived += OnChatMessageReceived;
                    TwitchChatClient.instance.onChatCommandReceived += OnChatCommandReceived;
                    TwitchChatClient.instance.onChatRewardReceived += OnChatRewardReceived;

                    MatchManager.instance.onMatchEnd += OnMatchEnd;
                    MatchManager.instance.onMatchBegin += OnMatchBegin;
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
            

            if (!MatchManager.instance.HasStarted) return;

           

            Debug.Log($"Unknown Command received: {chatCommand.Command}");
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

        void OnMatchBegin()
        {
           
        }

        void OnMatchEnd(float secondsElapsed)
        {
            TwitchChatClient.instance.SendChatMessage("---------------");
            TwitchChatClient.instance.SendChatMessage($"The game has ended, it took {secondsElapsed} seconds.");
            foreach (KeyValuePair<TwitchUser,UserInfo> user in GameUI.instance.Users)
            {
                TwitchChatClient.instance.SendChatMessage(user.Value.GetText());
            }
            TwitchChatClient.instance.SendChatMessage("---------------");
            GameUI.instance.Reset();
        }


        float timer;

        private void Update()
        {
            if (scene_is_wait)
            {
                timer += Time.deltaTime;

                if (timer >= 5)
                {
                    GameObject r = GameObject.Find("GameUImanager");

                    r.GetComponent<UImanager>().updateUserList(plzworkdude);

                }

            }
        }


        public void startGame()
        {
            float Len = plzworkdude.Count;
            
        }


    }

  


}