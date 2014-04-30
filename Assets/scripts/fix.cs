using UnityEngine;
using System.Collections;

public class fix : MonoBehaviour {
	public Mesh mesh;
	// Use this for initialization
	void Start () {
		TangentSolver.Solve( mesh , false );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
