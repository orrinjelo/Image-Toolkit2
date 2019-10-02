using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToolkit
{
    public class Operation : IOperation
    {
        private Action F;
        public Operation(string catagory, string name, string description, Action f, bool active)
        {
            Catagory = catagory;
            Name = name;
            Description = description;
            F = f;
        }

        // Required Properties
        public string Catagory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Required Methods
        public void Execute()
        {
            F();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class HyperOperation : HyperDrive.IHyperOperation
    {
        private Action F;
        public HyperOperation(string catagory, string name, string description, Action f, bool active)
        {
            Catagory = catagory;
            Name = name;
            Description = description;
            F = f;
        }

        // Required Properties
        public string Catagory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Required Methods
        public void Execute()
        {
            F();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    static class OperationRegistry
    {
        public static List<Operation> operations = new List<Operation>();

        public static void RegisterOperation(Operation op)
        {
            operations.Add(op);
        }


        public static Operation Retrieve(string Name="",String Description="")
        {
            if (Name != "")
            {
                foreach (Operation op in operations)
                {
                    if (op.Name == Name) return op;
                }
            }
            if (Description != "")
            {
                foreach (Operation op in operations)
                {
                    if (op.Description == Description) return op;
                }
            }
            return new Operation("None","Identity","",null,false);
        }
    }

}
