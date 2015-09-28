using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour {
	public float MaxX = 5f;
	public Transform EnemyPrefab;
	public BackgroundController BGC;


	private GameController controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindObjectOfType<GameController> ();
		//InvokeRepeating ("InstantiateEnemy", 1, 1);
	}
	
 	void InstantiateEnemy(){
		if (BGC.Lastinastance.transform) {
			var Max = 1;
			if(controller.GoalCount == 1)
				Max = 2;
			if(controller.GoalCount > 3)
				Max = 3;


			for(var i = 0; i<Random.Range(1,Max);i++){
				StartCoroutine("instantiateEnemy");
			}

		}
	}

	IEnumerator instantiateEnemy(){
		yield return new WaitForSeconds (Random.Range(1f,2f));
		var newenemy = Instantiate (EnemyPrefab, new Vector3 (transform.position.x + Random.Range (-MaxX, MaxX), transform.position.y, 0), transform.rotation) as Transform;
		newenemy.parent = BGC.Lastinastance.transform;
	}

	void StartGame(){
		CancelInvoke ("InstantiateEnemy");
		InvokeRepeating ("InstantiateEnemy", 1, 1);
	}
}
