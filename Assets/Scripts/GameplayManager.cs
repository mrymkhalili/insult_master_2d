using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameplayManager : MonoBehaviour {

// %%%%%%%%%%%% GAME VARIABLES %%%%%%%%%%%%
	[System.Serializable]
	// an object of class insult has a insult_txt and a correct answer based on which the computer/user wins a round
	// we need to serialize the object so that we can access it from GameFiller
	public class Insult {
		public string insult_txt;
		public int correct_answer;
	}

	private string[] answers; // array to hold answers
	private Insult[] insults; // array to hold insults
	private int current; // current insult
	private int user_score, cmp_score; // user vs computer score

	// UI objects
	public GameObject answers_parent; // user's insult/answer container
	public Text cmp_text;  // computer's insult/answer container
	public Text user_score_text, cmp_score_text;  // text that displays user vs computer score

	// audio sources
	private AudioSource audio_source;
	private AudioClip user_win_audio;
	private AudioClip cmp_win_audio;

// %%%%%%%%%%%% INITIALIZING/START %%%%%%%%%%%%

	void Start () {
		// grab the gameObjects from the scene
		answers_parent = GameObject.Find("AnswersSection");
		cmp_text = GameObject.Find("ComputerResponseText").GetComponent<Text>();

		cmp_score_text = GameObject.Find("ComputerScore").GetComponent<Text>();
		user_score_text = GameObject.Find("UserScore").GetComponent<Text>();

		// load answers & insults
		answers = GameFiller.Answers();
		insults = GameFiller.Insults();

		// audio variables
		audio_source = GetComponent<AudioSource>();
		cmp_win_audio = Resources.Load("boo") as AudioClip;
		user_win_audio = Resources.Load("applause") as AudioClip;

		// game variables - initializing the scores and the current index to zero
		user_score = 0;
		cmp_score = 0;
		current = 0;

		// randomly select who goes first, user or program
		int first_round = Random.Range(0, 1);
		Debug.Log(first_round);

		if (first_round == 1) {
			Debug.Log ("player goes first");
			// give the user a range of insults to choose from
			// Fill_Insults();
			Fill_UI(0);
		} else {
			Debug.Log ("computer goes first");
			// computer randomly chooses an insult
			StartCoroutine(SetRandomInsult ());
		}
	}

// %%%%%%%%%%%% MISCELLANEOUS %%%%%%%%%%%%

	// when user selects an answer (to program's insult)
	private void AnswerSelected(int index) {
		if (insults[current].correct_answer == index) {
			PlayerWinsRound ();
		} else {
			ComputerWinsRound ();
		}
	}

	// when user selects an insult, computer picks a random answer - if the answer matches the insult computer wins
	private void InsultSelected(int index) {
		current = index;
		StartCoroutine(GetRandomAnswer());
	}

	private IEnumerator SetRandomInsult(){
		yield return new WaitForSeconds(1);

		current = Random.Range(0, insults.Length);
		cmp_text.text = insults[current].insult_txt;
		Fill_UI(1); // insutls 0, answers 1
	}


	private IEnumerator GetRandomAnswer(){
		yield return new WaitForSeconds(1);

		int answerIndex = Random.Range (0, answers.Length);
		cmp_text.text = answers[answerIndex];

		yield return new WaitForSeconds(1);

		if (insults[current].correct_answer == answerIndex) {
			ComputerWinsRound ();
		} else {
			PlayerWinsRound ();
		}
	}

// %%%%%%%%%%%% WINNING CONDITIONS %%%%%%%%%%%%

	// computer scores a point every round it wins. if it wins three rounds the user loses.
	private void ComputerWinsRound(){
		cmp_score++;

		cmp_score_text.text = "PROGRAM: " + cmp_score.ToString();
		user_score_text.text = "YOU: " + user_score.ToString();

		audio_source.PlayOneShot(cmp_win_audio);

		if (cmp_score >= 3) {
			PlayerPrefs.SetString("winner", "computer");
			SceneManager.LoadScene ("EndGameScene");
			return;
		}
		StartCoroutine (SetRandomInsult());
		current = Random.Range (0, insults.Length);
		cmp_text.text = insults[current].insult_txt;
		Fill_UI(1);
	}

	// user scores a point every round it wins. if it wins three rounds it wins.
	private void PlayerWinsRound(){
		user_score++;

		cmp_score_text.text = "PROGRAM: " + cmp_score.ToString();
		user_score_text.text = "YOU: " + user_score.ToString();

		audio_source.PlayOneShot(user_win_audio);

		if (user_score >= 3) {
			PlayerPrefs.SetString("winner", "player");
			SceneManager.LoadScene ("EndGameScene");

			// Debug.Log ("player wins");
			return;
		}
		cmp_text.text = "...";
		// 0 for insults, 1 for answers
		Fill_UI(0);

	}

// %%%%%%%%%%%% Fill_Ui() %%%%%%%%%%%%
	private void Fill_UI(int choice) {
		// wipe previous options/answers
		foreach (Transform child in answers_parent.transform) {
			Destroy (child.gameObject);
		}

		float height = answers_parent.GetComponent<RectTransform>().rect.yMax - 20;

		for (int i = 0; i < 4; i++) {
			// ResponseButton is a prefab - we can instantiate new objects
			GameObject answerButton =(GameObject) Instantiate (Resources.Load("ResponseButton"), new Vector3(0,height), new Quaternion(0,0,0,0));

			// attach the prefab to the answers container/parent
			answerButton.transform.SetParent(answers_parent.transform, false);

			FillButton(answerButton, i, choice);
			height += answerButton.GetComponent<RectTransform> ().rect.y - 20;
		}
	}

	private void FillButton(GameObject answerButton, int index, int choice){
		Text textUi = answerButton.GetComponentInChildren<Text> ();
		if (choice == 0) {
			textUi.text = insults[index].insult_txt;
		} else {
			textUi.text = answers[index];
		}

		answerButton.GetComponent<Button> ().onClick.AddListener (() => {
			if (choice == 0) {
				InsultSelected(index);
			} else {
				AnswerSelected(index);
			}
			textUi.color = Color.green;
		});
	}
}