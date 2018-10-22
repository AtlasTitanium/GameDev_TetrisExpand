using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject ScoreText, PauzeMenu, GameOverMenu;
	public Button Resume;
	private int score = 0;

	void Start () {
		ScoreText.GetComponent<Text>().text = "Score : " + score;

		Button pauzeBtn = Resume.GetComponent<Button>();
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
		ScoreText.GetComponent<Text>().text = "Score : " + score;
	}

	private void StartGameOver(){
		ScoreText.SetActive(false);
		GameOverMenu.SetActive(true);
		score = 0;
		ScoreText.GetComponent<Text>().text = "Score : " + score;
	}

	public void PauzeGame(){
		ScoreText.SetActive(false);
		PauzeMenu.SetActive(true);
		EventManager.Pauze();
	}

	public void ResumeGame(){
		ScoreText.SetActive(true);
		PauzeMenu.SetActive(false);
		EventManager.Resume();
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		
	}
}
