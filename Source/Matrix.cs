using System;
using System.Text;

namespace ECCCMath
{
    public class Matrix
    {
        private int width;
        private int height;

        public float[][] Data;

        public Matrix(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            Data = new float[height][];
            for (int i = 0; i < height; i++)
            {
                Data[i] = new float[width];
                for (int j = 0; j < width; j++)
                {
                    Data[i][j] = 0f;
                }
            }
        }

        public float this[int i, int j]
        {
            get => Data[i][j];
            set => Data[i][j] = value;
        }
        
        public Matrix Randomise(float n)
        {
            Matrix c = Random(width, height, n);
            Data = c.Data;
            
            return this;
        }

        public static Matrix Random(int width, int height, float n)
        {
            Matrix m1 = new Matrix(width, height);
            var rng = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    m1[i, j] = (float)rng.NextDouble() * n - n * 0.5f;
                }
            }

            return m1;
        }
        
        public static Matrix RandomInt(int width, int height)
        {
            Matrix m1 = new Matrix(width, height);
            var rng = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    m1[i, j] = (float)Math.Floor(rng.NextDouble() * 10f);
                }
            }

            return m1;
        }

        public void Print()
        {
            Print(this);
        }
        
        public static void Print(Matrix m)
        {
            var s = new StringBuilder();

            for (var i = 0; i < m.height; i++)
            {
                for (var j = 0; j < m.width; j++)
                {
                    s.Append(Math.Round(m[i, j], 4)).Append('\t');
                }

                s.AppendLine();
            }
            
            Console.WriteLine(s);
        }

        /// <summary>
        /// Scalar adding.
        /// </summary>
        public Matrix Add(float n)
        {
            // Matrix c = Add(this, n);
            // Data = c.Data;
            
            return Add(this, n);
        }

        public static Matrix Add(Matrix m1, float n)
        {
            Matrix m2 = new Matrix(m1.width, m1.height);
            for (int i = 0; i < m2.height; i++)
            {
                for (int j = 0; j < m2.width; j++)
                {
                    m2[i, j] = m1[i, j] + n;
                }
            }

            return m2;
        }
        
        /// <summary>
        /// Element wise adding
        /// </summary>
        public Matrix Add(Matrix m2)
        {
            // Matrix c = Add(this, m2);
            // Data = c.Data;

            return Add(this, m2);;
        }

        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m1.height != m2.height || m1.width != m2.width)
            {
                throw new Exception("Add: Matrix shape doesn't match.");
            }
            
            Matrix c = new Matrix(m1.width, m1.height);
            for (int i = 0; i < c.height; i++)
            {
                for (int j = 0; j < c.width; j++)
                {
                    c[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return c;
        }

        public Matrix Subtract(Matrix m2)
        {
            // Matrix c = Subtract(this, m2);
            // Data = c.Data;

            return Subtract(this, m2);;
        }

        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (m1.height != m2.height || m1.width != m2.width)
            {
                throw new Exception("Subtract: Matrix shape doesn't match.");
            }
            
            Matrix c = new Matrix(m1.width, m1.height);
            for (int i = 0; i < c.height; i++)
            {
                for (int j = 0; j < c.width; j++)
                {
                    c[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return c;
        }

        /// <summary>
        /// Scalar product
        /// </summary>
        public Matrix Scale(float n)
        {
            // Matrix c = Scale(this, n);
            // Data = c.Data;
            
            return Scale(this, n);
        }

        public static Matrix Scale(Matrix m1, float n)
        {
            Matrix m2 = new Matrix(m1.width, m1.height);
            for (int i = 0; i < m2.height; i++)
            {
                for (int j = 0; j < m2.width; j++)
                {
                    m2[i, j] = m1[i, j] * n;
                }
            }

            return m2;
        }

        public Matrix Hadamard(Matrix m2)
        {
            // Matrix c = Hadamard(this, m2);
            // Data = c.Data;

            return Hadamard(this, m2);
        }

        public static Matrix Hadamard(Matrix m1, Matrix m2)
        {
            if (m1.height != m2.height || m1.width != m2.width)
            {
                throw new Exception("Hadamard: Matrix shape doesn't match.");
            }

            Matrix c = new Matrix(m1.width, m1.height);
            for (int i = 0; i < m1.height; i++)
            {
                for (int j = 0; j < m1.width; j++)
                {
                    c[i, j] = m1[i, j] * m2[i, j];
                }
            }

            return c;
        }

        public Matrix Map(Func<float, float> f)
        {
            // Matrix c = Map(this, f);
            // Data = c.Data;
            
            return Map(this, f);
        }
        
        public static Matrix Map(Matrix m1, Func<float, float> f)
        {
            Matrix m2 = new Matrix(m1.width, m1.height);
            for (int i = 0; i < m2.height; i++)
            {
                for (int j = 0; j < m2.width; j++)
                {
                    m2[i, j] = f(m1[i, j]);
                }
            }

            return m2;
        }

        /// <summary>
        /// Dot product | Matrix multiplication
        /// </summary>
        public Matrix MatrixProduct(Matrix other)
        {
            // Matrix c = MatrixProduct(this, other);
            // width = c.width;
            // height = c.height;
            // Data = c.Data;
            
            return MatrixProduct(this, other);
        }
        
        public static Matrix MatrixProduct(Matrix m1, Matrix m2)
        {
            if (m1.width != m2.height)
            {
                throw new Exception("Dot: Matrix shape doesn't match.");
            }

            Matrix c = new Matrix(m2.width, m1.height);
            for (int i = 0; i < c.height; i++)
            {
                for (int j = 0; j < c.width; j++)
                {
                    float weightedSum = 0f;
                    for (int k = 0; k < m1.width; k++)
                    {
                        weightedSum += m1[i, k] * m2[k, j];
                    }

                    c[i, j] = weightedSum;
                }
            }

            return c;
        }

        public Matrix Transpose()
        {
            return Transpose(this);
        }

        public static Matrix Transpose(Matrix m1)
        {
            Matrix c = new Matrix(m1.height, m1.width);

            for (int i = 0; i < m1.height; i++)
            {
                for (int j = 0; j < m1.width; j++)
                {
                    c[j, i] = m1[i, j];
                }
            }

            return c;
        }

        public static Matrix FromArray(float[][] arr)
        {
            Matrix c = new Matrix(arr[0].Length, arr.Length);

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[0].Length; j++)
                {
                    c[i, j] = arr[i][j];
                }
            }
            
            return c;
        }

        public float[] ToArray()
        {
            return ToArray(this);
        }

        public static float[] ToArray(Matrix m1)
        {
            Matrix m2 = Transpose(m1);
            
            float[][] arr = new float[m2.height][];
            for (int i = 0; i < m2.height; i++)
            {
                arr[i] = new float[m2.width];
                for (int j = 0; j < m2.width; j++)
                {
                    arr[i][j] = m2[i, j];
                }
            }

            return arr[0];
        }
    }
}