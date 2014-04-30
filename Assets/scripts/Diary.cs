using UnityEngine;
using System.Collections;

public class Diary : MonoBehaviour {
	public GameObject key;
	// Use this for initialization
	void Start () {
		transform.gameObject.AddComponent<Light>();
		light.intensity = 0.05f;
		StartCoroutine(unlockDiary());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator unlockDiary(){
		yield return StartCoroutine(MoveObject(key.transform,key.transform.position,new Vector3(key.transform.position.x,0.922f,key.transform.position.z),0.3f));
		StartCoroutine(RotateObject(key.transform,key.transform.rotation,Quaternion.Euler(0,180,270),0.5f));
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
	
	IEnumerator RotateObject (Transform thisTransform, Quaternion startRot, Quaternion endRot, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0) {
			i += Time.deltaTime * rate;
			thisTransform.rotation = Quaternion.Lerp(startRot, endRot, i);
			yield return null;
		}
	}
}
