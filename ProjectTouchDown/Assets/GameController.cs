using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static bool GameStart = false;
	public int GoalCount = 0;
	public Text GoalCountUI;
	public Slider SlowMoBar;

	public float SlowMoValue = 1;


	public Collider GameplayTouchArea;

	// List<GameObject> ActivateObjects = new List<GameObject>();
	public List<GameObject> DeactivateObjects = new List<GameObject>();

	public GameObject GamePlay_UI;
	public GameObject GO_UI;
	public Text BestSUI;
	public Text YourSUI;

	public Animator TouchDownLogo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GoalCountUI.text = GoalCount.ToString();
		SlowMoBar.value = SlowMoValue;


		if (GameController.GameStart == false) {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (GameplayTouchArea.Raycast(ray, out hit, Mathf.Infinity)){
					//foreach(var obj in ActivateObjects){obj.SetActive(true);}
					foreach(var obj in DeactivateObjects){obj.SetActive(false);}
					GamePlay_UI.SetActive(true);

					//Start Spamming enemyes
					GameObject.FindObjectOfType<EnemySpawnScript>().SendMessage("StartGame", SendMessageOptions.RequireReceiver);

					GameplayTouchArea.gameObject.SetActive(false);
					GameController.GameStart = true;
				}
			}

		}
	}


	void GameOverUI(){
		if(GoalCount > PlayerPrefs.GetInt ("Best", 0))
		PlayerPrefs.SetInt ("Best", GoalCount);

		BestSUI.text = "Best score: " + PlayerPrefs.GetInt ("Best", 0);
		YourSUI.text = "Your score: " + GoalCount;

		GamePlay_UI.SetActive(false);
		GO_UI.SetActive (true);
	}


	public void restartLevel(){
		BackgroundController.TimeScale = 1f;
		GameController.GameStart = false;
		Application.LoadLevel ("MainGameplay");
	}

	public void facebookShare(){
		string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" +  WWW.EscapeURL("https://play.google.com/store/apps/details?id=com.realityStudio.WarOfImperionLite");
		Application.OpenURL(facebookshare);

	}

	public void TwitterShare(){
		string twittershare = "http://twitter.com/home?status=" +  WWW.EscapeURL("https://play.google.com/store/apps/details?id=com.realityStudio.WarOfImperionLite");
		Application.OpenURL(twittershare);	
	}

	public void GoalIncrease(){
		GoalCount += 1;
		TouchDownLogo.SetTrigger("Animate");
		GoalCountUI.GetComponent<Animator> ().SetTrigger ("Animate");
	}
}
