using UnityEngine;
using System.Collections;

public class Metal : MonoBehaviour {
	public float weight;
	public float min;
	public float max;
	// Use this for initialization
	void Start () {
		weight = Random.Range (min, max);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
