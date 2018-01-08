using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

  public GameObject greenBlockPrefab;

  public GameObject playerPrefab;
  public GameObject player;

  public int levelWidth = 9;
  // Number of blocks in a row; 9 in PSX version
  public int levelSubHeight = 6;
  // Number of rows below the player; 6 in PSX version
  public int levelTotalHeight = 12;
  // Number of rows on screen; 12 in PSX version
    
  Camera m_MainCamera;

  // Block dimensions in pixel units

  // Use this for initialization
  void Start ()
  {

    greenBlockPrefab.SetActive (false); // Deactivate prefab so it does not affect the scene

    playerPrefab.SetActive (false);

    // Spawn initial blocks
    for (int row = 0; row < levelSubHeight; row++) {
      for (int col = 0; col < levelWidth; col++) {
        Vector3 spawnPosition = new Vector3 (col, -row);
        GameObject block = Instantiate (greenBlockPrefab, spawnPosition, Quaternion.identity) as GameObject;
        block.SetActive (true);
      }
    }

    // Spawn player
    this.player = Instantiate (playerPrefab, new Vector3 (4, 1), Quaternion.identity);
    this.player.SetActive (true);
        
    m_MainCamera = Camera.main;
    m_MainCamera.GetComponent<CameraFollow>().target = this.player;
       
  }
	
  // Update is called once per frame
  void Update ()
  {
		
  }
}
