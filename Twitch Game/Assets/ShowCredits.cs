using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    public GameObject diySprite;
    public GameObject test;
    public GameObject WelcomeText;
    public GameObject JoinText;
    public GameObject CommandsText;
    public GameObject QueueText;
    public GameObject StartButton;
    public GameObject CreditsButton;
    public GameObject QuitButton;
    public GameObject BackButton;
    public GameObject CreditsText;
    
   public void RevealCredits()
    {
        diySprite.SetActive(false);
        test.SetActive(true);
        WelcomeText.SetActive(false);
        JoinText.SetActive(false);
        CommandsText.SetActive(false);
        QueueText.SetActive(false);
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        QuitButton.SetActive(false);
        BackButton.SetActive(true);
        CreditsText.SetActive(true);
    }
}
