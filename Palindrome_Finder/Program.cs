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

        // Returns the last character of a string member string
        private char lastCharacter() {
            return m_strTheString.Last();
        }

        // Returns the string that results from removing the first
        //  and last characters from member string
        private string middleCharacters() {
            return m_strTheString.Remove(m_strTheString.Length - 1).Remove(0, 1);
        }

        public bool isPalindrome(string str) {
            // base case #1
            if (str.Length == 0 || str.Length == 1)
                return true;
            // base case #2
            if (firstCharacter() != lastCharacter())
                return false;
            // recursive case
            return isPalindrome(middleCharacters());
        }
    }   // class Palindrome

    class Palindromic_Tests
    {
        Palindrome p;

        public Palindromic_Tests()
        {
            p = new Palindrome()
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
            checkPalindrome("motor");
#if DEBUG
            Assert(p.isPalindrome("motor"));
#endif
        }
    }   // class Palndromic_Tests

    class Program
    {
        // 2 Main entry points!  One for DEBUG and the other for !DEBUG / RELEASE
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
        }
#endif
    }   // class Program
}   // namespace Palindrome_Finder
#pragma warning restore IDE1006, IDE0017, CS0219
