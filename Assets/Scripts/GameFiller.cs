using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

public class GameFiller{
	public static string[] Answers() {
		string[] answers = {
			"And I've got a little TIP for you, get the POINT?",
			"Even BEFORE they smell your breath?",
			"First you'd better stop waving it like a feather-duster.",
			"He must have taught you everything you know."
		};
		return answers;
	}

	public static GameplayManager.Insult[] Insults(){

		GameplayManager.Insult insult1 = new GameplayManager.Insult();
		insult1.insult_txt = "This is the END for you, you gutter-crawling cur!";
		insult1.correct_answer = 0;

		GameplayManager.Insult insult2 = new GameplayManager.Insult();
		insult2.insult_txt = "People fall at my feet when they see me coming.";
		insult2.correct_answer = 1;

		GameplayManager.Insult insult3 = new GameplayManager.Insult();
		insult3.insult_txt = "Soon you'll be wearing my sword like a shish kebab!";
		insult3.correct_answer = 2;


		GameplayManager.Insult insult4 = new GameplayManager.Insult();
		insult4.insult_txt = "I once owned a dog that was smarter then you.";
		insult4.correct_answer = 3;

		GameplayManager.Insult[] insults = {
			insult1, insult2, insult3, insult4
		};
		return insults;
	}
}

