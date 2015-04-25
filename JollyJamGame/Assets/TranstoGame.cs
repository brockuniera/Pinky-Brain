using UnityEngine;
using System.Collections;

public class TranstoGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			Debug.Log ("Something was pressed");
			Application.LoadLevel("level0");
		}
	}
}
