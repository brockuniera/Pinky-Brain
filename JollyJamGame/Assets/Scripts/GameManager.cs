using UnityEngine;

public class GameManager : MonoBehaviour
{
	public SoundManager soundManager;

	public void Start()
	{
		Debug.Log (1);
		soundManager.loopSound ("Armored Theme");
	}
}