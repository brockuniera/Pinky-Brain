using UnityEngine;
using System.Collections;

public class TESTER : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value < 0.01f)
			Enemy.Generate (GameObject.Find ("pl").transform, 1.0f);
	}
}
