// AUTHOR: Aaron Braten
// FILENAME: Driver.cs
// DATE: 4/29/18
// REVISION HISTORY: v.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPSC5051_P1
{
    class Driver
    {
        public bool isCorrect = false, isValid = false;
        public int key, high, low;
        public double avg;
        public char playAgain;
        public string word, cipherText, decryptedWord, input;
        const int MIN = 4;

        //short welcome printed at the start of the program
        public void Welcome()
        {
            Console.WriteLine("\nThis is a short game where you can guess " +
                "an encrypted\nword's Caeser cipher shift value.\n ");
        }

        //plays the game with the user
        public void playGame()
        {
            Welcome();

            //prompt for input
            Console.Write("Type a string to encrypt (at least" +
                " 4 characters long): ");
            word = Console.ReadLine();
            //validate input length
            while (word.Length < MIN)
            {
                Console.Write("\nToo short! Enter a string at least 4 " +
                    "characters long: ");
                word = Console.ReadLine();
            }
            Console.WriteLine("\n");
            EncryptWord encryptWord = new EncryptWord(word);
            cipherText = encryptWord.GetCipher();
            Console.WriteLine("Cipher text: " + cipherText);
            Console.WriteLine("\n");
            //prompt for key
            Console.Write("What is the cipher key? ");
            input = Console.ReadLine();

            //validate input
            isValid = int.TryParse(input, out key);
            while (!isValid)
            {
                Console.Write("\nEnter a valid integer: ");
                input = Console.ReadLine();
                isValid = int.TryParse(input, out key);
            }

            encryptWord.AddToList(key);
            isCorrect = encryptWord.CheckGuess(key);
            while(!isCorrect)
            {
                Console.WriteLine("\nThat is incorrect. \nHere are your " +
                    "statistics:");
                high = encryptWord.GetHighCount();
                low = encryptWord.GetLowCount();
                avg = encryptWord.GetAverageGuess();
                Console.WriteLine("High guesses: " + high + " Low guesses: " 
                    + low + " Avg: " + avg);
                Console.WriteLine("\n");
                Console.Write("What is the cipher key? ");
                input = Console.ReadLine();
                //validate input
                isValid = int.TryParse(input, out key);
                while (!isValid)
                {
                    Console.Write("Enter a valid integer: ");
                    input = Console.ReadLine();
                    isValid = int.TryParse(input, out key);
                }
                encryptWord.AddToList(key);
                isCorrect = encryptWord.CheckGuess(key);
            }
            Console.WriteLine("That's correct! Nice Guess!");
            decryptedWord = encryptWord.Decrypt();
            //Console.WriteLine("Decrypted: " + decryptedWord);
            encryptWord.Reset();
            PlayAgain();
        }

        //ask if they want to play again
        public void PlayAgain()
        {
            Console.Write("\nPlay again? ");
            playAgain = Console.ReadKey().KeyChar;
            if (playAgain == 'y' || playAgain == 'Y')
                playGame();
            else
                Farewell();
        }

        //short farewell
        public void Farewell()
        {
            Console.WriteLine("Thanks for playing!");
        }
    }
}
