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
        
        public void Randomise()
        {
            Matrix c = Random(width, height);
            Data = c.Data;
        }

        public static Matrix Random(int width, int height)
        {
            Matrix m1 = new Matrix(width, height);
            var rng = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    m1[i, j] = (float)rng.NextDouble() * 10f;
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
        public void Add(float n)
        {
            Matrix c = Add(this, n);
            Data = c.Data;
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
        /// Scalar product
        /// </summary>
        public void Scale(float n)
        {
            Matrix c = Scale(this, n);
            Data = c.Data;
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

        /// <summary>
        /// Dot product | Matrix multiplication
        /// </summary>
        public void Dot(Matrix other)
        {
            Matrix c = Dot(this, other);
            width = c.width;
            height = c.height;
            Data = c.Data;
        }
        
        public static Matrix Dot(Matrix m1, Matrix m2)
        {
            if (m1.width != m2.height)
            {
                // Console.Error.WriteLine("Matrix shape doesn't match.");
                throw new Exception("Matrix shape doesn't match.");
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
    }
}