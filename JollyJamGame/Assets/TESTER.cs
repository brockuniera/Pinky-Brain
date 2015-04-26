using UnityEngine;
using System.Collections;

public class TESTER : MonoBehaviour {

	private EnemySpawner _enemySpawner;

	// Use this for initialization
	void Awake() {
		_enemySpawner = GetComponent<EnemySpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_enemySpawner){
			if (Random.value < 0.01f)
				_enemySpawner.Generate(GameObject.FindWithTag("Player").transform, 1.0f);
		}
	}
}
