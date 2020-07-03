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
        protected Vector3 offsets;
        protected bool isSeamless;

        protected Texture2D noise2D;
        protected Texture3D noise3D;
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
            offsets = EditorGUILayout.Vector3Field("偏移向量", offsets);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("无缝纹理生成采用升维采样法，运行较耗时");
            isSeamless = EditorGUILayout.Toggle("无缝纹理", isSeamless);
            EditorGUILayout.Space(10);

            a = EditorGUILayout.Slider("a", a, 0, 3);
            f = EditorGUILayout.Slider("f", f, 0, 2);
            octave = EditorGUILayout.IntSlider("octave", octave, 1, 10);

            if (GUILayout.Button("生成 2D " + NoiseName))
            {
                if (!isSeamless) noise2D = NoiseGenerate.ShowNoise2D(width, height, offsets, octave, f, a, Creater);
                else noise2D = NoiseGenerate.ShowNoise2DSeamless(width, height, offsets, octave, f, a, Creater);
                tex2dPath = "Assets/Temp/" + id + "_" + NoiseName + "_Tex2D.asset";
                AssetDatabase.CreateAsset(noise2D, tex2dPath);
                AssetDatabase.SaveAssets();
            }

            if (noise2D != null)
            {
                GUI.Label(new Rect(0, 300, 800, 20), "2D纹理已生成， 路径为：" + tex2dPath);
                GUI.Label(new Rect(0, 325, 800, 20), "2D纹理预览图：");
                GUI.DrawTexture(new Rect(0, 350, noise2D.width, noise2D.height), noise2D); ;
            }


            //worleyNoise2D = (Texture2D)EditorGUILayout.ObjectField("Worely Noise 2D", worleyNoise2D, typeof(Texture), false);

            if (GUILayout.Button("生成 3D " + NoiseName))
            {
                if (!isSeamless) noise3D = NoiseGenerate.ShowNoise3D(width, height, depth, offsets, octave, f, a, Creater);
                else noise3D = NoiseGenerate.ShowNoise3DSeamless(width, height, depth, offsets, octave, f, a, Creater);
                tex3dPath = "Assets/Temp/" + id + "_" + NoiseName + "_Tex3D.asset";
                AssetDatabase.CreateAsset(noise3D, tex3dPath);
                AssetDatabase.SaveAssets();
            }
            if (noise3D != null)
            {
                float yoffset = noise2D == null ? 0 : noise2D.height;
                GUI.Label(new Rect(0, 400 + yoffset, 800, 20), "3D纹理已生成， 路径为：" + tex3dPath);
                GUI.Label(new Rect(0, 425 + yoffset, 800, 20), "3D纹理预览图（3D纹理的预览图会有问题，不代表真实效果，可以2D纹理效果为参考）");
                GUI.DrawTexture(new Rect(0, 450 + yoffset, noise3D.width, noise3D.height), noise3D);
            }

            GUILayout.EndVertical();
        }
    }
}

