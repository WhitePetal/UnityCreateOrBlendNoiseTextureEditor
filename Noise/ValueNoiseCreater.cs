using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class ValueNoiseCreater : INoisCreater
    {
        float[] RandomArray1D;
        float[,] RandomArray2D;
        float[,,] RandomArray3D;

        private int arraySize;

        public ValueNoiseCreater()
        {
            arraySize = 256; 
        }
        public ValueNoiseCreater(int size)
        {
            arraySize = size;
        }

        public int ArraySize
        {
            get { return arraySize; }
            set { arraySize = value; }
        }


        private void InitArray1D()
        {
            RandomArray1D = new float[ArraySize];
            System.Random r = new System.Random();
            for (int i = 0; i < ArraySize; i++)
            {
                RandomArray1D[i] = (float)r.NextDouble();
            }
        }

        private void InitArray2D()
        {
            RandomArray2D = new float[ArraySize, ArraySize];
            System.Random r = new System.Random();
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    RandomArray2D[i, j] = (float)r.NextDouble();
                }
            }
        }

        private void InitArray3D()
        {
            RandomArray3D = new float[ArraySize, ArraySize, ArraySize];
            System.Random r = new System.Random();
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    for(int k = 0; k < ArraySize; k++) RandomArray3D[i, j, k] = (float)r.NextDouble();
                }
            }
        }

        public float Get1D(float x)
        {
            if (RandomArray1D == null)
            {
                InitArray1D();
            }

            int xmin = (int)(Mathf.Floor(x) % this.RandomArray1D.Length);
            float t = x - xmin;
            return Mathf.Lerp(RandomArray1D[xmin], RandomArray1D[xmin + 1], t);
        }

        public float Get2D(float x, float y)
        {
            if (RandomArray2D == null) InitArray2D();

            int xmin = (int)(Mathf.Floor(x) % this.ArraySize);
            int ymin = (int)(Mathf.Floor(y) % this.ArraySize);

            double tx = x - xmin;
            double ty = y - ymin;

            /// Random values at the corners of the cell
            int rx0 = (int)(xmin & (this.ArraySize - 1));
            int rx1 = (int)((xmin + 1) & (this.ArraySize - 1));
            int ry0 = (int)(ymin & (this.ArraySize - 1));
            int ry1 = (int)((ymin + 1) & (this.ArraySize - 1));

            float sx = (int)(tx * tx * (3 - (2 * tx)));
            float sy = (int)(ty * ty * (3 - (2 * ty)));

            float p00 = this.RandomArray2D[rx0, ry0];
            float p01 = this.RandomArray2D[rx0, ry1];
            float p10 = this.RandomArray2D[rx1, ry0];
            float p11 = this.RandomArray2D[rx1, ry1];

            var pl0 = Mathf.Lerp(p00, p10, sx);
            var pl1 = Mathf.Lerp(p01, p11, sx);

            return Mathf.Lerp(pl0, pl1, sy);
        }

        public float Get3D(float x, float y, float z)
        {
            if (RandomArray3D == null) InitArray3D();

            int xmin = (int)(Mathf.Floor(x) % this.ArraySize);
            int ymin = (int)(Mathf.Floor(y) % this.ArraySize);
            int zmin = (int)(Mathf.Floor(z) % this.ArraySize);

            double tx = x - xmin;
            double ty = y - ymin;
            double tz = z - zmin;

            /// Random values at the corners of the cell
            int rx0 = (int)(xmin & (this.ArraySize - 1));
            int rx1 = (int)((xmin + 1) & (this.ArraySize - 1));
            int ry0 = (int)(ymin & (this.ArraySize - 1));
            int ry1 = (int)((ymin + 1) & (this.ArraySize - 1));
            int rz0 = (int)(zmin & (this.ArraySize - 1));
            int rz1 = (int)((zmin + 1) & (this.ArraySize - 1));

            float sx = (int)(tx * tx * (3 - (2 * tx)));
            float sy = (int)(ty * ty * (3 - (2 * ty)));
            float sz = (int)(tz * tz * (3 - (2) * tz));

            float p000 = this.RandomArray3D[rx0, ry0, rz0];
            float p010 = this.RandomArray3D[rx0, ry1, rz0];
            float p100 = this.RandomArray3D[rx1, ry0, rz0];
            float p110 = this.RandomArray3D[rx1, ry1, rz1];

            float p001 = this.RandomArray3D[rx0, ry0, rz1];
            float p011 = this.RandomArray3D[rx0, ry1, rz1];
            float p101 = this.RandomArray3D[rx1, ry0, rz1];
            float p111 = this.RandomArray3D[rx1, ry1, rz1];

            var x01 = Mathf.Lerp(p000, p100, sx);
            var x02 = Mathf.Lerp(p010, p110, sx);
            var x11 = Mathf.Lerp(p001, p101, sx);
            var x12 = Mathf.Lerp(p011, p111, sx);
            var x0 = Mathf.Lerp(x01, x02, sy);
            var x1 = Mathf.Lerp(x11, x12, sy);

            return Mathf.Lerp(x0, x1, sz);
        }
    }
}

