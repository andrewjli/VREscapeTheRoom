using UnityEngine;
using System.Collections;

public class PortraitBurn : MonoBehaviour {
	public GameObject fire,picture;
	public AudioClip sound;
	public bool burnPortait = false;
	bool alreadyBurnt= false,isBurning = false;
	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if(MainGame.lampLit && MainGame.lighterLit && other.name.Equals("lighterCollider") &&!alreadyBurnt && !isBurning ){
			audio.clip = sound;
			audio.Play();
			StartCoroutine(burn());
		}
	}

	void Update(){
		/*if(burnPortait && !isBurning){
			audio.clip = sound;
			audio.Play();
			StartCoroutine(burn());
			//}
		}*/
	}

	IEnumerator burn(){
		isBurning = true;
		alreadyBurnt = true;
		fire.SetActive(true);
		yield return new WaitForSeconds(7.0f);   //Wait
		fire.SetActive(false);
		picture.SetActive(false);
		MainGame.portraitBurnt = true;
	}
}
