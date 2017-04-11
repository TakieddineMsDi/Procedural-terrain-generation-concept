using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHUDManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SinglePlayerScene(){
		SceneManager.LoadScene ("Scenes/SinglePlayer");
	}
	public void MultiPlayerScene(){
		SceneManager.LoadScene ("Scenes/MultiPlayer");
	}
	public void HighSoresScene(){
		SceneManager.LoadScene ("Scenes/HighScores");
	}
	public void MainScene(){
		SceneManager.LoadScene ("Scenes/Main");
		if (GameObject.Find ("Network Manager")) {
			Destroy (GameObject.Find ("Network Manager").gameObject);
		} else {
			print ("No network manager here");
		}
	}
}
