using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class RandomNoiseCreater : INoisCreater
    {
        float[] RandomArray1D;
        float[,] RandomArray2D;
        float[,,] RandomArray3D;

        private int arraySize;

        public RandomNoiseCreater()
        {
            arraySize = 256; 
        }

        public RandomNoiseCreater(int size)
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
            if (RandomArray1D == null) InitArray1D();
            int ix = (int)x;
            return Mathf.Clamp01(RandomArray1D[ix % (arraySize - 1)]);
        }

        public float Get2D(float x, float y)
        {
            if (RandomArray2D == null) InitArray2D();
            int ix = (int)x;
            int iy = (int)y;
            return Mathf.Clamp01(RandomArray2D[ix % (arraySize - 1), iy & (arraySize - 1)]);
        }

        public float Get3D(float x, float y, float z)
        {
            if (RandomArray3D == null) InitArray3D();
            int ix = (int)x;
            int iy = (int)y;
            int iz = (int)z;

            return Mathf.Clamp01(RandomArray3D[ix % (arraySize - 1), iy % (arraySize - 1), iz % (arraySize - 1)]);
        }
    }
}

