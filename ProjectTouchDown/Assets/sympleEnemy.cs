using UnityEngine;
using System.Collections;

public class sympleEnemy : MonoBehaviour {
	public Transform PlayerTarget;

	public float Speed = 4;
	public float minDistance = 1.5f;
	//public float DeltaSpeed = 0f;


	private Vector3 v_diff;
	private float atan2;
	private bool canRotate = true;
	
	// Use this for initialization
	void Start () {
		PlayerTarget = GameObject.FindObjectOfType<PlayerController> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (transform.up*Speed) * Time.deltaTime * BackgroundController.TimeScale;//Vector3(0,DeltaSpeed * Time.deltaTime * BackgroundController.TimeScale,0);



		if (PlayerTarget && canRotate == true) {

			if (Vector2.Distance (new Vector2(PlayerTarget.position.x,PlayerTarget.position.y), new Vector2(transform.position.x,transform.position.y)) < minDistance)
				canRotate = false;

		//	transform.position = Vector3.MoveTowards( transform.position, PlayerTarget.position, DeltaSpeed*Time.deltaTime*BackgroundController.TimeScale);
			v_diff = (PlayerTarget.position - transform.position);    
			atan2 = Mathf.Atan2 ( v_diff.y, v_diff.x )-190;
			transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg );
		}
	}
}
