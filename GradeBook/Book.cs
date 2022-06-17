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

    // Book is inherited from base class NamedObject, and implements interface IBook
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
}
