using UnityEngine;
using System.Collections;

public class ExplodeOnDestroy : MonoBehaviour {


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy()
	{
		Instantiate(Resources.Load("Explode"),transform.position, transform.rotation);

	}
}
