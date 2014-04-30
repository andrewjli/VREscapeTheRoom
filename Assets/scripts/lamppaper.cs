using UnityEngine;
using System.Collections;

public class lamppaper : MonoBehaviour {
	public GameObject lanternPaper;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void VRAction(){
		if(MainGame.safeOpen){
			audio.clip = sound;
			audio.Play();
			lanternPaper.SetActive(true);
			MainGame.paperFloating = true;
			this.transform.gameObject.SetActive(false);
		}
	}
}
