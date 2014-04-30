using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;

public class safeOpen : MonoBehaviour {
	Animator anim;
	public AudioClip safeOpenSound;
	bool safeOpening = false;
	//public bool open = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("here");
		/*vrButtons buttons = null;
		// Getting a reference to different device types
		if (MiddleVR.VRDeviceMgr != null) {
			buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0.Buttons");
		}
		if (buttons != null)
		{
			if (buttons.IsToggled(1)&& !safeOpening)
			{
				//Debug.Log("opening safe");
				safeOpening = true;
				audio.clip = safeOpenSound;
				if(!audio.isPlaying){
					audio.Play();
				}
				anim.SetBool("opensesame",true);
			}
		}*/
	}

	void VRAction(){
		if(MainGame.lampLit && MainGame.portraitBurnt && !MainGame.safeOpen && !safeOpening){
			safeOpening = true;
			audio.clip = safeOpenSound;
			if(!audio.isPlaying){
				audio.Play();
			}
			anim.SetBool("opensesame",true);
			GetComponent<BoxCollider>().enabled = false;
			MainGame.safeOpen = true;
		}
	}
}
