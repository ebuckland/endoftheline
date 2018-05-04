using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	public void startGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void enterControls()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
	}

	public void exitControls()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
	}

	public void mainMenu()
	{
		FindObjectOfType<Score_Controller> ().resetScore ();

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-3);
	}
}
