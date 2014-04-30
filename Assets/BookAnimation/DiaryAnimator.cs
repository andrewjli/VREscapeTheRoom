using UnityEngine;
using System.Collections;

public class DiaryAnimator : MonoBehaviour {

	Animator diaryAnim;
	public GameObject key;
	private int pageCounter = 1;
	private int curPage = 6;
	private ArrayList pages = new ArrayList();
	bool lit = false,unlocked = false,isUnlocking=false;

	// Use this for initialization
	void Start () {
		diaryAnim = GetComponent<Animator>();
		transform.gameObject.AddComponent<Light>();
		for (int i=1; i<=6; i++) {
			pages.Add(Resources.Load("Textures/Diary" + i.ToString()) as Texture);
		}
		pages.Add (Resources.Load ("Textures/BrickSequence") as Texture);
		GameObject page = searchHierarchyForBone (transform, "Page001").gameObject;
		GameObject prevPage = searchHierarchyForBone (transform, "Box001").gameObject;
		GameObject nextPage = searchHierarchyForBone (transform, "Box002").gameObject;
		prevPage.renderer.material.mainTexture = (Texture)pages [0];
		nextPage.renderer.material.mainTexture = (Texture)pages [0];
		page.renderer.material.mainTexture = (Texture)pages [0];
		this.light.cullingMask = 1 << 11;
		light.intensity = 0f;
		//StartCoroutine(unlockDiary());
	}

	
	IEnumerator unlockDiary(){
		isUnlocking = true;
		yield return StartCoroutine(MoveObject(key.transform,key.transform.position,new Vector3(key.transform.position.x,0.922f,key.transform.position.z),0.3f));
		yield return StartCoroutine(RotateObject(key.transform,key.transform.rotation,Quaternion.Euler(0,180,270),0.5f));
		key.SetActive(false);
		yield return StartCoroutine(PlayOneShot("unlock"));
		yield return new WaitForSeconds(1f);
		yield return StartCoroutine(PlayOneShot("openDiary"));
		isUnlocking = false;
		unlocked = true;
	}

	IEnumerator flipPage(){
		if (pageCounter != curPage) {
			GameObject page = searchHierarchyForBone (transform, "Page001").gameObject;
			GameObject nextPage = searchHierarchyForBone (transform, "Box002").gameObject;
			curPage = pageCounter;
			if (pageCounter >= 7) {
				return true;
			} else {
				nextPage.renderer.material.mainTexture = (Texture)pages[pageCounter];
			}
			yield return StartCoroutine (PlayOneShot ("flipPage"));
			yield return new WaitForSeconds (0.8f);
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
	
	// Update is called once per frame
	void Update () {
		if(MainGame.lampLit && !lit){
			lit = true;
			this.light.intensity = 5;
			this.light.range = 1000;

			createBooks.SetLayerRecursively(transform.gameObject,11);
		}

		if (Input.GetKeyUp (KeyCode.U)) {
			StartCoroutine( "unlockDiary" );
		}
		
		if (Input.GetKeyUp (KeyCode.P)) {
			StartCoroutine( "flipPage" );
		}
	}

	public IEnumerator PlayOneShot ( string paramName ){
		diaryAnim.SetBool (paramName, true);
		yield return null;
		diaryAnim.SetBool (paramName, false);
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

	void VRAction(){
		Debug.Log("diary");
		if(MainGame.keyFound){
			if(!unlocked && !isUnlocking){
				StartCoroutine( "unlockDiary" );
			}else if(unlocked && !isUnlocking){
				StartCoroutine( "flipPage" );
			}
		}
	}
}
