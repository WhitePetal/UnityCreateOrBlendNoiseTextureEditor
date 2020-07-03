using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NoiseCreater
{
    public class ValueNoiseEditorWindow : SingleNoiseEditorWindowBase
    {
        protected override INoisCreater Creater => new ValueNoiseCreater();

        protected override string NoiseName => "Value Noise";

        [MenuItem("Tools/Noise/ValueNoise")]
        static void Init()
        {
            window = GetWindow(typeof(ValueNoiseEditorWindow));
        }
    }
}

