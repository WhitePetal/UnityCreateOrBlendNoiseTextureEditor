using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class BlendNoiseCreater : INoisCreater
    {
        public INoisCreater rCreater;
        public INoisCreater gCreater;
        public INoisCreater bCreater;
        public INoisCreater aCreater;


        public float Get1D(float x)
        {
            throw new System.NotImplementedException();
        }

        public float Get2D(float x, float y)
        {
            throw new System.NotImplementedException();
        }

        public float Get3D(float x, float y, float z)
        {
            throw new System.NotImplementedException();
        }
    }
}

