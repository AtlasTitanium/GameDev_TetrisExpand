using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour{
	//singleton
	private static EventManager instance = null;
	public static EventManager Instance {
		get {
			if(instance == null){
				instance = new EventManager();
			}
			return instance;
		}
	}

	//Got a tetris
	public delegate void Tetris();
	public static event Tetris GotTetris;

	public void GoTetris(){
		if(GotTetris != null){
			GotTetris();
		}
	}
}
