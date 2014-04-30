using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class BookControl : MonoBehaviour {
	Animator bookAnim;
	Vector3 originalPosition;
	Quaternion originalRotation;
	static bool bookInUse = false;
	public AudioClip sound;
	bool isSelected= false;
	public bool wand = false,lit =false;
	public int pageCounter = 0;
	public int curPage = 5;
	Texture hintPage, bookHint, brickHint, keyHint, lampHint, loremPage, portraitHint;
	private static ArrayList coverTypes = new ArrayList{"A","B","C","D"};
	//private static Random rand = new Random();
	private float hintProb = 0.5f;
	//bool audioPlayed = false;
	//public Texture h;
	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		bookAnim = GetComponent<Animator>();
		bookHint = Resources.Load("Textures/BookHint") as Texture;
		brickHint = Resources.Load("Textures/BrickHint") as Texture;
		keyHint = Resources.Load("Textures/KeyHint") as Texture;
		lampHint = Resources.Load("Textures/LightHint") as Texture;
		loremPage = Resources.Load("Textures/LoremPage") as Texture;
		portraitHint = Resources.Load("Textures/PortraitHint") as Texture;
		GameObject cover = searchHierarchyForBone(transform,"Line002").gameObject;
		cover.renderer.material.mainTexture = Resources.Load(
			"Textures/Book" + coverTypes[Random.Range(0, coverTypes.Count)]) as Texture;
	}
	
	// Update is called once per frame
	void Update () {
		if(!wand && !BookControl.bookInUse){
			deselectBook(false);
		}
		if(MainGame.lampLit && !lit){
			lit = true;
			light.intensity = 0.01f;
			light.cullingMask = 1 << 10;
			createBooks.SetLayerRecursively(transform.gameObject,10);
		}
		/*if (Input.GetKeyUp (KeyCode.O)) {
			StartCoroutine( takeBook() );
		}
		
		if (Input.GetKeyUp (KeyCode.P)) {
			StartCoroutine( PlayOneShot("flipPage") );
		}
		
		if (Input.GetKeyUp (KeyCode.C)) {
			StartCoroutine( replaceBook() );
		}*/
		vrButtons buttons = null;
		// Getting a reference to different device types
		if (MiddleVR.VRDeviceMgr != null) {
			buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0.Buttons");
		}
		if (buttons != null)
		{
			if (buttons.IsToggled(0)&& bookInUse)
			{

				StartCoroutine( "flipPage" );

			}
		}
	}

	IEnumerator takeBook(){
		deselectBook(true);
		BookControl.bookInUse = true;
		yield return StartCoroutine(MoveObject(transform,transform.position, new Vector3(0.94f,1.23f,-0.18f),0.5f));
		yield return StartCoroutine(RotateObject(transform,transform.rotation, Quaternion.Euler(60,270,0),0.5f));
		yield return StartCoroutine(PlayOneShot("openBook"));
		hintPage = lampHint;
		if (MainGame.lampLit) {
			if (Random.value < 0.5) {
				hintPage = keyHint;
			} else {
				hintPage = portraitHint;
			}
			if (!MainGame.keyFound) {
				hintPage = keyHint;
			} else if (!MainGame.portraitBurnt) {
				hintPage = portraitHint;
			} else {
				hintPage = brickHint;
			}
		}
	}

	IEnumerator replaceBook(){
		yield return StartCoroutine(PlayOneShot("closeBook"));
		yield return StartCoroutine(RotateObject(transform,transform.rotation,originalRotation,0.5f));
		yield return StartCoroutine(MoveObject(transform,transform.position, originalPosition,0.5f));
		BookControl.bookInUse = false;
	}

	IEnumerator flipPage(){
		if (pageCounter != curPage) {
			GameObject prevPage = searchHierarchyForBone (transform, "Box001").gameObject;
			GameObject page = searchHierarchyForBone (transform, "Page001").gameObject;
			GameObject nextPage = searchHierarchyForBone (transform, "Box002").gameObject;
			curPage = pageCounter;
			if (pageCounter >= 5) {
				pageCounter = 0;
				StartCoroutine (replaceBook ());
				return true;
			} else if (pageCounter == 4) {
				if (Random.value < hintProb) {
					nextPage.renderer.material.mainTexture = hintPage;
				} else {
					nextPage.renderer.material.mainTexture = bookHint;
				}
			} else if (pageCounter < 4) {
				prevPage.renderer.material.mainTexture = loremPage;
				page.renderer.material.mainTexture = loremPage;
				nextPage.renderer.material.mainTexture = loremPage;
			}

			yield return StartCoroutine (PlayOneShot ("flipPage"));

			//

			yield return new WaitForSeconds (0.9f);
			prevPage.renderer.material.mainTexture = page.renderer.material.mainTexture;
			page.renderer.material.mainTexture = nextPage.renderer.material.mainTexture;
			pageCounter++;
		}
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

	public IEnumerator PlayOneShot ( string paramName ){
		bookAnim.SetBool (paramName, true);
		yield return null;
		bookAnim.SetBool (paramName, false);
	}

	void VRAction(){
		if(!BookControl.bookInUse && !MainGame.gameOver){
			//lighter.SetActive(false);
			StartCoroutine(takeBook());
		}
	}
	public void selectBook() {
		//Debug.Log(other.name);
		if(!isSelected && !BookControl.bookInUse){
			wand = true;
			isSelected = true;
			//StartCoroutine(MoveObject(transform,transform.position,new Vector3(transform.position.x -0.04f,transform.position.y,transform.position.z),0.3f));
			//StartCoroutine(RotateObject(transform, transform.rotation,Quaternion.Euler(80,90,270),0.3f));
			this.light.intensity = 3;
			this.light.cullingMask = 1 << 13;
			createBooks.SetLayerRecursively(this.transform.gameObject,13);
		}
	}

	public void deselectBook(bool noMove){
		this.light.intensity = 0.01f;
		this.light.cullingMask = 1 << 10;
		createBooks.SetLayerRecursively(this.transform.gameObject,10);
		if(!noMove){
			//StartCoroutine(MoveObject(transform,transform.position,originalPosition,0.3f));
			//StartCoroutine(RotateObject(transform, transform.rotation,originalRotation,0.3f));
		}
		isSelected = false;
	}

	public Transform searchHierarchyForBone(Transform current, string name)   
	{
		// check if the current bone is the bone we're looking for, if so return it
		if (current.name == name)
			return current;
		// search through child bones for the bone we're looking for
		for (int i = 0; i < current.childCount; ++i)
		{
			// the recursive step; repeat the search one step deeper in the hierarchy
			Transform found = searchHierarchyForBone(current.GetChild(i), name);
			
			// a transform was returned by the search above that is not null,
			// it must be the bone we're looking for
			if (found != null)
				return found;
		}
		// bone with name was not found
		return null;
	}

}
