using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormedBlock : MonoBehaviour {
	private Block blocky;
	private List<Block> blocks = new List<Block>();
	
	void Start(){
		for(int i = 0; i < transform.childCount; i++){
			blocky = transform.GetChild(i).gameObject.GetComponent<Block>();
			blocks.Add(blocky);
		}
		StartCoroutine(DropDown());
	}
	void Update () {
		bool noLeft = false;
		bool noRight = false;
		
		//there's no walls, so the player can go anywhere
		for(int i = 0; i < blocks.Count; i++){
			if(blocks[i].LeftBlocked){
				noLeft = true;
			}
			if(blocks[i].RightBlocked){
				noRight = true;
			}
		}

		if(!noRight){
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				transform.position = new Vector2(transform.position.x + 2, transform.position.y);
			}
		}
		if(!noLeft){
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				transform.position = new Vector2(transform.position.x - 2, transform.position.y);
			}
		}

		if(!noRight && !noLeft){
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				transform.Rotate(Vector3.forward * -90, Space.World);
			}	
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				transform.Rotate(Vector3.forward * 90, Space.World);
			}	
		}

	}

	IEnumerator DropDown(){
		yield return new WaitForSeconds(0.1f);
		transform.position = new Vector2(transform.position.x, transform.position.y - 1);
		//Debug.Log(gameObject + " goes down");
		StartCoroutine(DropDown());
	}

	public void Change(){
		//Debug.Log("they hit the ground");
		StopAllCoroutines();
		for(int f = 0; f < blocks.Count; f++){
			blocks[f].GetComponent<Renderer>().material.color = Color.red;
			blocks[f].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
			blocks[f].transform.tag = "floor";
		}
		//Debug.Log("every block is floor");
		this.enabled = false;
	}
}
