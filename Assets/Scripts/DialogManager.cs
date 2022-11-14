using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour {
	public void backToMenu(){
		SceneManager.LoadScene ("MenuScene");
	}

	public void playAgain() {
		SceneManager.LoadScene ("MainGameScene");
	}

	public static void ShowEndDialog(bool playerWon, string gameObjectReference){
		GameObject dialogEndGame =(GameObject) Instantiate (Resources.Load("DialogEndGame"), new Vector3(0,0), new Quaternion(0,0,0,0));

		dialogEndGame.transform.SetParent(GameObject.Find(gameObjectReference).transform, false);
		string result;
		if (playerWon) {
			result = "you won :>";
		} else {
			result = "you lost :(";
		}
		dialogEndGame.GetComponentInChildren<Text> ().text = result;
	}
}
