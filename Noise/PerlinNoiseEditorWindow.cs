using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoiseCreater
{
    public class PerlinNoiseEditorWindow : SingleNoiseEditorWindowBase
    {
        private INoisCreater creater = new PerlinNoiseCreater();
        protected override INoisCreater Creater { get { return creater; } }

        protected override string NoiseName { get { return "Perlin Noise"; } }

        [MenuItem("Tools/Noise/PerlinNoise")]
        static void Init()
        {
            window = GetWindow(typeof(PerlinNoiseEditorWindow));
        }
    }
}

