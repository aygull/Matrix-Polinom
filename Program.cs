using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m = new Matrix(2);
            //Console.WriteLine(m);
            Matrix n = (Matrix)m.Clone();
            m.array[0, 0] = 0;
            //Console.WriteLine(n);
            //Console.WriteLine();
            List<Monom> l = new List<Monom>
            {
                new Monom(new Matrix(2.0), "x", 3),
                new Monom(new Matrix(2.5), "x", 1)
            };

            Polinom p1 = new Polinom(l);
            Polinom p2 = new Polinom(l);

            //Console.WriteLine(p1 + "\n");
            Console.WriteLine(p2 *p1);
            Console.WriteLine(p2.CompareTo(p1));
            Console.WriteLine(m.CompareTo(n));

            //Console.WriteLine(Polinom.Compose(p1, p2).ReducedPolinom());
        }

    }
}
