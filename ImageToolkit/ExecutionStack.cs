using System.Windows.Forms;

namespace ImageToolkit
{
    public static class ExecutionStack
    {
        public static FormMain mainForm;
        public static ListBox ls;

        public static IOperand X =>
            ls.Items.Count >= 1 ? (IOperand)ls.Items[0] : null;

        public static IOperand Y =>
            ls.Items.Count >= 2 ? (IOperand)ls.Items[1] : null;

        public static void Push(IOperand op)
        {
            if (op != null)
            {
                ls.Items.Insert(0, op);
            }
            ls.Refresh();
        }

        public static IOperand Pop()
        {
            IOperand temp = null;
            if (ls.Items.Count >= 1)
            {
                temp = (IOperand)ls.Items[0];
                ls.Items.RemoveAt(0);
                ls.Refresh();
            }
            return temp;
        }

        public static void ClearAll()
        {
            while (Pop() != null) ;
        }

        public static void SwapXY()
        {
            if (ls.Items.Count >= 2)
            {
                object temp = ls.Items[0];
                ls.Items[0] = ls.Items[1];
                ls.Items[1] = temp;
            }
        }

        public static void Remove(IOperand op)
        {
            while (ls.Items.Contains(op))
            {
                ls.Items.Remove(op);
            }
        }

        public static void RemoveAll()
        {
            ls.Items.Clear();
        }
    }
}