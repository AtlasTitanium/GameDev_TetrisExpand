using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	private bool leftBlocked;
	public bool LeftBlocked{
		get{
			return leftBlocked;
		}
		set{
			leftBlocked = false;
		}
	}

	private bool rightBlocked;
	public bool RightBlocked{
		get{
			return rightBlocked;
		}
		set{
			rightBlocked = false;
		}
	}


	void Start(){
		GetComponent<Renderer>().material.color = new Color(Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2));
		GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2),Random.RandomRange(0.5f,2)));
	}

	
	void Update () {
		if(transform != null){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, -Vector3.up, out hit, (transform.localScale.y/2 + 0.5f)))
			{
				//there's a ground
				if(hit.transform.tag == "floor"){
					GetComponent<Renderer>().material.color = Color.red;
					GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
					transform.parent.GetComponent<FormedBlock>().Change();
					this.enabled = false;
					return;
				}
			}
			if (Physics.Raycast(transform.position, Vector3.right, out hit, (transform.localScale.x/2 + 0.5f)))
			{
				//there's a right wall so the player can only go left
				if(hit.transform.tag == "wall" || hit.transform.tag == "floor"){
					rightBlocked = true;
					return;
				}
			}
			if (Physics.Raycast(transform.position, -Vector3.right, out hit, (transform.localScale.x/2 + 0.5f)))
			{
				//there's a left wall so the player can only go right
				if(hit.transform.tag == "wall" || hit.transform.tag == "floor"){
					leftBlocked = true;
					return;
				}
			}
			leftBlocked = false;
			rightBlocked = false;
		}
	}
}
