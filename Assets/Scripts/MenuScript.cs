using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	
	public void exitGame(){
		Debug.Log ("exitting");
		Application.Quit();
	}

	public void playGame() {
		SceneManager.LoadScene("MainGameScene");
	}
}
