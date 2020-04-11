using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace MatrixClass
{
    class Matrix: IEnumerable, ICloneable, IComparable
    {
        public int N;
        public double[,] array;

        public Matrix(int n)
        {
            Random rand = new Random();
            this.N = n;
            this.array = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.array[i, j] = rand.Next(1, 50);
                }
            }
        }

        public Matrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            this.N = n;
            this.array = matrix;
        }

        public Matrix(double num)
        {
            this.N = 1;
            this.array = new double[1,1];
            this.array[0, 0] = num;
        }

        public void Operation(Action<int, int> func)
        {
            for (var i = 0; i < this.N; i++)
            {
                for (var j = 0; j < this.N; j++)
                {
                    func(i, j);
                }
            }
        }

        public static void Check(Matrix a, Matrix b)
        {
            if (a.N != b.N)
            {
                throw new MatrixException("Размерность матриц должна быть одинаковой!");
            }
        }

        public static Matrix operator+(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a.N);
            Check(a, b);
            res.Operation((int i, int j) => res.array[i, j] = a.array[i, j] + b.array[i, j]);
            return res;
        }
        public static Matrix operator-(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a.N);
            Check(a, b);
            res.Operation((int i, int j) => res.array[i, j] = a.array[i, j] - b.array[i, j]);
            return res;
        }

        public static Matrix operator*(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a.N);
            Check(a, b);
            for (int i = 0; i < a.N; ++i)
            {
                for (int j = 0; j < a.N; ++j)
                {
                    res.array[i, j] = 0;
                    for (int k = 0; k < a.N; ++k)
                    {
                        res.array[i, j] += a.array[i, k] * b.array[k, j];  
                    }
                }
            }
            return res;
        }

        public static Matrix operator*(Matrix a, double number)
        {
            Matrix res = new Matrix(a.N);
            res.Operation((int i, int j) => res.array[i, j] = a.array[i, j]*number);
            return res;
        }

        public static Matrix operator/(Matrix a, double number)
        {
            Matrix res = new Matrix(a.N);
            res.Operation((int i, int j) => res.array[i, j] = a.array[i, j] / number);
            return res;
        }

        public static Matrix operator /(Matrix a, Matrix b)
        {
            return new Matrix(a.array[0, 0] / b.array[0, 0]);      
        }

        public Matrix GetMinor(int row, int column)
        {
            if (row < 0 || row >= this.N || column < 0 || column >= this.N)
            {
                throw new MatrixException("Неверный индекс");
            }
            var result = new Matrix(this.N - 1);

            result.Operation((i, j) =>
            {
                if (i < row && j < column)
                {
                    result.array[i, j] = this.array[i, j];
                }
                else if (i >= row && j < column)
                {
                    result.array[i, j] = this.array[i + 1, j];
                }
                else if (i < row && j >= column)
                {
                    result.array[i, j] = this.array[i, j + 1];
                }
                else if (i >= row && j >= column)
                {
                    result.array[i, j] = this.array[i + 1, j + 1];
                }
            });
            return result;

        }
        public double Determinant( )
        {
            if (this.N == 1)
            {
                return this.array[0, 0];
            }
            if (this.N == 2)
            {
                return this.array[0, 0] * this.array[1, 1] - this.array[0, 1] * this.array[1, 0];
            }
            double result = 0;
            for (var j = 0; j < this.N; j++)
            {
                result += (j % 2 == 1 ? -1 : 1) * this.array[0, j] *
                    this.GetMinor(0,j).Determinant();
            }

            return result;
        }

        public Matrix GetTransposeMatrix()
        {
            Matrix result = new Matrix(this.N);
            result.Operation((i, j) => result.array[i, j] = this.array[j, i]);
            return result;
        }

        public Matrix InvertMatrix()
        {
            if (this.N == 1)
            {
                return new Matrix(this.array[0, 0]);
            }
            var determinant = Determinant();
            if (Math.Abs(determinant) < 1E-10)
                return null;

            var result = new Matrix(this.N);
            Operation((i, j) =>
            {
                result.array[i, j] = ((i + j) % 2 == 1 ? -1 : 1) * GetMinor(i, j).Determinant() / determinant;
            });
            return result.GetTransposeMatrix();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {
                    str.Append(this.array[i, j] + " ");
                }
                str.Append("\n");
            }
            return str.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }

        public object Clone() => new Matrix(1.0) { N = this.N, array = (double[,])this.array.Clone() };

        public int CompareTo(object obj1)

        {
            if (obj1.GetType() != typeof(Matrix))
            {
                throw new MatrixException("Невозможно сравнить");
            }
            Matrix a = (Matrix)obj1;
            Matrix b = new Matrix(this.array);
            Check(a, b);
            double detA = a.Determinant(), detB = b.Determinant();
            if (detA > detB)
            {
                return 1;
            }
            if (detA < detB)
            {
                return -1;
            }

            if (detA == detB)
            {
                return 0;
            }
            throw new NotImplementedException();
        }
    }
}
