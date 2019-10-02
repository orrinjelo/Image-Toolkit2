using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit
{
    public static class ExecutionStack
    {
        public static frmMain mainForm;
        public static ListBox ls;
     /*   public static ExecutionStack(frmMain m)
        {
            mainForm = m;
            ls = mainForm.getStack();
        } */

        public static IOperand X
        {
            get
            {
                if (ls.Items.Count >= 1)
                    return (IOperand)ls.Items[0];
                else
                    return null;
            }
        }
        
        public static IOperand Y
        {
            get
            {
                if (ls.Items.Count >= 2)
                    return (IOperand)ls.Items[1];
                else
                    return null;
            }
        }

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
                ls.Items.Remove(op);
        }

        public static void RemoveAll()
        {
            ls.Items.Clear();
        }

    }
}
