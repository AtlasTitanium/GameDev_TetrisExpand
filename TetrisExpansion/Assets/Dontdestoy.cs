using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestoy : MonoBehaviour {
	public static Dontdestoy instance = null;  
	void Start () {
		if (instance == null) instance = this;
    	else if (instance != this) Destroy(gameObject); 
		DontDestroyOnLoad(this.gameObject);
	}
	
}
