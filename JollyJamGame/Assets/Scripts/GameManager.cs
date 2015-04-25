using UnityEngine;

public class GameManager : MonoBehaviour
{
	public SoundManager soundManager;

	[SerializeField]
	private WaveCard waveCard;

	[HideInInspector]
	public int wave;

	public void Awake()
	{
		wave = 0;
	}

	public void Start()
	{
		soundManager.loopSound ("Armored Theme");
	}

	public void advanceWave()
	{
		waveCard.showWave (++wave);
	}
}