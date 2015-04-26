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
		soundManager.loopSound ("Armored Theme", 0.2f);
	}

	public void advanceWave()
	{
		soundManager.playSound ("WaveStart");
		waveCard.showWave (++wave);
	}
}