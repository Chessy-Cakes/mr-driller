using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartGame : MonoBehaviour {

  public void Restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
  }

}