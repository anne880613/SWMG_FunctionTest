using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMG_FunctionTest
{
    abstract class IOBase
    {
        public abstract bool GetDI(int port, int number);
        public abstract bool GetDO(int port, int number);
        public abstract void SetDO(int port, int number, bool onOff);

        public abstract int GetAI(int port, int number);
        public abstract void SetAO(int port, int number);
        public abstract void Close();
        public bool IsValid { protected set; get; }
        public string InitialErrorMessage { protected set; get; }
    }
}
