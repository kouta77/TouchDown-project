using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class BackgroundController : MonoBehaviour {
	[System.Serializable]
	public class campusParts{
		public BG_part_ctrl MainPart;
		public BG_part_ctrl FinisherPart;
	}



	public static float TimeScale = 1f;

	public List<campusParts> BGList = new List<campusParts>();

	public GameObject Lastinastance; 

	public int Indexu = 0;

	private bool gameStarted = false;

//	private GameController controller;
	// Use this for initialization
	void Start () {
//		controller = GameObject.FindObjectOfType<GameController> ();

		Lastinastance = GameObject.FindObjectOfType<BG_part_ctrl> ().gameObject;
		Lastinastance.transform.parent = this.transform;
		InvokeRepeating ("InstantiateBG", 0.5f, 0.5f);

		//StartCoroutine ("ChangeBackground");
	}
	
	// Update is called once per frame
	void Update () {
	}
	public float MaxTime = 3f;
	IEnumerator ChangeBackground(){
		if (gameStarted) {
			Debug.Log ("Started");
			//if(controller.GoalCount > 0)
			//	MaxTime = 7f;

			yield return new WaitForSeconds (Random.Range (5f, 8f));
			Debug.Log ("Passed");

			var lastIndex = Indexu;
			InstantiateBG_Final ();
			Indexu = Random.Range (0, BGList.Count);

			while (Indexu == lastIndex) {
				Indexu = Random.Range (0, BGList.Count);
				yield return null;
			}
			StartCoroutine ("ChangeBackground");
		}

	}

	void InstantiateBG(){
		if (GameObject.FindObjectsOfType<BG_part_ctrl> ().Length > 3)
			return;

		var RoadObject = Instantiate (BGList[Indexu].MainPart.gameObject,transform.position,transform.rotation) as GameObject;
		RoadObject.transform.parent = this.transform;
		RoadObject.GetComponent<BG_part_ctrl>().belowPart = Lastinastance.GetComponent<BG_part_ctrl>();
		Lastinastance = RoadObject;

	}
	void InstantiateBG_Final(){
		Debug.Log ("final");
		var RoadObject = Instantiate (BGList[Indexu].FinisherPart.gameObject,transform.position,transform.rotation) as GameObject;
		RoadObject.transform.parent = this.transform;
		RoadObject.GetComponent<BG_part_ctrl>().belowPart = Lastinastance.GetComponent<BG_part_ctrl>();
		Lastinastance = RoadObject;
		
	}

	public void GameStart(){gameStarted = true;StartCoroutine ("ChangeBackground");}
}
