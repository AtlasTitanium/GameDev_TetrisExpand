using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public bool HitGround = false;
	public bool LeftBlocked = false;
	public bool RightBlocked = false;

	void Start(){
		GetComponent<Renderer>().material.color = new Color(Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2));
		GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2)));
	}
	void Update () {
		if(transform != null){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, -Vector3.up, out hit, (transform.localScale.y/2)))
			{
				//there's a ground
				if(hit.transform.tag == "floor"){
					HitGround = true;	
					return;
				}
			}
			if (Physics.Raycast(transform.position, Vector3.right, out hit, (transform.localScale.x/2)))
			{
				//there's a right wall so the player can only go left
				if(hit.transform.tag == "wall" || hit.transform.tag == "floor"){
					//Debug.Log("there's a right wall");
					RightBlocked = true;
					return;
				}
			}
			if (Physics.Raycast(transform.position, -Vector3.right, out hit, (transform.localScale.x/2)))
			{
				//there's a left wall so the player can only go right
				if(hit.transform.tag == "wall" || hit.transform.tag == "floor"){
					//Debug.Log("there's a left wall");
					LeftBlocked = true;
					return;
				}
			}
			LeftBlocked = false;
			RightBlocked = false;
		}
	}
}
