namespace ImageToolkit.HyperDrive
{
    class TestOperation : IHyperOperation
    {
        private static int _operationCount = 0;

        private readonly int _idNumber = _operationCount++;
        private readonly int _iterations = 0;

        public TestOperation(int iterations)
        {
            _iterations = iterations;
        }

        void IHyperOperation.Execute()
        {
            float x;
            for (int i = 0; i < _iterations; i++)
            {
                x = 1.0f / i;
            }
        }

        string IHyperOperation.ToString()
        {
            return "Test operation " + _idNumber.ToString();
        }
    }
}