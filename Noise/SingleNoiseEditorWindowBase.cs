using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoiseCreater
{
    public abstract class SingleNoiseEditorWindowBase : EditorWindow
    {
        protected static EditorWindow window;

        protected string id;
        protected int width;
        protected int height;
        protected int depth;
        protected Texture2D perlinNoise2D;
        protected Texture3D perlinNoise3D;
        protected string tex3dPath;
        protected string tex2dPath;

        protected abstract INoisCreater Creater { get; }
        protected float a;
        protected float f;
        protected int octave;

        protected abstract string NoiseName { get;}

        protected virtual void OnGUI()
        {
            GUILayout.BeginVertical();

            id = EditorGUILayout.TextField("纹理ID", id);

            width = EditorGUILayout.IntField("宽度", width);
            height = EditorGUILayout.IntField("高度", height);
            depth = EditorGUILayout.IntField("厚度", depth);
            a = EditorGUILayout.Slider("a", a, 0, 3);
            f = EditorGUILayout.Slider("f", f, 0, 2);
            octave = EditorGUILayout.IntSlider("octave", octave, 1, 10);

            if (GUILayout.Button("生成 2D " + NoiseName))
            {
                perlinNoise2D = NoiseGenerate.ShowNoise2D(width, height, octave, f, a, Creater);
                tex2dPath = "Assets/Temp/" + id + + '_' + NoiseName + "_Tex2D.asset";
                AssetDatabase.CreateAsset(perlinNoise2D, tex2dPath);
                AssetDatabase.SaveAssets();
            }

            if (perlinNoise2D != null)
            {
                GUI.Label(new Rect(0, 200, 800, 20), "2D纹理已生成， 路径为：" + tex2dPath);
                GUI.Label(new Rect(0, 225, 800, 20), "2D纹理预览图：");
                GUI.DrawTexture(new Rect(0, 250, perlinNoise2D.width, perlinNoise2D.height), perlinNoise2D); ;
            }


            //worleyNoise2D = (Texture2D)EditorGUILayout.ObjectField("Worely Noise 2D", worleyNoise2D, typeof(Texture), false);

            if (GUILayout.Button("生成 3D " + NoiseName))
            {
                perlinNoise3D = NoiseGenerate.ShowNoise3D(width, height, depth, octave, f, a, Creater);
                tex3dPath = "Assets/Temp/" + id + '_' + NoiseName + "_Tex3D.asset";
                AssetDatabase.CreateAsset(perlinNoise3D, tex3dPath);
                AssetDatabase.SaveAssets();
            }
            if (perlinNoise3D != null)
            {
                GUI.Label(new Rect(0, 400, 800, 20), "3D纹理已生成， 路径为：" + tex3dPath);
                GUI.Label(new Rect(0, 425, 800, 20), "3D纹理预览图（3D纹理的预览图会有问题，不代表真实效果，可以2D纹理效果为参考）");
                GUI.DrawTexture(new Rect(0, 450, perlinNoise3D.width, perlinNoise3D.height), perlinNoise3D);
            }

            GUILayout.EndVertical();
        }
    }
}

