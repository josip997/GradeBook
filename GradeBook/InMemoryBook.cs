namespace GradeBook
{
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
