using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoiseCreater
{
    public class WorelyNoiseEditorWindow : SingleNoiseEditorWindowBase
    {
        private INoisCreater creater = new WorleyNoiseCreater();
        protected override INoisCreater Creater { get { return creater; } }

        protected override string NoiseName { get { return "Worely Noise"; } }

        [MenuItem("Tools/Noise/WorelyNoise")]
        static void Init()
        {
            window = GetWindow(typeof(WorelyNoiseEditorWindow));
        }
    }
}
