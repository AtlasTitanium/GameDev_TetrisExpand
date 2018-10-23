using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCheck : MonoBehaviour {
	private float width = 20;
	private float height = 20;
	private Vector3 origin;
	private Vector3 newPos;
	private int howManyPoints;


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
		while(newPos.y < height){
			while(newPos.x < width){		
				Collider[] hitColliders = Physics.OverlapBox(transform.position,new Vector3(0.1f,0.1f,0.1f),Quaternion.identity);
				if(hitColliders.Length > 0){
					//There is a block in this line
					newPos.x += 2;
					transform.position = newPos;
				} else { 
					//No Blocks = No Tetris = One line up
					newPos.x = origin.x;
					newPos.y += 2;
					transform.position = newPos;
					break;
				}
			}

			if(newPos.x >= width){
				//If there is a tetris
				howManyPoints ++;
				newPos.x = origin.x;
				newPos.y += 2;
				transform.position = newPos;
			}	
			
		}

		if(newPos.y >= height){
			for(int i = 0; i < howManyPoints; i++){
				//Ativate Tetris as much as the aquired points
				EventManager.GoTetris();
			}	
			howManyPoints = 0;
			newPos.y = origin.y;
			newPos.x = origin.x;
			transform.position = origin;		
		}	
	}


	public void IncreaseSize(){
		width += 2;
		height += 2;
	}
}