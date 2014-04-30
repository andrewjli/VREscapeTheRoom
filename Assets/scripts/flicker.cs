using UnityEngine;
using System.Collections;

public class flicker : MonoBehaviour {
	Light dirLight;
	int i = 0;
	public int rate = 4;
	public float intensity = 0.5f;
	// Use this for initialization
	void Start () {
		dirLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(i <rate){
			i++;
		}else{
			i =0;
			dirLight.intensity =  Random.value*0.1f+intensity;
		}
	}
}
