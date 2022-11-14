# insult_master_2d
### Recreating Monkey Island's insult sword fighting game.

### game demo:

[insult master demo (YT)](https://www.youtube.com/watch?v=MxZ12_gpjvw)

“””
The duel is divided into several rounds and each round is an insult and a response. The player who manages to win three rounds of the game wins. 

At the beginning of the duel, it is randomly chosen whose turn is to play. In our case, we have two opponents: the player and the computer. In each round, the player whose turn is yells an insult. In the case of the computer, the sentence must be random. 

In the original game, the player learns the insults and their responses as he fights other pirates. In our case, the player already has all the questions and answers learned and they will be presented to him every time he/she is ready to insult. Here you can find the insults from the original game (you can ignore the Sword Master's insults). 
“””

Scenes:
1. MenuScene
2. MainGameScene
3. EndGameScene


Scripts:
1. GameFiller.cs:
2. GameplayManager.cs:
3. DialogManager.cs
4. FinalScreenManager.cs
5. MenuScript.cs

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

## GameFiller.cs:

In GameFiller class two important methods are defined: 

** 1) Answers: **
Array of strings (contains only four elements for this game’s purposes) that holds comeback responses to the insults.


** 2) Insults: **
An array of Insult objects (also containing four elements). The Insult class objects are serialized from GameplayManager and each object has two properties insult_txt and correct_answer. Each insult has one single correct comeback (response) based on which the computer or user can score and win a round.


## GameplayManager.cs:

GameplayManager has many parts, I’m going to go through each briefly: 

<ins> ** Defining game variables: **  </ins> 
<br>
Here we define an Insult class and initialize arrays of type string (to be populated with Answers) and array of objects of type Insult (to be populated with insults and their correct answer). We then define a ‘current’ variable that points to the current insult/comeback in each iteration (choice made by user or program). We also have to define variables to keep score for user and computer and then load audios that we will play at the end of each round based on who scored. Finally, we initialize some UI objects that we will update throughout the game.

<ins> Start() / Initializing: </ins>
First thing we do is load Answers and Insults with string[] and Insult[] data types respectively. Then we grab relevant UI objects such as AnswersSection, ComputerResponseText, and computer/user score from the scene and store in their respective variables. The initial scores and also the current insult are set to zero and will be updated at the end of each round. Based on a random pick, the program decides which player can go first. 

If the user gets to go first, Fill_UI() will be called to populate the answers section with several insults options for the user to select from. The computer then randomly picks a comeback from answers. If the computer’s answer matches the correct answer for the current insult, the user loses and the ‘boo’ audio plays. Otherwise the user wins and the ‘applause’ audio plays. The player who wins gets to pick the insult in the next round of the game.

If the computer gets to go first, a random insult will be displayed (to which the user has to respond). And the game continues until either player scores three strikes.


<ins> Winning conditions: </ins>
In this part of the program we are ready to update scores and refill UI components after each round and finally define winning conditions.


<ins> Fill_UI: </ins>
Depending on who’s turn it is, we fill the UI component answers_parent with Answers or Insults data and make them clickable.

In the beginning I struggled with instantiating new GameObjects through prefabs and then filling in those objects, positioning them on the UI, adding events to them, and modifying their properties. I decided to get help from GameplayManager examples in open source projects on the internet that helped a lot with objects like Vector3, Quaternion and transformation.


