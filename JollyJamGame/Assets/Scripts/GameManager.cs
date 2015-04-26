using UnityEngine;

public class GameManager : MonoBehaviour
{
	public SoundManager soundManager;

	public void Start()
	{
		soundManager.loopSound ("Armored Theme");
	}
}