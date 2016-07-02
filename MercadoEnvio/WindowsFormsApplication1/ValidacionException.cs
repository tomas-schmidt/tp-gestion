using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class ValidacionException : Exception
    {

        public ValidacionException(string message)
            : base(message)
        {
        }
    }
}
