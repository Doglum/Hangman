using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //loads all words in dictionary.txt into a list:
            StreamReader sr = new StreamReader("dictionary.txt");
            List<string> allWords = new List<string>();
            while (!sr.EndOfStream)
            {
                allWords.Add(sr.ReadLine());
            }
            sr.Close();

            //picks a random word from the list
            Random random = new Random();
            string word = allWords[random.Next(allWords.Count - 1)];

            //variable declarations
            int guesses = 0;
            char guessedChar = '0';
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            List<char> remainingAlphabet = alphabet.ToList();
            string guessedWord = "";
            for (int i = 0; i < word.Length; i++) //creates string of hyphens with same length as word
            {
                guessedWord += '-';
            }

            //main gameplay loop, breaks when all characters guessed:
            while(guessedWord.Contains('-'))
            {
                //displays info for user and gets input
                Console.WriteLine();
                Console.WriteLine(guessedWord);
                Console.WriteLine("Enter a letter:");
                guessedChar = Console.ReadLine()[0];

                //if the character hasn't already been guessed:
                if (remainingAlphabet.Contains(guessedChar))
                {
                    //remove the character from the remaining alphabet and add a guess
                    remainingAlphabet.Remove(guessedChar);
                    guesses += 1;

                    //if guess is accurate:
                    if (word.Contains(guessedChar))
                    {
                        //fill in the blanks for that letter:
                        StringBuilder str = new StringBuilder(guessedWord);
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == guessedChar)
                            {
                                str[i] = guessedChar;
                            }
                        }
                        guessedWord = str.ToString();
                        Console.WriteLine("Word contains " + guessedChar);
                    }
                    //if the guess is inaccurate:
                    else
                        Console.WriteLine(guessedChar + " is not in the word");
                }
                //if the letter has already been guessed
                else
                {
                    if (alphabet.Contains(guessedChar))
                        Console.WriteLine("You've already guessed " + guessedChar);
                    else
                        Console.WriteLine(guessedChar + " is invalid");
                }  
            }

            //endgame results
            Console.WriteLine(word);
            Console.WriteLine("You took " + guesses.ToString() + " guesses");
            Console.ReadLine();
        }
    }
}
