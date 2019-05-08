using System.Collections.Generic;
using System.Linq;

/*
 * Command-line: Palindrome_Finder.exe [optional input string]
 */

#pragma warning disable IDE1006, IDE0017, CS0219
// IDE1006  Naming rule violation
// IDE0017  Object initialization can be simplified  
// CS0219   The variable 'variable' is assigned but its value is never used

#if DEBUG
using static System.Diagnostics.Debug;
#else
using static System.Console;
#endif

namespace Palindrome_Finder
{
    class Palindrome
    {
        // member string
        private string m_strTheString;

        // Constructor to initialize the string - defaults to that from the challenge if unspecified
        public Palindrome(string value = "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop")
        {
            m_strTheString = value;
        }

        // get / set property accessors
        public string TheString
        {
            get => m_strTheString;
            set => m_strTheString = value;
        }

        // Returns the first character of the member string
        private char firstCharacter() {
            return m_strTheString.First();
        }

        // Returns the last character of the member string
        private char lastCharacter() {
            return m_strTheString.Last();
        }

        // Returns the string that results by removing the first
        // and last characters from member string
        private string middleCharacters(string str) {
            return str.Remove(str.Length - 1).Remove(0, 1);
        }

        // perform test on string passed into function
        // Returns: true if a palindrome, false if not.
        public bool isPalindrome(string str) {
            TheString = str;                                            // assign member variable from function param
            // base case #1
            if (str.Length <= 1)                                        // is a null string technically a palindrome?
                return true;                                            // anyway, a single letter certainly is. 
            // base case #2
            if (firstCharacter().CompareTo(lastCharacter()) != 0)       // does the first and last letter match?  if not, it's obviously not a palindrome.
                return false;
            // recursive case
            return isPalindrome(middleCharacters(TheString));           // perform a recursive search on the "inner" part of the string
        }

        // perform test on member string
        // Returns: true if a palindrome, false if not.
        public bool isPalindrome() {
            // base case #1
            if (TheString.Length <= 1)
                return true;
            // base case #2
            if (firstCharacter() != lastCharacter())
                return false;
            // recursive case
            return isPalindrome(middleCharacters(TheString));
        }

        // Finds the 3 longest UNIQUE palindromes in a supplied string
        // Returns: a dictionary of recovered palindromes and their indexes within the supplied string
        public Dictionary<string, int> findPalindromes()
        {
            Dictionary<string, int> dictPalindromes = new Dictionary<string, int>();
            string strOriginal = TheString;

            for (int j = strOriginal.Length; j > 0; j--)                            // test the entire length of string, in decreasing length
            {
                for (int i = 0; i <= strOriginal.Length - j; i++)                   // start search at beginning of string, and move forward / right
                {
                    string strTryThis = strOriginal.Substring(i, j);                // extract the string from this point, for (decreasing) char length
#if DEBUG
                    WriteLine(strTryThis);
#endif
                    if (isPalindrome(strTryThis))
                    {
                        bool bFound = false;

                        // First, test to see if we already have this string within our collection
                        if (dictPalindromes.Count > 0)
                        {
                            foreach (KeyValuePair<string, int> p in dictPalindromes)
                                if (p.Key.Contains(strTryThis))
                                {
                                    bFound = true;
                                    break;
                                }
                        }
                        // if not found within, then add to the collection
                        if (!bFound)
                        {
                            dictPalindromes.Add(strTryThis, i);
                            if (dictPalindromes.Count == 3)                             // Found our quota of palindromes!
                                return dictPalindromes;
                        }
                    }
                }   // for (i ... starting string index
            }   // for (j ... ever decreasing length of characters
            return dictPalindromes;
        }
    }   // class Palindrome

    class Palindromic_Tests
    {
        Palindrome p;

        // default constructor
        public Palindromic_Tests()
        {
            p = new Palindrome();
        }
        public void checkPalindrome(string str)
        {
            WriteLine("Is this word a palindrome? " + str);
            WriteLine(p.isPalindrome(str));
        }

        public void doChecks()
        {
            checkPalindrome("a");
#if DEBUG
            Assert(p.isPalindrome("a"));
#endif
            checkPalindrome("rotor");
#if DEBUG
            Assert(p.isPalindrome("rotor"));
#endif
            checkPalindrome("gdeffedg");
#if DEBUG
            Assert(p.isPalindrome("gdeffedg"));
#endif
            checkPalindrome("amanaplanacanalpanama");
#if DEBUG
            Assert(p.isPalindrome("amanaplanacanalpanama"));
#endif
            checkPalindrome("motor");
#if DEBUG
            Assert(p.isPalindrome("motor"));
#endif
        }
    }   // class Palndromic_Tests

    class Program
    {
        // 2 Main entry points!  One is for DEBUG and the other for !DEBUG / RELEASE
#if DEBUG
        static void Main(string[] args)
        {
            Palindromic_Tests t = new Palindromic_Tests();
            t.doChecks();
        }
#else
        static void Main(string[] args)
        {
            Palindrome p;
            if (args.Length == 1)
                p = new Palindrome(args[0]);    // assign string from cmd-line parameter
            else
                p = new Palindrome();           // default string from challenge

            foreach (KeyValuePair<string, int> palindrome in p.findPalindromes())
            {
                WriteLine($"Text: {palindrome.Key}, Index: {palindrome.Value}, Length: {palindrome.Key.Length}");
            }

            Write("\nPress Enter: ");
            ReadLine();
        }
#endif
    }   // class Program
}   // namespace Palindrome_Finder
#pragma warning restore IDE1006, IDE0017, CS0219
