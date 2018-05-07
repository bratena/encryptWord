// AUTHOR: Aaron Braten
// FILENAME: EncryptWord.cs
// DATE: 4/16/18
// REVISION HISTORY: v.2


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPSC5051_P1
{

    // Description:  
    // Class contains a constructor which sets values to zero and sets the shift. 
    // Initial state is "off"; to turn "on" need to call encrypt method. To 
    // "reset" call the reset function. Calling the encrypt 
    // function encrypts the string with a Caesar cipher. 
    // Decrypt function decrypts the word. Object can only be on or off 
    // Requires a valid string of at least four characters to create an object. 
    // If too short user is prompted to try again. 
    // Anticipated use by Application programmer for elementary students. 
    // Only strings and ints are processed to complete encryption. 
    // A list is used to determine average guesses. Guesses should be valid ints 
    // between 1 and 25 ints outside that range will still be accepted and user 
    // will be shown if their guess is too high or too low. 
    // Assumptions:  
    // EncryptWord object needs to be created using a the constructor, initial 
    // state is off, after encrypt is called state is on. 
    // Reset may be called to reset values
    // Any number of EncryptWord objects can be created. 
    class EncryptWord
    {
        //constants (invariants) used through the program
        const int MIN_RANGE = 1;
        const int MAX_RANGE = 25;
        const int MIN_STRING = 4;
        const int ALPHEBET_RANGE = 26;

        private string plainText, cipherText;
        private int shift, highCount, lowCount, averageGuess;
        private List<int> guesses = new List<int>();
        private bool on;



        // description: creates an object using the text passed in  
        // precondition: no object with that name exists, there is enough memory 
        // available string is at least 4 characters 
        // postcondition: shift will be set and all other values set to null or 0 
        public EncryptWord(string input)
        {
            plainText = input;
            shift = SetShift();
            highCount = 0;
            lowCount = 0;
            averageGuess = 0;
            on = false;
            cipherText = Encrypt(input, shift);
        }

        // description: encrypts using ALPHEBET_RANGE
        // precondition:EncryptWord object has been created. 
        // postcondition: encrypted char is returned to the calling function 
        public char Cipher(char c)
        {
            if (!char.IsLetter(c))
            {
                return c;
            }
            char d = char.IsUpper(c) ? 'A' : 'a';
            return (char)((((c + shift) - d) % ALPHEBET_RANGE) + d);
        }

        // description: calls helper function Cipher to encrypt each char
        // precondition: valid string of at least 4 chars and EncryptWord object has 
        // been created. 
        // postcondition: encrypted string is returned to the calling function 
        private string Encrypt(string input, int key)
        {
            string cipherText = string.Empty;

            foreach (char ch in input)
                cipherText += Cipher(ch);
            if(on == false)
                SetOn();
            return cipherText;
        }

        // description: calls the Encrypt function with the Range minus the shift
        // to decrypt each char
        // precondition: valid string of at least 4 chars and EncryptWord object has 
        // been created. 
        // postcondition: decrypted string is returned to the calling function 
        public string Decrypt()
        {
            if (on == true)
                SetOff();
            return Encrypt(cipherText, ALPHEBET_RANGE - shift);
        }

        // description: returns the average guess calling the Average function
        // precondition: There is a EncryptWord object, there has been at least one
        // guess 
        // postcondition: returns the average guess calling the Average function
        public double GetAverageGuess()
        {
            double average;
            average = guesses.Average();
            return average;
        }

        // description:  returns the lowCount to the calling function 
        // precondition: There is a EncryptWord object 
        // postcondition: returns the lowCount to the calling function 
        public int GetLowCount()
        {
            return lowCount;
        }

        // description:  increments the lowCount variable if there is a guess too low 
        // precondition: There is a EncryptWord object, at least one guess too low 
        // postcondition: increments lowCount by one. 
        public void IncrementLowCount()
        {
            lowCount++;
        }

        // description: returns the count for high guesses 
        // precondition: There is a EncryptWord object 
        // postcondition: returns highCount to calling function 
        public int GetHighCount()
        {
            return highCount;
        }

        // description: increments the highCount variable if there is a guess too 
        // high 
        // precondition: There is a EncryptWord object, has been at least one guess 
        // too high 
        // postcondition: increments the highCount variable by one 
        public void IncrementHighCount()
        {
            highCount++;
        }

        // description: adds to the List using Add 
        // precondition: There is a encryptWord object, a guess needs to be made 
        // postcondition: adds guess to end of List.
        public void AddToList(int x)
        {
            guesses.Add(x);
        }

        // description: turns on the word, to be used after encrypting 
        // precondition: There is a encryptWord object 
        // postcondition: sets object to "on"
        private void SetOn()
        {
            on = true;
        }

        // description: turns off the word, to be used after decrypting 
        // precondition: There is a encryptWord object 
        // postcondition: sets object to "off"
        private void SetOff()
        {
            on = false;
        }

        // description: resets all values  
        // precondition: EncryptWord object has been created 
        // postcondition: resets all values 
        public void Reset()
        {
            plainText = "";
            shift = SetShift();
            highCount = 0;
            lowCount = 0;
            averageGuess = 0;
            on = false;
        }

        //invariants below this point
        
        // description: plain text is returned when called 
        // precondition: there is text to call 
        // postcondition: returns the plain text to the calling function 
        public string GetPlainText() 
        {
	        return plainText;
        }

        // description: cipher is returned when called 
        // precondition:  there is an EncryptWord object, encrypt function has been 
        // called, EncryptWord state is "on"
        // postcondition: returns the cipher text to the calling function 
        public string GetCipher() 
        {
	        return cipherText;
        }

        // description: uses Random to generate a random number between 
        // 1 and 25. 
        // precondition: There is a EncryptWord object 
        // postcondition: sets the shift for the EncryptWord object 
        private int SetShift() 
        {
            Random rnd = new Random();
            int result = rnd.Next(MIN_RANGE, MAX_RANGE);
            return result;
        }

        //description: Checks if the guess is correct 
        //precondition: There is a EncryptWord object and one guess has been made
        //postcondition: returns true if key is correct false if not
        public bool CheckGuess(int key)
        {
            AddToList(key);
            if (key == shift)
                return true;
            else if (key < shift)
            {
                IncrementLowCount();
                return false;
            }
            else
                IncrementHighCount();
                return false;

        }


    }
}
