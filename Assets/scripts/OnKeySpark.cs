using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class OnKeySpark : MonoBehaviour {
	public GameObject sparks,pointLight,flame;

	public float durationForSpark = 0.2f;
	public float durationForPointLight = 5f;
	public AudioClip sparkSound;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//for desktop use
		/*if(Input.GetKeyDown(KeyCode.Z)){
			StartCoroutine(Spark ());		
			StartCoroutine(light ());
		}*/

		vrButtons buttons = null;
        // Getting a reference to different device types
        if (MiddleVR.VRDeviceMgr != null) {
            buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0.Buttons");
        }
		if (buttons != null)
        {
            if (buttons.IsToggled(2))
            {
		//if(Input.GetKeyDown(KeyCode.Z)){
			StartCoroutine(Spark ());		
				StartCoroutine(lightFlame ());
		//}
			}
		}

	}
	
    IEnumerator Spark(){
		if(!MainGame.lighterLit){
			audio.clip = sparkSound;
			audio.Play ();
			sparks.SetActive(true);
			yield return new WaitForSeconds(durationForSpark);   //Wait
			sparks.SetActive(false);
		}

	}
	
	
    IEnumerator lightFlame(){
		if(!MainGame.lighterLit){
			MainGame.lighterLit = true;
			pointLight.SetActive(true);
			flame.SetActive(true);
			yield return new WaitForSeconds(durationForPointLight);   //Wait
			pointLight.SetActive(false);
			flame.SetActive(false);
			if(!MainGame.lampLit)
				changePic.lightOff = true;
			MainGame.lighterLit = false;
		}
	}
}
