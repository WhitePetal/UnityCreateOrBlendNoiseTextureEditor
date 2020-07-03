using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NoiseCreater
{
    public class SimplexNoiseEditorWindow : SingleNoiseEditorWindowBase
    {
        protected override INoisCreater Creater => new SimplexNoiseCreater();

        protected override string NoiseName => "Simplex Noise";

        [MenuItem("Tools/Noise/SimplexNoise")]
        static void Init()
        {
            window = GetWindow(typeof(SimplexNoiseEditorWindow));
        }
    }
}

