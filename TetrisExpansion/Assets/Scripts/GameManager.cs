using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject roof, rightwall, leftwall, backwall, ground, light;
	public GameObject[] tetrisBlockPrefabs;
	private GameObject currentBlock;

	//Singleton
	public static GameManager instance = null;

	void Start(){
		//Instantiate Singleton
        if (instance == null) instance = this;
    	else if (instance != this) Destroy(gameObject);  
		CreateBlock();
	}


	void OnEnable(){
		EventManager.GotTetris += Sirtet;
		EventManager.LostGame += GameOver;
		EventManager.BlockDrop += CreateBlock;
		EventManager.IfPauze += Pauze;
		EventManager.IfResume += Resume;
	}


	void OnDisable(){
		EventManager.GotTetris -= Sirtet;
		EventManager.LostGame -= GameOver;
		EventManager.BlockDrop -= CreateBlock;
		EventManager.IfPauze -= Pauze;
		EventManager.IfResume -= Resume;	
	}


	void Update(){
		if(currentBlock != null){
			if(!currentBlock.GetComponent<FormedBlock>().enabled){
				PlaceBlock();
			}
		}
	}

	
	void CreateBlock(){
		//this creates the block
		int randomBlock = Random.RandomRange(0,tetrisBlockPrefabs.Length);
		if(roof.transform.position.x % 2 != 0){
			currentBlock = Instantiate(tetrisBlockPrefabs[randomBlock], new Vector3(roof.transform.position.x + 1, roof.transform.position.y, roof.transform.position.z), roof.transform.rotation);
		} else{
			currentBlock = Instantiate(tetrisBlockPrefabs[randomBlock], roof.transform.position, roof.transform.rotation);
		}
	}


	public void PlaceBlock(){
		//this places the block
		currentBlock = null;
		EventManager.Checker();
	}


	public void Sirtet() {
		//if Tetris complete
		int multiplier = 1;

		rightwall.transform.position = new Vector2(rightwall.transform.position.x + multiplier*2, rightwall.transform.position.y + multiplier);
		leftwall.transform.position = new Vector2(leftwall.transform.position.x, leftwall.transform.position.y + multiplier);
		backwall.transform.position = new Vector3(backwall.transform.position.x + multiplier, backwall.transform.position.y + multiplier, backwall.transform.position.z);
		ground.transform.position = new Vector2(ground.transform.position.x + multiplier, ground.transform.position.y);

		rightwall.transform.localScale = new Vector3(rightwall.transform.localScale.x + multiplier*2, rightwall.transform.localScale.y,rightwall.transform.localScale.z);
		leftwall.transform.localScale = new Vector3(leftwall.transform.localScale.x + multiplier*2, leftwall.transform.localScale.y,leftwall.transform.localScale.z);
		backwall.transform.localScale = new Vector3(backwall.transform.localScale.x + multiplier*2, backwall.transform.localScale.y + multiplier*2,backwall.transform.localScale.z);
		ground.transform.localScale = new Vector3(ground.transform.localScale.x + multiplier*2, ground.transform.localScale.y,ground.transform.localScale.z);

		roof.transform.position = new Vector2(roof.transform.position.x + multiplier, roof.transform.position.y + multiplier*2);
		roof.transform.localScale = new Vector3(roof.transform.localScale.x + multiplier*2, roof.transform.localScale.y,roof.transform.localScale.z);

		transform.position = new Vector3(transform.position.x + multiplier, transform.position.y + multiplier, transform.position.z - multiplier*2);
		light.transform.position = new Vector3(light.transform.position.x + multiplier, light.transform.position.y + multiplier, light.transform.position.z);
		light.GetComponent<Light>().range += multiplier;
	}


	public void GameOver() {
		//if Game over
		StopAllCoroutines();
		currentBlock = null;
		this.enabled = false;
	}


	private void Pauze() {
		//if pauzed
		if(currentBlock != null){
			currentBlock.SetActive(false);
		}
	}


	private void Resume() {
		//if pauzed
		if(currentBlock != null){
			currentBlock.SetActive(true);
		}
	}
}
