using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD

=======
            
>>>>>>> 6845cc7c6abb5e5b97e1aaed0cf630794d7b9f8b
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            Console.WriteLine("Average Scores");

            double total_math_score = 0, to_science_score = 0, to_english_score = 0;
            int max_math = 0; int min_math = 100;
            int max_science = 0; int max_english = 0;
            int min_english = 100; int min_science = 100;

            (string Name, int TotalScore)[] totalScores = new (string, int)[stdCount];

            for (int i = 1; i <= stdCount; i++)
            {
                int math_score = int.Parse(data[i, 2]);
                int science_score = int.Parse(data[i, 3]);
                int english_score = int.Parse(data[i, 4]);
<<<<<<< HEAD

                if (max_math < math_score) max_math = math_score;
                if (min_math > math_score) min_math = math_score;
                if (max_science < science_score) max_science = science_score;
                if (max_english < english_score) max_english = english_score;
                if (min_english > english_score) min_english = english_score;
                if (min_science > science_score) min_science = science_score;

                total_math_score += math_score;
                to_science_score += science_score;
                to_english_score += english_score;

=======
                    
                if (max_math < math_score) max_math = math_score;
                if (min_math > math_score) min_math = math_score;
                if(max_science < science_score) max_science = science_score;
                if (max_english < english_score) max_english = english_score;
                if(min_english > english_score) min_english = english_score;
                if(min_science > science_score) min_science = science_score;

                total_math_score += math_score; 
                to_science_score += science_score;
                to_english_score += english_score;
               
>>>>>>> 6845cc7c6abb5e5b97e1aaed0cf630794d7b9f8b

                int totalScore = math_score + science_score + english_score;
                totalScores[i - 1] = (data[i, 1], totalScore);
            }

<<<<<<< HEAD
            Console.WriteLine("Math : {0}", total_math_score / stdCount);
=======
            Console.WriteLine("Math : {0}", total_math_score/stdCount);
>>>>>>> 6845cc7c6abb5e5b97e1aaed0cf630794d7b9f8b
            Console.WriteLine("Science : {0}", to_science_score / stdCount);
            Console.WriteLine("English : {0}", to_english_score / stdCount);
            Console.WriteLine("\nMax and min score");
            Console.WriteLine("Math({0}, {1})", max_math, min_math);
<<<<<<< HEAD
            Console.WriteLine("English({0}, {1})", max_english, min_english);
            Console.WriteLine("Science({0}, {1})", max_science, min_science);
=======
            Console.WriteLine("Englisg({0}, {1})", max_english, min_english);
            Console.WriteLine("Science({0}, {1})", max_science, min_science);
            Console.WriteLine("\nStudents rank by total scores : ");
>>>>>>> 6845cc7c6abb5e5b97e1aaed0cf630794d7b9f8b

            var rankedScores = totalScores
                .OrderByDescending(student => student.TotalScore)
                .Select((student, index) => (student.Name, Rank: index + 1))
                .ToList();

            Console.WriteLine("\nStudents rank by total scores:");
            foreach (var (name, rank) in rankedScores)
            {
                string suffix = rank == 1 ? "st" : rank == 2 ? "nd" : rank == 3 ? "rd" : "th";
                Console.WriteLine("{0}: {1}{2}", name, rank, suffix);
            }
        }
    }

<<<<<<< HEAD

=======
    
>>>>>>> 6845cc7c6abb5e5b97e1aaed0cf630794d7b9f8b
}


/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/
