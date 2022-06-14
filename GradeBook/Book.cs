using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{

    //delegate for event GradeAdded
    //event is void type and has this same parameters
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    //Base class (could be in it's own file)
    //book is inheriting parameter name from this class so no need for defining it there anymore
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name //Auto-property generates and uses variable in background
        {
            get;
            set; // private set; modifying get set
        }
    }

    //Interface for book, defining which parameters and methods should every book should have
    //More used than abstract classes
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistic();
        string Name { get; set; }
        event GradeAddedDelegate GradeAdded;

    }
    // InMemoryBook is inherited from base class NamedObject, and implements interface IBook
    // You can only have one inheritence, but one or more interfaces
    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name) //passing name for base class constructor because is needed so
        {
        }

        public abstract event GradeAddedDelegate GradeAdded; // virtual = derived class may choose to override implementation details
                                                           // abstract = derived class has to implement

        public abstract void AddGrade(double grade); // every object derived from Book has to have implementation for AddGrade

        public abstract Statistics GetStatistic();
    }

    // DiskBook is inherited from abstract class Book
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.Write(grade);
                GradeAdded?.Invoke(this, new EventArgs()); //invoke of event when grade is added
            }
        }

        public override Statistics GetStatistic()
        {
            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }

    // InMemoryBook is inherited from abstract class Book
    public class InMemoryBook : Book 
    {

        public InMemoryBook(string name) : base(name) //passing name for base class constructor because is needed so
        {
            grades = new List<double>();
            this.Name = name;
        }

        public void AddLetterGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >=0)
            {
                grades.Add(grade);

                //if (GradeAdded != null)
                //{
                //    GradeAdded(this, new EventArgs());
                //}

                //same function as code above this
                GradeAdded?.Invoke(this, new EventArgs()); //invoke of event when grade is added
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
                       
        }
        public override Statistics GetStatistic()
        {
            var result = new Statistics();

            foreach(var grade in grades)
            {
                result.Add(grade);
            }

            return result;

        }

        public override event GradeAddedDelegate GradeAdded; //defining event GradeAdded using delegate

        public const string CATEGORY = "Science"; // Constant is associated with type 'InMemoryBook' instead of object
        private List<double> grades;
    }
}
