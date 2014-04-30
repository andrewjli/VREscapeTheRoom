using UnityEngine;
using System.Collections;

public class createBooks : MonoBehaviour {
	float rowX = 1.25f;
	float[] rowY = new float[]{1.58f,1.13f,0.685f,0.31f};
	float[] rowZ = new float[]{0.29f,-1f};
	Vector3 currentPosition;
	public int numberOfBooks = 20;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		generateBooks();
	}

	void generateBooks(){
		currentPosition = new Vector3(rowX,rowY[0],rowZ[0]);
		GameObject books = new GameObject();
		float randomZ;
		books.name = "books";
		books.transform.parent = transform;
		int currentRow = 0;
		for(int i = 0;i < numberOfBooks;i++){
			if (Random.value < 0.9) {
				randomZ = 0.000001f;
			} else {
				randomZ = Random.Range(0.05f, 0.4f);
			}			if(!betweenRowZ(currentPosition.z-randomZ)){
				currentRow++;

				if(currentRow == rowY.Length){
					return;
				}
				currentPosition.y = rowY[currentRow];
				currentPosition.z = rowZ[0];
			}else{
				GameObject book = (GameObject)Instantiate(Resources.Load("Book"));
				book.AddComponent<BookControl>();
				book.GetComponent<BookControl>().sound = sound;
				currentPosition.z -= randomZ;
				book.name = "book "+i;
				book.transform.parent = books.transform;
				book.transform.position = currentPosition;
				currentPosition.z-= 0.048f;
				book.AddComponent<BoxCollider>();
				book.GetComponent<BoxCollider>().center = new Vector3(-1.4f,0f,1.7f);
				book.GetComponent<BoxCollider>().size = new Vector3(3f,1f,3.5f);
				book.GetComponent<BoxCollider>().isTrigger = true;
				book.AddComponent<Light>();
				book.GetComponent<Light>().color = Color.white;
				book.GetComponent<Light>().intensity = 0f;
				book.GetComponent<Light>().cullingMask = 1 <<10;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	bool betweenRowZ(float z){
		return (z > rowZ[1] && z < rowZ[0]);
	}
	public static void SetLayerRecursively(GameObject obj, int newLayer ){
		obj.layer = newLayer;
		foreach( Transform child in obj.transform ){
			SetLayerRecursively( child.gameObject, newLayer );
		}
	}

}
