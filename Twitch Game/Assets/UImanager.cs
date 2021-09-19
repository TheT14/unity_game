using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using TwitchChatConnect.Example.MiniGame;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject PlayerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
        

           
        
    }

    public void updateUserList(Dictionary<string, TwitchUser> plzworkdude)
    {
        if (plzworkdude.Count == 0)
        {
        
        }    
        else
        {
            PlayerText.GetComponent<Text>().text = " ";
            foreach (KeyValuePair<string, TwitchUser> name in plzworkdude)
            {

                PlayerText.GetComponent<Text>().text += System.Environment.NewLine;
                PlayerText.GetComponent<Text>().text += name.Key;

            }
        }   
        
        
    }


    
    public void callGame()
    {
        GameObject r = GameObject.Find("Game");

        r.GetComponent<GameS>().startGame();
    }
}
