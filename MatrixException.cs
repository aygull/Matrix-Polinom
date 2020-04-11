using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixClass
{
    class MatrixException : ArgumentException
    {
        public int Value { get; }
        public MatrixException(string message)
        : base(message) {
        }
    }
}
