using UnityEngine;
using System.Collections;

public class GemScript : MonoBehaviour {
	public Transform effectPrefab;

	//on Object death...


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			var prefab = Instantiate (effectPrefab, transform.position, effectPrefab.rotation) as Transform;
			prefab.parent = this.transform.parent;
		}
	}
}
