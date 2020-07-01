using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class PerlinNoiseCreater : INoisCreater
    {
        public float Get1D(float x)
        {
            float color = Mathf.PerlinNoise(x, 0);
            color = Mathf.Clamp01(color);
            return color;
        }

        public float Get2D(float x, float y)
        {
            float color = Mathf.PerlinNoise(x, y);
            color = Mathf.Clamp01(color);
            return color;
        }

        public float Get3D(float x, float y, float z)
        {
            float nois1 = Mathf.PerlinNoise(x, y);
            float nois2 = Mathf.PerlinNoise(x, z);
            float nois3 = Mathf.PerlinNoise(y, z);

            float color = (nois1 + nois2 + nois3) / 3f;
            color = Mathf.Clamp01(color);
            return color;
        }
    }
}

