using UnityEngine;
using System.Collections;

public class Sequence : MonoBehaviour {
	public GameObject pointLight,directionalLight,spotLights;

	public static bool matching = false;
	bool lightDim = false;

	static char[] array = new char[] {'D','O','O','R','W','A','Y'};
	static char[] anotherArray = new char[7];
	static int counter = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(MainGame.paperFloating && !lightDim){
			dimLights();
		}
		if(counter == 7) {
			for(int i = 0; i<7; i++) {
				if(array[i] == anotherArray[i]) {
					matching = true;
				} else {
					matching = false;
					break;
				}
			}
			if(matching) {
				shelfMove.moveShelf = true;
			} else {
				dimLights ();
				counter = 0;
			}
		}
	}

	void dimLights(){
		lightDim = true;
		pointLight.light.intensity = 0.3f;
		directionalLight.GetComponent<flicker>().intensity = 0.2f;
		foreach(Transform child in spotLights.transform){
			child.gameObject.light.intensity -= 2.0f;
			//child.gameObject.GetComponent<Bricks>().lit = false;
		}
		foreach(Transform child in transform){
			child.gameObject.GetComponent<Bricks>().lit = false;
		}
		spotLights.SetActive(true);
	}

	public static void set(string s) {
		anotherArray[counter] = s.ToCharArray()[0];
		counter++;
	}
}
