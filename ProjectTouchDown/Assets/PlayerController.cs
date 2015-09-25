using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public GameController Controller;

	public Vector3 TouchOffset;
	public bool SlowMo = false;
	public float MoveSpeed = 4.0f;



	private bool StartGame = false;
	public SpriteRenderer BG_DIM;
	public Color[] ColorsDIM;
	public float Slider;
	public float offset = 0.2f;

	public float SliderRot = 0;

	public bool PlayerDead = false;

	public List<Sprite> skins = new List<Sprite>();

	private SpriteRenderer spriter;
	private int currentSkin = 0;
	private int LastSelectSkin;
	// Use this for initialization
	void Start () {
		spriter = GetComponent<SpriteRenderer> ();

		BackgroundController.TimeScale = 0.3f;

	if (GameObject.FindObjectOfType<GameController> ())
			Controller = GameObject.FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StartGame == true)
			BG_DIM.color = Color.Lerp (ColorsDIM [0], ColorsDIM [1], Slider);
		else {
			spriter.sprite = skins[currentSkin];
		}

		if (Input.GetMouseButton (0) == true && GameController.GameStart == true && PlayerDead== false) {
			if (SlowMo == false) {
				Camera.main.gameObject.SendMessage ("StartFunc", SendMessageOptions.RequireReceiver);
				GameObject.FindObjectOfType<BackgroundController>().SendMessage("GameStart", SendMessageOptions.RequireReceiver);
				StartGame = true;
				Slider = Mathf.Lerp (Slider, 1, 15f * Time.deltaTime);
			}

			if (SlowMo == true) {
				Camera.main.gameObject.SendMessage ("NormalCam", SendMessageOptions.RequireReceiver);
				SlowMo = false;
			}

			//Time.timeScale = 1f;
			BackgroundController.TimeScale = 1f;
			if (Controller.SlowMoValue < 1)
				Controller.SlowMoValue += 0.5f * Time.deltaTime;

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				var newPosition = hit.point;
				transform.position = Vector3.MoveTowards (transform.position, newPosition + TouchOffset, MoveSpeed * Time.deltaTime * BackgroundController.TimeScale);

				SliderRot = Vector2.Distance(new Vector2(transform.position.x,0),new Vector2(newPosition.x,0));

				float newSlider = 0;

				newSlider = Mathf.Lerp(newSlider, SliderRot, Time.time*Time.deltaTime);

				//if(SliderRot < 2.1) 
				//	SliderRot = 0;//Mathf.MoveTowards(newSlider, 0, 4f*Time.deltaTime);

				if(hit.point.x > transform.position.x+offset)SliderRot = -SliderRot;

				Vector3 newRot = new Vector3(0,0,SliderRot*20);
				transform.eulerAngles = newRot;//Vector3.MoveTowards(transform.eulerAngles,newRot, Time.time*Time.deltaTime);

			}
		} else if (StartGame == true && Controller.SlowMoValue > 0 && PlayerDead==false) {
			//BG_DIM.color = Color.Lerp(ColorsDIM[1],ColorsDIM[0], 0.02f);
			Slider = Mathf.Lerp (Slider, 0, 15f * Time.deltaTime);
			SlowMo = true;
			//Time.timeScale = 0.3f;
			BackgroundController.TimeScale = 0.3f;
			Camera.main.gameObject.SendMessage ("SlowMoCam", SendMessageOptions.RequireReceiver);
			Controller.SlowMoValue -= 1f * Time.deltaTime;
		} else if (Controller.SlowMoValue < 1 && PlayerDead == false) {
			BackgroundController.TimeScale = 1f;
			SlowMo = false;
			Slider = Mathf.Lerp (Slider, 1, 15f * Time.deltaTime);
		}
}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "finish") {
			Controller.GoalCount += 1;
		}
		if (col.tag == "Enemy") {
			Destroy (col.gameObject);
			PlayerDead = true;
			BackgroundController.TimeScale = 1.1f;
			Controller.SendMessage("GameOverUI",SendMessageOptions.RequireReceiver);
		}
		
	}

	public void NextSkin(){ if(currentSkin < skins.Count-1 )currentSkin += 1; }
	public void lastSkin(){ if(currentSkin > 0 )currentSkin -= 1;}
	public void skinSelection(int Mode){ 

		if (Mode == 1) {//Salir
			currentSkin = LastSelectSkin;
		}
		if (Mode == 0) {//entering
			LastSelectSkin = currentSkin;
		}
//		if (Mode == 2) {
//
//		}
	}
}
