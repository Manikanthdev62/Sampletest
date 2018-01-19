using System;
using System.Collections.Generic;
using System.Linq;

namespace sampletest
{
    class Program
    {
        public static string FindLongestWords(string[] listOfWords)
        {
            if (listOfWords == null) throw new ArgumentException("listOfWords");
            var sortedWords = listOfWords.OrderByDescending(word => word.Length).ToList();
            var dict = new HashSet<String>(sortedWords);
            foreach (var word in sortedWords)
            {
                if (IsMadeOfWords(word, dict))
                {
                    return word;
                }
            }
            return null;
        }

        private static List<Tuple<string, string>> generatePairs(string word)
        {
            var output = new List<Tuple<string, string>>();
            for (int i = 1; i < word.Length; i++)
            {
                output.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
            }
            return output;
        }

        private static bool IsMadeOfWords(string word, ICollection<string> dict)
        {
            if (String.IsNullOrEmpty(word)) return false;
            if (word.Length == 1)
            {
                return dict.Contains(word);
            }
            foreach (var pair in generatePairs(word).Where(pair => dict.Contains(pair.Item1)))
            {
                return dict.Contains(pair.Item2) || IsMadeOfWords(pair.Item2, dict);
            }
            return false;
        }

        private static void Main(string[] args)
        {
            string[] listOfWords = System.IO.File.ReadAllLines(@"C:\Users\manikanth\Downloads\New folder\wordlist.txt");
            string longest = FindLongestWords(listOfWords);
            Console.WriteLine(longest);
        }
    }
}

