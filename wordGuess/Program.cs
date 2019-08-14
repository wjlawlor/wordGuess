using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wordGuess
{
    class Program
    {
        /// <summary>
        /// Main. Calls a ton of methods.
        /// </summary>
        static void Main()
        {
            // 2B
            List<string> phrases = GetPhrases();

            // 3B
            PlayGame(phrases);

            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Store phrases to play the game with. Has to avoid anything but A-Z for now.
        /// </summary>
        /// <returns>A list of phrases.</returns>
        private static List<string> GetPhrases()
        {
            List<string> puzzles = new List<string>();
            puzzles.Add("Dread it run from it destiny still arrives");
            puzzles.Add("Mr Stark I dont feel so good");
            puzzles.Add("I am Iron Man");

            return puzzles;
        }

        /// <summary>
        /// Picks a random phrase out of the list of phrases.
        /// </summary>
        /// <param name="phrases">Predefined Phrases</param>
        /// <returns>A random phrase out of a list of phrases.</returns>
        private static string SelectPhrase(List<string> phrases)
        {
            var random = new Random();
            var randomInteger = random.Next(0,phrases.Count);

            string selectedPhrase = phrases[randomInteger].ToUpper();

            return selectedPhrase;
        }

        /// <summary>
        /// The main method from which the game process runs.
        /// </summary>
        /// <param name="phrases">The phrase that is being used to play the game.</param>
        private static void PlayGame(List<string> phrases)
        {
            // Win Condition
            const int totalGuesses = 5;
            int failedGuesses = 0;

            // Intro
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Here is your puzzle:");
            Console.WriteLine();

            // Selects Random Phrase
            string selectedPhrase;
            selectedPhrase = SelectPhrase(phrases);

            char[] charArray = GetPhraseCharacters(selectedPhrase);

            HashSet<char> uniqueChars = GetPhraseDistinctCharacters(charArray);
            var correctChars = new HashSet<char>();

            List<char> phraseGuessedCharacters = new List<char>();

            char guessedChar;

            while (totalGuesses != failedGuesses)
            {
                if (uniqueChars.Count == correctChars.Count)
                {
                    Console.Clear();
                    DisplayPhrase(charArray, phraseGuessedCharacters);
                    Console.Write("You Won!");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                DisplayPhrase(charArray, phraseGuessedCharacters);
                guessedChar = GetCharacterGuess(phraseGuessedCharacters);

                if (uniqueChars.Contains(guessedChar))
                {
                    correctChars.Add(guessedChar);
                    Console.WriteLine($"You got one. Guess another! You have {totalGuesses-failedGuesses} guesses remaining.");
                    Console.WriteLine();
                }
                else
                {
                    failedGuesses++;
                    Console.WriteLine($"Sorry, {guessedChar} isn't part of the answer. You have {totalGuesses-failedGuesses} guesses remaining.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("You lost. Better luck next time!");
        }

        /// <summary>
        /// Puts the characters from the selected phrase (a string) into a character array.
        /// </summary>
        /// <param name="phrase">A string containing the phrase the game is using the play.</param>
        /// <returns>A char array with the phrase the game is using to play the game.</returns>
        private static char[] GetPhraseCharacters(string phrase)
        { 
            char[] charArray = phrase.ToCharArray();

            return charArray;
        }

        /// <summary>
        /// Method to get the unique characters from the phrase for two reason: a win condition on the game and to check guessed characters against.
        /// </summary>
        /// <param name="charArray">A char array with the phrase the game is using to play the game.</param>
        /// <returns>A HashSet with the unique letters in the phrase.</returns>
        private static HashSet<char> GetPhraseDistinctCharacters(char[] charArray)
        {
            HashSet<char> uniqueChars = new HashSet<char>(charArray);

            if (uniqueChars.Contains(' '))
            {
                uniqueChars.Remove(' ');
            }

            return uniqueChars;
        }

        /// <summary>
        /// Prints the Phrase on the Console, with guessed letters shown.
        /// </summary>
        /// <param name="phraseCharacters">The characters in the phrase.</param>
        /// <param name="phraseGuessedCharacters">The characters which the user has guessed.</param>
        private static void DisplayPhrase(char[] phraseCharacters, List<char> phraseGuessedCharacters)
        {
            foreach(char value in phraseCharacters)
            {
                if (phraseGuessedCharacters.Contains(value))
                {
                    Console.Write(value);
                }
                else if(char.IsWhiteSpace(value))
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.Write('-');
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Method called to prompt the user to input thier character guess. Asks for another input if it is not an A-Z char.
        /// </summary>
        /// <param name="guessedCharacters">The characters previously guessed by the user.</param>
        /// <returns>The character the user has guessed, if it is originial.</returns>
        private static char GetCharacterGuess(List<char> guessedCharacters)
        {
            Console.WriteLine("Enter a character to guess!");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                char guessedChar = char.ToUpper(key.KeyChar);

                if (guessedCharacters.Contains(guessedChar))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Already guessed that character!");
                }
                else if ((key.KeyChar >= 'a' && key.KeyChar <= 'z') || (key.KeyChar >= 'A' && key.KeyChar <= 'Z'))
                {                  
                    guessedCharacters.Add(guessedChar);
                    Console.Clear();

                    return guessedChar;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Not a valid character, please enter a letter.");
                }
            } 
        }
    }
}
