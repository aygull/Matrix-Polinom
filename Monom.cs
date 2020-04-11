using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixClass
{
    class Monom: IComparer
    {
        public Matrix coeff = new Matrix(1.0);
        // protected double numCoeff;
        public string symbol;
        public int degree;

        public Monom(Matrix coef, string smb, int deg)
        {
            this.coeff = coef;
            this.symbol = smb;
            this.degree = deg;
        }

        public Monom(string smb, int dgr)
        {
            this.symbol = smb;
            this.degree = dgr;
        }

        public Monom(Matrix coef)
        {
            this.coeff = coef;
            this.symbol = "";
            this.degree = 0;
        }

        public static Monom operator+(Monom a, Monom b)
        {
            if (a.degree == b.degree && a.symbol == b.symbol)
            {
                return new Monom(a.coeff + b.coeff, a.symbol, a.degree);
            }
            return null;
        }
        public static Monom operator -(Monom a, Monom b)
        {
            if (a.degree == b.degree && a.symbol == b.symbol)
            {
                return new Monom(a.coeff - b.coeff, a.symbol, a.degree);
            }
            return null;
        }
        public static Monom operator *(Monom a, Monom b)
        {
            return new Monom(a.coeff * b.coeff, a.symbol, a.degree + b.degree);
        }

        public static Monom operator /(Monom a, Monom b)
        {
            return new Monom(a.coeff / b.coeff, a.symbol, a.degree - b.degree);

        }

        public int Compare(object obj1, object obj2)
        {
            if (obj1.GetType() != typeof(Monom) || obj2.GetType() != typeof(Monom))
            {
                throw new MatrixException("Невозможно сравнить");
            }
            Monom a = (Monom)obj1;
            Monom b = (Monom)obj2;
            if (a.degree == b.degree)
            {
                return Compare(a.coeff, b.coeff);
            }
            throw new NotImplementedException();
        }

        public Matrix GetMean(Monom a, double value)
        {
            return a.coeff * Math.Pow(value, a.degree);
        }

  
    }
}