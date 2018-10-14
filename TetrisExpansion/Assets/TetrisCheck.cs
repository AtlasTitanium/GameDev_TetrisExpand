using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCheck : MonoBehaviour {
	public GameObject blockPrefab, latestBlock;
	private List<GameObject> TetrisCheckers = new List<GameObject>();
	private bool ifTetris = false;
	private bool checkForTetris = true;

	void Start(){
		for(int i = 0; i < gameObject.transform.childCount; i++){
			TetrisCheckers.Add(gameObject.transform.GetChild(i).gameObject);
		}
	}

	void Update(){
		if(checkForTetris){
			Check();
		}
	}

	public void Check(){
		for(int i = 0; i < TetrisCheckers.Count; i++){
			RaycastHit hit;
			if (Physics.Raycast(TetrisCheckers[i].transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Infinity))
			{
				if(hit.transform.tag == "floor"){
					ifTetris = true;
				}
			}
			else
			{
				ifTetris = false;
				break;
			}
		}

		if(ifTetris){
			Debug.Log("There's a Tetris!!!!");
			EventManager Instance = new EventManager();
			Instance.GoTetris();
			StartCoroutine(WaitForNext());
		}
	}

	public void IncreaseSize(){
		latestBlock =  Instantiate (blockPrefab, latestBlock.transform.position, latestBlock.transform.rotation);
		latestBlock.transform.parent = transform;
		latestBlock.transform.position = new Vector2(latestBlock.transform.position.x + 2, latestBlock.transform.position.y);
	}

	IEnumerator WaitForNext(){
		checkForTetris = false;
		yield return new WaitForSeconds(2);
		checkForTetris = true;
	}
}
