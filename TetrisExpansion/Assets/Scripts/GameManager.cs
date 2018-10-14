using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject rowPrefab, roof, rightwall, leftwall, backwall, ground;
	public GameObject TetrisCheckParent, tetrisCheckPrefab, latestTetrisCheck;
	public GameObject latestRow, currentBlock;

	public GameObject[] tetrisBlockPrefabs;

	void Start(){
		CreateBlock();
		EventManager.GotTetris += Sirtet;
	}

	void Update(){
		for(int i = 0; i < currentBlock.transform.childCount; i++){
			Block blocky = currentBlock.transform.GetChild(i).gameObject.GetComponent<Block>();
			if(blocky.HitGround){
				PlaceBlock();
			}
		}
	}
	void CreateBlock(){
		//this creates the block
		int randomBlock = Random.RandomRange(0,tetrisBlockPrefabs.Length);
		currentBlock = Instantiate(tetrisBlockPrefabs[randomBlock], roof.transform.position, roof.transform.rotation);
	}

	public void PlaceBlock(){
		//this places the block
		for(int i = 0; i < currentBlock.transform.childCount; i++){
			currentBlock.transform.GetChild(i).transform.tag = "floor";
		}
		currentBlock.GetComponent<FormedBlock>().enabled = false;
		currentBlock = null;
		CreateBlock();
	}

	public void Sirtet() {
		//if Tetris complete
		latestTetrisCheck =  Instantiate (tetrisCheckPrefab, latestTetrisCheck.transform.position, latestTetrisCheck.transform.rotation);
		latestTetrisCheck.transform.parent = TetrisCheckParent.transform;
		latestTetrisCheck.transform.position = new Vector2(latestTetrisCheck.transform.position.x, latestTetrisCheck.transform.position.y + 2);

		for(int i = 0; i < TetrisCheckParent.transform.childCount; i++){
			TetrisCheckParent.transform.GetChild(i).GetComponent<TetrisCheck>().IncreaseSize();
		}

		rightwall.transform.position = new Vector2(rightwall.transform.position.x + 2, rightwall.transform.position.y + 1);
		leftwall.transform.position = new Vector2(leftwall.transform.position.x, leftwall.transform.position.y + 1);
		backwall.transform.position = new Vector2(backwall.transform.position.x + 1, backwall.transform.position.y + 1);
		ground.transform.position = new Vector2(ground.transform.position.x + 1, ground.transform.position.y);

		rightwall.transform.localScale = new Vector3(rightwall.transform.localScale.x + 2, rightwall.transform.localScale.y,rightwall.transform.localScale.z);
		leftwall.transform.localScale = new Vector3(leftwall.transform.localScale.x + 2, leftwall.transform.localScale.y,leftwall.transform.localScale.z);
		backwall.transform.localScale = new Vector3(backwall.transform.localScale.x + 2, backwall.transform.localScale.y + 2,backwall.transform.localScale.z);
		ground.transform.localScale = new Vector3(ground.transform.localScale.x + 2, ground.transform.localScale.y,ground.transform.localScale.z);

		roof.transform.position = new Vector2(roof.transform.position.x, roof.transform.position.y + 2);

		latestRow = Instantiate(rowPrefab, latestRow.transform.position, latestRow.transform.rotation);
		latestRow.transform.parent = roof.transform;
		latestRow.transform.position = new Vector2(latestRow.transform.position.x + 2, latestRow.transform.position.y);

		transform.position = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z - 2);
	}
}
