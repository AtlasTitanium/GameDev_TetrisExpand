using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager{

	//Got a tetris
	public delegate void Tetris();
	public static event Tetris GotTetris;

	public static void GoTetris(){
		if(GotTetris != null){
			GotTetris();
		}
	}

	public delegate void Check();
	public static event Check CheckTetris;

	public static void Checker(){
		if(CheckTetris != null){
			CheckTetris();
		}
	}
}
