using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public interface INoisCreater
    {
        float Get1D(float x);
        float Get2D(float x, float y);
        float Get3D(float x, float y, float z);
    }
}

