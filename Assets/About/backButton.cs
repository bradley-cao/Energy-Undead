using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backButton : MonoBehaviour
{
    public Button back;
	void Start () {
		Button btn = back.GetComponent<Button>();
		btn.onClick.AddListener(BackToStart);
	}

	void BackToStart(){
		SceneManager.LoadScene("Start Menu");
	}
}
