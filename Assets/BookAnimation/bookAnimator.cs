using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;
public class bookAnimator : MonoBehaviour {

	Animator bookAnim;

	// Use this for initialization
	void Start () {
		bookAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		/*vrButtons buttons = null;
		// Getting a reference to different device types
		if (MiddleVR.VRDeviceMgr != null) {
			buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0.Buttons");
		}
		if (buttons != null)
		{
			if (buttons.IsToggled(3))
			{
				//if(Input.GetKeyDown(KeyCode.Z)){
				StartCoroutine( PlayOneShot("openBook") );
			}
		}*/
		if (Input.GetKeyUp (KeyCode.O)) {
			StartCoroutine( PlayOneShot("openBook") );
		}

		if (Input.GetKeyUp (KeyCode.P)) {
			StartCoroutine( PlayOneShot("flipPage") );
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			StartCoroutine( PlayOneShot("closeBook") );
		}
	}

	public IEnumerator PlayOneShot ( string paramName ){
		bookAnim.SetBool (paramName, true);
		yield return null;
		bookAnim.SetBool (paramName, false);
	}
}
