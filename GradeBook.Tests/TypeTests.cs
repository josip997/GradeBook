namespace GradeBook.Tests
{
    //delegate is used for reference methods, it has to have same type and parameters as methods for delegating
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {

        int count = 0;

        [Fact]

        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log;

            log = new WriteLogDelegate(ReturnMessage); //instancing of delegate

            log += IncrementCount; // multiple methods can be added
            log += ReturnMessage;


            var result = log("Hello!");
            Assert.Equal("Hello!", result); //Asserting that delegate is calling method

            Assert.Equal(3, count); //Asserting that delegate can be used for multiple method calls

        }

        //WriteLogDelegate is also used to reference IncrementCount
        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        //WriteLogDelegate is used to reference ReturnMessage
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        //Value types can also be passed by reference
        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x); //Asserting that method SetInt changed value of x to 42 by passing reference
              
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New name");

            Assert.Equal("New name", book1.Name); //Asserting that method GetBookSetName changed book by passing reference

        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New name");

            Assert.Equal("New name", book1.Name);

        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Book 1";
            var upper = MakeUppercase(name);

            Assert.Equal("Book 1", name);
            Assert.Equal("BOOK 1", upper);

            name = MakeUppercaseRef(ref name);

            Assert.Equal(upper, name);

        }
        private string MakeUppercaseRef(ref string name)
        {
            return name.ToUpper(); //Strings are immutable, methods like this return copy of original string
        }
        private string MakeUppercase(string name)
        {
            return name.ToUpper(); //Strings are immutable, methods like this return copy of original string
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name); //Asserting that two initialized objects are not same
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2); 
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2); //Asserting that you can hold same object in multiple variables
            Assert.True(object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}