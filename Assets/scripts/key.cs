using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void VRAction(){
		if(MainGame.lampLit && !MainGame.keyFound){
			MainGame.keyFound = true;
			transform.gameObject.SetActive(false);
		}
	}
}
