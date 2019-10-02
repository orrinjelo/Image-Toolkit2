using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToolkit.HyperDrive
{
    class testOperation : IHyperOperation
    {
        private static int _operationCount = 0;

        private int _idNumber = _operationCount++;
        private int _iterations = 0;

        public testOperation(int iterations)
        {
            this._iterations = iterations;
        }

        void IHyperOperation.Execute()
        {
            float x;
            for (int i = 0; i < _iterations; i++) x = 1.0f / (float)i;
        }

        string IHyperOperation.ToString()
        {
            return "Test operation " + _idNumber.ToString();
        }
    }
}
