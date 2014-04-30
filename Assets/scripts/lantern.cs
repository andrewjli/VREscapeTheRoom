using UnityEngine;
using System.Collections;

public class lantern : MonoBehaviour {
	public GameObject pointLight,dirLight,halo;
	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.name);
		if(!MainGame.lampLit && MainGame.lighterLit && other.name.Equals("lighterCollider") ){
			pointLight.SetActive(true);
			dirLight.SetActive(true);
			halo.SetActive(true);
			MainGame.lampLit = true;
		}
	}
}
