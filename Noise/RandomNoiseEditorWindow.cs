using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NoiseCreater
{
    public class RandomNoiseEditorWindow : SingleNoiseEditorWindowBase
    {
        protected override INoisCreater Creater => new RandomNoiseCreater();

        protected override string NoiseName => "Random Noise";

        [MenuItem("Tools/Noise/RandomNoise")]
        static void Init()
        {
            window = GetWindow(typeof(RandomNoiseEditorWindow));
        }
    }
}

