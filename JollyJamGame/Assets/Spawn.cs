using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject[] _spawnTypes;
	public float[] _spawnPercents;
	public int _numSpawns = 10;
	public int _maxSize = 1;

	private int[] _xArray;
	private int[] _yArray;

	public bool _spawnOverTime = false;

	private bool _fail = false;

	private Vector2[] _positions;

	private float _timer = 10;

	// Use this for initialization
	void Start () {
		SpawnStuff();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(_spawnOverTime)
		{
			_timer -= Time.deltaTime;
		}
		if(_timer <= 0)
		{
			SpawnStuff();
			_timer = 15;
		}

	}

	void SpawnStuff()
	{
		
		_xArray = new int[(int)transform.localScale.x / _maxSize];
		_yArray = new int[(int)transform.localScale.y / _maxSize];
		_positions = new Vector2[_numSpawns];
		
		for(int j = 0; j < _spawnTypes.Length; j++)
		{
			for(int i = 0; i < _spawnPercents[j] * _numSpawns; i++)
			{
				for(int xs = 0; xs < _xArray.Length; xs++)
				{
					_xArray[xs] = (int)(transform.position.x - transform.localScale.x/2f + xs);
				}
				for(int ys = 0; ys < _yArray.Length; ys++)
				{
					_yArray[ys] = (int)(transform.position.y - transform.localScale.y/2f + ys);
				}
				int _x = Random.Range(0,_xArray.Length);
				int _y = Random.Range(0,_yArray.Length);
				for(int k = 0; k < _positions.Length; k++)
				{
					Vector2 newPos = new Vector2(_x,_y);
					if(Vector2.Equals(newPos,_positions[k]))
					{
						_fail = true;
					}
				}
				if(!_fail)
				{
					_positions[i*j].x = _x;
					_positions[i*j].y = _y;
					Vector3 pos = new Vector3(_xArray[_x],_yArray[_y],0);
					Instantiate(_spawnTypes[j],pos,transform.rotation);
					Debug.Log(pos);
				}
				else
				{
					i--;
					_fail = false;
				}
			}
		}

	}
}
