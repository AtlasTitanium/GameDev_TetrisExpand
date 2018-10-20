using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCheck : MonoBehaviour {
	private Vector3 origin;
	private Vector3 newPos;
	public int width = 20;
	public int height = 20;
	private int howManyPoints;
	void Start(){
		//DontDestroyOnLoad(this.gameObject);
		origin = this.transform.position;
		newPos = origin;

		EventManager.CheckTetris += Check;
		EventManager.GotTetris += IncreaseSize;
	}

	void Reset()
    {
        EventManager.CheckTetris += Check;
		EventManager.GotTetris += IncreaseSize;	
    }
	
	void Update(){
		//Debug.Log(howManyPoints + " : building up");
	}

	public void Check(){
		while(newPos.y < height){
			while(newPos.x < width){
				//Debug.Log("i Resetted");		
				Collider[] hitColliders = Physics.OverlapBox(transform.position,new Vector3(0.1f,0.1f,0.1f),Quaternion.identity);
				if(hitColliders.Length > 0){
					newPos.x += 2;
					transform.position = newPos;
					//Debug.Log(transform.localPosition.x + " this x position has the block " + hitColliders[0] + " in it");
				} else { 
					//Debug.Log("no tetris");
					newPos.x = origin.x;
					newPos.y += 2;
					transform.position = newPos;
					break;
				}
			}

			if(newPos.x >= width){
				//Debug.Log("Tetris!!");
				//Debug.Log(howManyPoints + " : building up");
				howManyPoints ++;
				newPos.x = origin.x;
				newPos.y += 2;
				transform.position = newPos;
			}	
			
		}

		if(newPos.y >= height){
			for(int i = 0; i < howManyPoints; i++){
				//Debug.Log("doing tetris");
				EventManager.GoTetris();
			}	
			howManyPoints = 0;
			newPos.y = origin.y;
			newPos.x = origin.x;
			transform.position = origin;		
		}	

		//Debug.Log(howManyPoints + " : total");	
		
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