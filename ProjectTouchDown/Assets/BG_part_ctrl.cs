using UnityEngine;
using System.Collections;

public class BG_part_ctrl : MonoBehaviour {
	public BG_part_ctrl belowPart;
	public Transform UpPart;

	public float Speed = 4f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (belowPart) {
			transform.position = belowPart.UpPart.position;
		} else {
			if (BackgroundController.TimeScale != 1.1f)
				transform.Translate (0, -Speed * Time.deltaTime * BackgroundController.TimeScale, 0);
		}


		if (transform.localPosition.y < -61.1)
			Destroy (this.gameObject);
	}
}
