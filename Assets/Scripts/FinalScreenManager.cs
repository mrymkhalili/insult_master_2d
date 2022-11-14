using UnityEngine;
using System.Collections;

public class FinalScreenManager : MonoBehaviour {
	void Start () {
		string winner = PlayerPrefs.GetString ("winner");
		DialogManager.ShowEndDialog (winner.Equals("player"), "Canvas");
	}
}
