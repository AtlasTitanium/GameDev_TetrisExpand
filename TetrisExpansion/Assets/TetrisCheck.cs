using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCheck : MonoBehaviour {
	private bool ifTetris = false;
	private bool checkForTetris = true;
	private int howManyBlocks = 0;
	private Vector3 origin;
	private Vector3 newPos;
	public int width = 20;
	public int height = 20;
	void Start(){
		origin = this.transform.position;
		newPos = origin;

		EventManager.CheckTetris += Check;
		EventManager.GotTetris += IncreaseSize;
	}

	public void Check(){
		
		while(newPos.y < height){
			while(newPos.x < width){
				Collider[] hitColliders = Physics.OverlapBox(transform.position,new Vector3(0.1f,0.1f,0.1f),Quaternion.identity);
				if(hitColliders.Length > 0){
					newPos.x += 2;
					transform.position = newPos;
					Debug.Log(transform.localPosition.x + " this x position has the block " + hitColliders[0] + " in it");
				} else {
					Debug.Log("no tetris");
					transform.position = origin;
					newPos.x = origin.x;
					newPos.y += 2;
					break;
				}
			}

			if(newPos.x >= width){
				Debug.Log("Tetris!!");
				EventManager.GoTetris();
				break;
			}
		}

		if(newPos.y >= height){
			newPos.y = origin.y;
		}
		
		
	}

	public void IncreaseSize(){
		width += 2;
		height += 2;
	}
}

/*if(hit.transform.tag == "floor"){
					howManyBlocks ++;
					//Debug.Log("how many blocks in view" + howManyBlocks);
					if(howManyBlocks == TetrisCheckers.Count){
						howManyBlocks = 0;
						Debug.Log("There's a Tetris!!!!");
						EventManager.GoTetris();
						StartCoroutine(WaitForNext());
					}
				} */