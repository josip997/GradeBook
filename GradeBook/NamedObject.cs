namespace GradeBook
{
    //Base class 
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
}
