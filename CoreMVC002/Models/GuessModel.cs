using System;
namespace CoreMVC002.Models
{
    public class GuessModel
    {
        public string UserGuess { get; set; }
        public int TotalGuesses { get; set; }
        public List<string> GuessHistory { get; set; }

        public GuessModel()
        {
            GuessHistory = new List<string>();
        }
    }

}