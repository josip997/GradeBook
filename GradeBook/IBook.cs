namespace GradeBook
{
    //Interface for book, defining which parameters and methods should every book have
    //More used than abstract classes
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistic();
        string Name { get; set; }
        event GradeAddedDelegate GradeAdded;

    }
}
