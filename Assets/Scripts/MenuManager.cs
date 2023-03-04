using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject AboutButton;
    
    void Start()
    {
        StartButton.SetActive(true);
        AboutButton.SetActive(true);
        StartButton.GetComponent<Button>().onClick.AddListener(() => StartGame());
        AboutButton.GetComponent<Button>().onClick.AddListener(() => About());
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void About()
    {
        StartButton.SetActive(false);
    }

    public void Quit()
    {
        StartButton.SetActive(false);
    }
    
}