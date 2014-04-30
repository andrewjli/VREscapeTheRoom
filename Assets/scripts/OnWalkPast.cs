using UnityEngine;
using System.Collections;

public class OnWalkPast : MonoBehaviour {
	public AudioClip floorSound;

	void OnTriggerEnter(Collider other) {
		Debug.Log("in here");
		audio.clip = floorSound;
		if(!audio.isPlaying){
			audio.Play();
			audio.pitch = 0.5f*Random.value + 0.6f;
		}
    }
}
