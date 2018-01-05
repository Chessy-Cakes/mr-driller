﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;

	public int numberOfPlatforms;
	public float levelWidth = 3f;
	public float minY = .2f;
	public float maxY = 1.5f;

	// Use this for initialization
	void Start () {
		Vector3 spawnPosition = new Vector3();
		for (int i = 0; i < numberOfPlatforms; i++) {
			spawnPosition.x += Random.Range(-levelWidth, levelWidth);
			spawnPosition.y += Random.Range(minY, maxY);
			Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
