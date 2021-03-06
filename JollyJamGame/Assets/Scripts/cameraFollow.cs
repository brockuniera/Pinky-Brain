﻿using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public float XOffset;
	public float YOffset;

	private GameObject _player;
	private float _zpos;
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");
		_zpos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pose = _player.transform.position;
		pose.x += XOffset;
		pose.y += YOffset;
		pose.z = _zpos;
		this.transform.position = pose;
	}
}
