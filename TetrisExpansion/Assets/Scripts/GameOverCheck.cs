using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCheck : MonoBehaviour {
	private float width = 20;
	private Vector3 origin;
	private Vector3 newPos;


	void Start(){
		origin = this.transform.position;
		newPos = origin;
	}


	void OnEnable(){
		EventManager.CheckTetris += Check;
		EventManager.GotTetris += IncreaseSize;
	}


	void OnDisable(){
		EventManager.CheckTetris -= Check;
		EventManager.GotTetris -= IncreaseSize;
	}


	public void Check(){
		while(newPos.x < width){
			//check for blocks in row
			Collider[] hitColliders = Physics.OverlapBox(transform.position,new Vector3(0.01f,0.01f,0.01f),Quaternion.identity);
			if(hitColliders.Length > 0){
				if(hitColliders[0].tag == "floor"){
					//if it hit any block = Game Over
					transform.position = origin;
					EventManager.GameOver();
				} else {
					newPos.x += 2;
					transform.position = newPos;
				}
				
			} else { 
				newPos.x += 2;
				transform.position = newPos;
			}
		}
		if(newPos.x >= width){
			//No more checks = no Game Over
			EventManager.DropBlock();
			newPos.x = origin.x;
			transform.position = origin;
		}			
	}


	public void IncreaseSize(){
		width += 2;
		origin.y += 2;
		newPos = origin;
		transform.position = origin;
	}
}
