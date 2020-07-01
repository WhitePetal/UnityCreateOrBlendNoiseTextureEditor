using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class WorleyNoiseCreater : INoisCreater
    {
        private float[] DistanceArray = new float[3];
        private int Seed = 3221;
        private uint OFFSET_BASIS = 2166136261;
        private uint FNV_PRIME = 16777619;


        private float Noise(Vector3 input)
        {
            long lastRandom;
            long numberFeaturePoints;
            Vector3 randomDiff = Vector3.zero;
            Vector3 featurePoint = Vector3.zero;
            int cubeX, cubeY, cubeZ;

            for (int i = 0; i < DistanceArray.Length; i++)
            {
                DistanceArray[i] = float.MaxValue;
            }

            int evalCubeX = (int)(Mathf.Floor(input.x));
            int evalCubeY = (int)(Mathf.Floor(input.y));
            int evalCubeZ = (int)(Mathf.Floor(input.z));

            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    for (int k = -1; k < 2; ++k)
                    {
                        cubeX = evalCubeX + i;
                        cubeY = evalCubeY + j;
                        cubeZ = evalCubeZ + k;

                        lastRandom = LcgRandom(Hash((cubeX + Seed) & 0xffffffff, (cubeY) & 0xffffffff, (cubeZ) & 0xffffffff));

                        numberFeaturePoints = ProbLookup(lastRandom);

                        for (int l = 0; l < numberFeaturePoints; ++l)
                        {
                            lastRandom = LcgRandom(lastRandom);
                            randomDiff.x = (float)lastRandom / 0x100000000;

                            lastRandom = LcgRandom(lastRandom);
                            randomDiff.y = (float)lastRandom / 0x100000000;

                            lastRandom = LcgRandom(lastRandom);
                            randomDiff.z = (float)lastRandom / 0x100000000;

                            featurePoint.x = randomDiff.x + cubeX;
                            featurePoint.y = randomDiff.y + cubeY;
                            featurePoint.z = randomDiff.z + cubeZ;

                            float v = EuclidanDistanceFunc(input, featurePoint);
                            Insert(DistanceArray, v);
                        }
                    }
                }
            }

            float color = CombinerFunction1(DistanceArray);
            color = Mathf.Clamp01(1 - color);
            return color;
        }

        private long LcgRandom(long lastValue)
        {
            return (((1103515245 & 0xffffffff) * lastValue + (12345 & 0xffffffff)) % 0x100000000) & 0xffffffff;
        }

        private long Hash(long i, long j, long k)
        {
            return ((((((OFFSET_BASIS ^ (i & 0xffffffff)) * FNV_PRIME) ^ (j & 0xffffffff)) * FNV_PRIME)
        ^ (k & 0xffffffff)) * FNV_PRIME) & 0xffffffff;
        }

        private long ProbLookup(long value)
        {
            value = value & 0xffffffff;
            if (value < 393325350) return 1 & 0xffffffff;
            if (value < 1022645910) return 2 & 0xffffffff;
            if (value < 1861739990) return 3 & 0xffffffff;
            if (value < 2700834071) return 4 & 0xffffffff;
            if (value < 3372109335) return 5 & 0xffffffff;
            if (value < 3819626178) return 6 & 0xffffffff;
            if (value < 4075350088) return 7 & 0xffffffff;
            if (value < 4203212043) return 8 & 0xffffffff;
            return 9 & 0xffffffff;
        }

        private float EuclidanDistanceFunc(Vector3 p1, Vector3 p2)
        {
            return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y)
        + (p1.z - p2.z) * (p1.z - p2.z);
        }

        private void Insert(float[] ar, float value)
        {
            float temp;

            for (int i = ar.Length - 1; i >= 0; i--)
            {
                if (value > ar[i]) break;

                temp = ar[i];
                ar[i] = value;
                if (i + 1 < ar.Length) ar[i + 1] = temp;
            }
        }

        private float CombinerFunction1(float[] ar)
        {
            return ar[0];
        }
        float CombinerFunction2(float[] ar)
        {
            return ar[1] - ar[0];
        }

        public float Get2D(float x, float y)
        {
            return Noise(new Vector3(x, y, 0));
        }

        public float Get3D(float x, float y, float z)
        {
            return Noise(new Vector3(x, y, z));
        }

        public float Get1D(float x)
        {
            return Noise(new Vector3(x, 0, 0));
        }
    }
}
