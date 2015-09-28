using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnScript : MonoBehaviour {
	public float MaxX = 5f;

	public List<Transform> enemyPrefab = new List<Transform>();
	//public Transform EnemyPrefab;

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

		int Max = 0;
		if(controller.GoalCount > 1)
			Max = Random.Range(0,enemyPrefab.Count);

		Transform Prefab = enemyPrefab[Random.Range(0,Max)];

		var newenemy = Instantiate (Prefab, new Vector3 (transform.position.x + Random.Range (-MaxX, MaxX), transform.position.y, 0), transform.rotation) as Transform;
		newenemy.parent = BGC.Lastinastance.transform;
	}

	void StartGame(){
		CancelInvoke ("InstantiateEnemy");
		InvokeRepeating ("InstantiateEnemy", 1, 1);
	}
}
