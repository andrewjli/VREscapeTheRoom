using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class shelfMove : MonoBehaviour {
	bool shelfMoving = false;
	public GameObject ambient;
	public AudioClip movingSound;
	public float time = 3;
	public static bool moveShelf = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(MainGame.bricksLit){
		if(moveShelf && !shelfMoving){
		/*vrButtons buttons = null;
		// Getting a reference to different device types
		if (MiddleVR.VRDeviceMgr != null) {
			buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0.Buttons");
		}
		if (buttons != null)
		{
			if (buttons.IsToggled(2)&& !shelfMoving)
			{*/
				shelfMoving = true;
				audio.clip = movingSound;
				audio.pitch = 0.8f;
				audio.Play();
				StartCoroutine(MoveObject(transform, transform.localPosition,
				                          transform.localPosition+Vector3.forward , time));
			MainGame.gameOver = true;
			//}
		}
			
		//}
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0) {
			ambient.GetComponent<AudioLowPassFilter>().cutoffFrequency = 300 + (22000-300)*i;
			i += Time.deltaTime * rate;
			thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
}
