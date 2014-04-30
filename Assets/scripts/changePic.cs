using UnityEngine;
using System.Collections;

public class changePic : MonoBehaviour {
	public Texture[] pics;
	static public bool lightOff=false;
	int currentTex=0;
	
	
	// Update is called once per frame
	void Update () {
		if(lightOff){
			if(changeTex()){
				renderer.material.mainTexture = pics[currentTex];
				lightOff = false;
			}
		}
	}
	
	bool changeTex(){
		if(pics.Length == 0 || pics.Length<0){
			return false;
		}else{
			currentTex = ++currentTex % pics.Length;
			return true;
		}
	}
}
