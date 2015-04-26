using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveCard : MonoBehaviour {

	[SerializeField]
	private Image waveText;

	[SerializeField]
	private Image waveBackground;

	[SerializeField]
	private Sprite[] waveImages;

	private Animator animator;

	public void Awake()
	{
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		waveBackground.transform.Rotate (new Vector3 (0.0f, 0.0f, 1.0f));
	}

	public void showWave(int wave)
	{
		if(wave <= waveImages.Length)
			waveText.sprite = waveImages [wave - 1];

		animator.SetTrigger ("wave");
		Debug.Log ("ASdas");
	}
}
