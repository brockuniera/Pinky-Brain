using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static float Difficulty = 1.0f;

	[Range(0.0f, 2.0f)]
	public float difficultyIncrease;

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
		GameObject[] clones = GameObject.FindGameObjectsWithTag("Explosion");
		foreach(GameObject t in clones)
		{
			Destroy(t.gameObject);
		}
	}

	public void advanceWave()
	{
		Difficulty += difficultyIncrease;
		soundManager.playSound ("WaveStart");
		waveCard.showWave (++wave);
	}
}