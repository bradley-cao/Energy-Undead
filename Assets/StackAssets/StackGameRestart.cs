using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StackGameRestart : MonoBehaviour
{
    public Button restartButton;
	void Start () {
		Button btn = restartButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		SceneManager.LoadScene("Stack", LoadSceneMode.Single);
	}
}
