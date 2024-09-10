//Welcome to Hangman!
//Louai AL Jabi
//CMP220
//Application Assignment #2

Console.WriteLine(
    """
    Welcome to Hangman!

    The rules to this game are simple, you are presented with a blank word and you have to guess, one letter
    at a time, what that word is. You get 6 guesses before you're "hanged" or in this case you just lose.
    """
    );

Hangman.Run();

//Establishing a loop that determines if the player wants to play again.
Console.WriteLine("\nIf you wish to play again, press any key. If you wish to quit, press ENTER");
while (Console.ReadKey(true).Key != ConsoleKey.Enter)
{
    Hangman.Run();
}

public class Hangman
{
    static public void Run()
    {
        //The method runs the game using methods from other classes.
        //Asking the player for his name, and prompting them to start the game.
        Console.WriteLine("\nPlease enter your name to procede:");
        string name = Console.ReadLine();
        Console.WriteLine($"Okay, {name}, press any key to start.");
        Console.ReadKey(true);
        //Establish a number of guesses the player has. 
        int guesses = 6;
        //Establish an empty list to store future wrong guesses.
        List<char> incorrect = new List<char>();
        //Get a list of characters from the wordgenerator method.
        List<char> word = Word.wordgenerator();
        //establish a a list of "holding spaces" that updates with every guess, aka the state of the game.
        List<char> empty = new List<char> { '_', '_', '_', '_', '_', '_', '_' };
        Console.WriteLine($"Word: {string.Join(" ", empty)} | Remaining: {guesses} | " +
        $"Incorrect: {string.Join(" ", incorrect)} | Guess: ");
        //Establish a while loop that runs until the player guesses the word correctly or runs out of guesses.
        while (guesses != 0)
        {
            //Getting the user's entry throut the Entry method in Player.
            char entry = Player.Entry();
            //Checking if the player had entered their entry already.
            if (empty.Contains(entry))
            {
                Console.WriteLine("You already guessed this letter!");
            }
            else
            {
                //If the entry is in the word list, then update the gamestate with the entry.
                //If it's not then add the wrong guess into the incorrect list and subtract a guees from
                //the player.
                if (word.Contains(entry))
                {
                    for (int j = 0; j < word.Count; j++)
                    {
                        if (word[j] == entry) empty[j] = entry;
                    }
                }
                else
                {
                    incorrect.Add(entry);
                    guesses--;
                }
            }
            //Use the Display method from Word to display the gamestate, the entry, the list of incorrect
            //guesses, and the remaining number of guesses.
            Console.WriteLine(Word.Display(incorrect, entry, empty, guesses));
            if (!empty.Contains('_')) break;
        }
        Console.WriteLine(Player.Score(name, empty, guesses,word));
    }
}

public class Word
{
    //Establish a wordgenerator method which goes through a dictionary of words, and returns a char list
    //consisting of the chosen word in character form.
    static public List<char> wordgenerator()
    {
        //Dictionary of random words
        string[] WordDictionary = { "adjunct", "jacuzzi", "quickly", "jaywalk", "cumquat","genetic","lobster"+
        "science","healthy","skyline"};
        //Using a random number generator, randomly choose a word.
        Random rng = new Random();
        string word = WordDictionary[rng.Next(1, 10)];
        //Establish an empty list which will hold the word as characters
        List<char> finalword = new List<char>();
        //Loop through the word and add each letter seperatly into the list
        foreach (char letter in word)
        {
            finalword.Add(letter);
        }
        return finalword;
    }
    //Establish a method which displays the gamestate, the entry, the list of incorrect
    //guesses, and the remaining number of guesses.
    static public string Display(List<char> incorrect, char entry, List<char> empty, int guesses)
    {
        return $"Word: {string.Join(" ", empty)} | Remaining: {guesses} " +
            $"| Incorrect: {string.Join(" ", incorrect)} | Guess: {entry}";
    }
}

public class Player
{
    //Establish a method that asks the player for an entry and makes sure the entry is suitable
    //to continue the game.
    static public char Entry()
    {
        string playerinput = Console.ReadLine();
        //Make sure the player enters just one letter.
        while (playerinput.Length != 1)
        {
            Console.WriteLine("Please enter a single letter!");
            playerinput = Console.ReadLine();
        }
        //Switching the one entered letter to a character.
        char entry = char.Parse(playerinput);
        //If the letter is in an upper case letter, switch to lower case.
        if (!char.IsLower(entry))
        {
            entry = char.ToLower(entry);
        }
        return entry;
    }
    //Establish a method that checks the player has performed, and returns
    //that status.
    static public string Score(string name, List<char> empty, int guesses, List<char> word)
    {
        //Checks if the player had guessed the word successfully.
        if (!empty.Contains('_'))
        {
            //Returns a message based on how many gueeses left for the player before winning.
            if (guesses == 6) return $"\n{string.Join("", word)}!\nWOW! {name}, you guessed the word without " +
                    $"any wrong guesses!";
            else if (3 <= guesses && guesses < 6) return $"\n{string.Join("", word)}!\nAdmerable " +
                    $"performance {name}!";
            else return $"\n{string.Join("", word)}!\nGood job, {name}!";
        }
        //If the player didn't win, return a sad response :(
        else return $"Bad luck, {name} :(\nThe word was {string.Join("",word)}.";
    }   
}