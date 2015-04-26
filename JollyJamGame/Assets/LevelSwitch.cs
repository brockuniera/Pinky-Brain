using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "Player") {
			other.gameObject.GetComponent<move>().trans = true;
		}
	}
}
