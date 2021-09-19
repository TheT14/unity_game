using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back2menu : MonoBehaviour
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

    public void RevealMainM()
    {
        diySprite.SetActive(true);
        test.SetActive(false);
        WelcomeText.SetActive(true);
        JoinText.SetActive(true);
        CommandsText.SetActive(true);
        QueueText.SetActive(true);
        StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        QuitButton.SetActive(true);
        BackButton.SetActive(false);
        CreditsText.SetActive(false);
    }
}