using UnityEngine;
using System.Collections;

public class ExplodeOnDestroy : MonoBehaviour {

	public GameObject _explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy()
	{
		Instantiate(_explosion,transform.position, transform.rotation);
	}
}
