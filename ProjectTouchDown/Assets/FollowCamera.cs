using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	public float LimitX;

	Vector3 targetPos;


	// Use this for initialization
	void Start () {
		targetPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//

		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 7f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			if(targetPos.x > -LimitX && targetPos.x < LimitX) //Limit ScreenPosition Horizontally
			transform.position = Vector3.Lerp( new Vector3(transform.position.x,0,-10), new Vector3(targetPos.x,0,-10) + offset, 25f*Time.deltaTime*BackgroundController.TimeScale);
		}
	}

	void StartFunc(){
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 8.608795f, 2f * Time.smoothDeltaTime);
	}

	void SlowMoCam(){
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 9.14f, 5f * Time.smoothDeltaTime);
	}

	void NormalCam(){
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 8.608795f, 5f * Time.smoothDeltaTime);
	}

}


