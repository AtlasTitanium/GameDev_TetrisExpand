using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public GameObject scoreText, pauzeMenu, gameOverMenu, resumeButton;
	private int score = 0;


	void Start () {
		scoreText.GetComponent<Text>().text = "Score : " + score;

		Button pauzeBtn = resumeButton.GetComponent<Button>();
        pauzeBtn.onClick.AddListener(ResumeGame);
	}
	

	void OnEnable(){
		EventManager.GotTetris += IncreaseScore;
		EventManager.LostGame += StartGameOver;
	}


	void OnDisable(){
		EventManager.GotTetris -= IncreaseScore;
		EventManager.LostGame -= StartGameOver;
	}


	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauzeGame();
		}
	}


	private void IncreaseScore(){
		score += 100;
		scoreText.GetComponent<Text>().text = "Score : " + score;
	}


	private void StartGameOver(){
		scoreText.SetActive(false);
		gameOverMenu.SetActive(true);
		score = 0;
		scoreText.GetComponent<Text>().text = "Score : " + score;
	}


	public void PauzeGame(){
		scoreText.SetActive(false);
		pauzeMenu.SetActive(true);
		EventManager.Pauze();
	}


	public void ResumeGame(){
		scoreText.SetActive(true);
		pauzeMenu.SetActive(false);
		EventManager.Resume();
	}


	public void QuitGame(){
		Application.Quit();
	}


	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		
	}
}
