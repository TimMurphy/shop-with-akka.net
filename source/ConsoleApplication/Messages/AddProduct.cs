namespace ConsoleApplication.Messages
{
    internal class AddProduct
    {
        public AddProduct(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}