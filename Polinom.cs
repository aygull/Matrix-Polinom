using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixClass
{
    class Polinom: IEnumerable, ICloneable, IComparable
    {
        public List<Monom> monoms;

        public Polinom(Monom m)
        {
            this.monoms = new List<Monom>
            {
                m
            };
        }

        public Polinom(List<Monom> monoms)
        {
            this.monoms = new List<Monom>();
            foreach (Monom elem in monoms)
            {
                this.monoms.Add(new Monom(elem.coeff, elem.symbol, elem.degree));
            }
        }

        public Polinom()
        {
            this.monoms = new List<Monom>();
        }

        public object Clone()
        {
            return new Polinom() { monoms = new List<Monom>(this.monoms) };
        }

        public static Polinom operator*(Polinom a, Polinom b)
        {
            Polinom res = new Polinom();
            foreach (Monom i in a)
            {
                foreach(Monom j in b)
                {
                    res.monoms.Add(i * j);
                    Console.WriteLine(res);
                }
            }
            return res.ReducedPolinom();
        }

        /*public static Polinom operator *(Polinom a, Matrix b)
        {
            Polinom res = new Polinom();
            foreach (Monom i in a)
            {
                foreach (Monom j in b)
                {
                    res.monoms.Add(* j);
                }
            }
            return res;
        }*/

        public static Polinom operator/(Polinom a, Polinom b)
        {
            Polinom res = new Polinom();
            if(a.monoms[0].degree < b.monoms[0].degree) { return null; }
            Monom tmp = a.monoms[0] / b.monoms[0];
            res.monoms.Add(tmp);
            Polinom pol = a - (new Polinom(tmp) * b).ReducedPolinom();
            foreach(Monom i in pol)
            {
                foreach(double elem in i.coeff)
                {
                    if (elem != 0)
                    {
                        res.monoms.AddRange((pol / b).monoms);
                    }
                }
            }
            return res;
            
        }
        public static Polinom operator+(Polinom a, Polinom b)
        {
            Polinom res = new Polinom();
            foreach (Monom i in a)
            {
                res.monoms.Add(i);
            }
            foreach (Monom j in b)
            {
                res.monoms.Add(j);
            }
            return res.ReducedPolinom();
        }

        public static Polinom operator-(Polinom a, Polinom b)
        {
            Polinom res = new Polinom();
            foreach (Monom i in a)
            {
                res.monoms.Add(i);
            }
            foreach (Monom j in b)
            {
                j.coeff = j.coeff * -1;
                res.monoms.Add(j);
            }
            return res.ReducedPolinom();
        }

        public static Polinom Compose(Polinom a, Polinom b)
        {
            Polinom result = new Polinom();
            foreach(Monom monA in a.monoms)
            {
                Polinom pol = Polinom.Pow(b, monA.degree);
                foreach(Monom monB in pol)
                {
                    result.monoms.Add(new Monom(monA.coeff * monB.coeff, monA.symbol, monB.degree));
                }
            }
            return result.ReducedPolinom();
        }

        public static Polinom Pow(Polinom a, int degree)
        {
            Polinom result = (Polinom)a.Clone();
            for(var i = 0; i < degree - 1; i++)
            {
                result = result * a; 
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            int i = 1;
            foreach(Monom mon in this)
            {
                if (mon.degree == 0)
                {
                    res.Append(mon.coeff);
                }
                else
                {
                    res.Append(mon.coeff + mon.symbol + "^" + mon.degree);
                }
                if (i < this.monoms.Count)
                {
                    res.Append("+\n");
                }
                i++;
            }
            return res.ToString();
        }

        public Polinom ReducedPolinom()
        {
          //  Polinom res = new Polinom();
            //res.monoms = new List<Monom>();
            for(var i = 0; i < this.monoms.Count; i++)
            {
                for(var j = 0; j < this.monoms.Count; j++)

                {
                    if (i == j) continue;
                    if(this.monoms[i].degree == this.monoms[j].degree)
                    {
                        this.monoms[i].coeff = this.monoms[i].coeff + this.monoms[j].coeff;
                        this.monoms.Remove(this.monoms[j]);
                        j--;
                    }
                }
                
            }
            return this;
        }

        public IEnumerator GetEnumerator()
        {
            return monoms.GetEnumerator();
        }

        public int CompareTo(object obj1)
        {
            if (obj1.GetType() != typeof(Polinom))
            {
                throw new MatrixException("Невозможно сравнить");
            }
            Polinom pol2 = (Polinom)obj1;
            foreach (Monom i in this.monoms)
            {
                foreach(Monom j in pol2)
                {
                    if (i.degree == j.degree)
                    {
                       return i.coeff.CompareTo(j.coeff);
                    }
                }                    
            }
            return 0;
        }
    }
}
