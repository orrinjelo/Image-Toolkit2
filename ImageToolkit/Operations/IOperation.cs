namespace ImageToolkit
{
    interface IOperation
    {
        // Required Properties
        string Catagory { get; }
        string Name { get; }
        string Description { get; }

        // Required Methods
        void Execute();
        string ToString();
    }
}
