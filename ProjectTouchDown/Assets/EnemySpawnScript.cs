using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour {
	public float MaxX = 5f;
	public Transform EnemyPrefab;
	public BackgroundController BGC;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("InstantiateEnemy", 1, 1);
	}
	
 	void InstantiateEnemy(){
		if (BGC.Lastinastance.transform) {
			for(var i = 0; i<Random.Range(0,4);i++){
				instantiateEnemy();
			}

		}
	}

	void instantiateEnemy(){
		var newenemy = Instantiate (EnemyPrefab, new Vector3 (transform.position.x + Random.Range (-MaxX, MaxX), transform.position.y, 0), transform.rotation) as Transform;
		newenemy.parent = BGC.Lastinastance.transform;
	}

	void StartGame(){
		CancelInvoke ("InstantiateEnemy");
		InvokeRepeating ("InstantiateEnemy", 1, 1);
	}
}
