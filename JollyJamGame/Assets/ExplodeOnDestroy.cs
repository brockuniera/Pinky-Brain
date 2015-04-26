using UnityEngine;
using System.Collections;

public class ExplodeOnDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(this.gameObject);
	
	}

	void OnDestroy()
	{
		Instantiate(Resources.Load("Explode"),transform.position, transform.rotation);
	}
}
