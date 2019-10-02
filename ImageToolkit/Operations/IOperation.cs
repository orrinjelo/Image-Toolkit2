using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
