namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //IBook book = new InMemoryBook("Josip's Grade book");
            IBook book = new DiskBook("Josip's Grade book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var result = book.GetStatistic();

            //Console.WriteLine(InMemoryBook.CATEGORY);
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {result.Low}");
            Console.WriteLine($"The highest grade is {result.High}");
            Console.WriteLine($"The average grade is {result.Average:N1}");
            Console.WriteLine($"The letter grade is {result.Letter}");
        }

        //Polymorphic method, using interface IBook to pass book since book can be InMemoryBook or DiskBook
        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                string input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("***");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs args) //static void method used for invoking GradeAdded event from InMemoryBook class
        {
            Console.WriteLine("A grade was added.");
        }
    }
}