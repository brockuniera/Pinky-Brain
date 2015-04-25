using UnityEngine;
using System.Collections;

public class Metal : MonoBehaviour {
	public bool IsRandom = false;

	public float weight;

	public float Min;
	public float Max;


	void Start () {
		if(IsRandom)
			weight = Random.Range(Min, Max);
	}

}
