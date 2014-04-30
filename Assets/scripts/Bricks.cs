using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {
	public GameObject spotlight;
	public bool lit =false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void VRAction(){
		if( MainGame.paperFloating && !lit){
			lit = true;
			spotlight.light.intensity += 2.0f;
			Sequence.set(this.name);
		}
	}


}
