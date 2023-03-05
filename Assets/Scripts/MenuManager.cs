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
        SceneManager.LoadScene("Cutscene", LoadSceneMode.Single);
    }

    public void About()
    {
        SceneManager.LoadScene("About", LoadSceneMode.Single);
    }
}