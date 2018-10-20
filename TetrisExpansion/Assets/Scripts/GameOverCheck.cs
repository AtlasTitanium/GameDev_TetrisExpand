using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCheck : MonoBehaviour {


	private Vector3 origin;
	private Vector3 newPos;
	public int width = 20;

	void Start(){
		//DontDestroyOnLoad(this.gameObject);
		origin = this.transform.position;
		newPos = origin;

		EventManager.CheckTetris += Check;
		EventManager.GotTetris += IncreaseSize;
		
	}

	public void Check(){
		while(newPos.x < width){
			//Debug.Log("origin.y = " + origin.y);
			Collider[] hitColliders = Physics.OverlapBox(transform.position,new Vector3(0.01f,0.01f,0.01f),Quaternion.identity);
			if(hitColliders.Length > 0){
				//Debug.Log("GameOver");
				if(hitColliders[0].tag == "floor"){
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
			//Debug.Log("no game over");
			EventManager.DropBlock();
			newPos.x = origin.x;
			transform.position = origin;
			//Debug.Log("no loss");
		}			
	}

	public void IncreaseSize(){
		width += 2;
		origin.y += 2;
		newPos = origin;
		transform.position = origin;
	}
}
