using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoiseCreater
{
    public class BlendNoiseEditorWindow : EditorWindow
    {
        private static EditorWindow window;

        private static Dictionary<NoiseType, INoisCreater> createrDic = new Dictionary<NoiseType, INoisCreater>()
        {
            {NoiseType.None, null },
            {NoiseType.Perlin, new PerlinNoiseCreater() },
            {NoiseType.Worely, new WorleyNoiseCreater() }
        };

        private string id;
        private int width;
        private int height;
        private int depth;

        private INoisCreater rCreater;
        private INoisCreater gCreater;
        private INoisCreater bCreater;
        private INoisCreater aCreater;

        private bool openR;
        private bool openG;
        private bool openB;
        private bool openA;

        private NoiseType rType;
        private NoiseType gType;
        private NoiseType bType;
        private NoiseType aType;

        private int rOctave;
        private int gOctave;
        private int bOctave;
        private int aOctave;

        private float rA;
        private float gA;
        private float bA;
        private float aA;

        private float rF;
        private float gF;
        private float bF;
        private float aF;

        private Texture2D tex2D;
        private Texture3D tex3D;
        private string tex2dPath;
        private string tex3dPath;



        [MenuItem("Tools/Noise/BlendNoise")]
        static void Init()
        {
            window = GetWindow(typeof(BlendNoiseEditorWindow));
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            id = EditorGUILayout.TextField("纹理ID", id);

            width = EditorGUILayout.IntField("宽", width);
            height = EditorGUILayout.IntField("高", height);
            depth = EditorGUILayout.IntField("厚", depth);

            openR = EditorGUILayout.Toggle("开启R通道", openR);
            openG = EditorGUILayout.Toggle("开启G通道", openG);
            openB = EditorGUILayout.Toggle("开启B通道", openB);
            openA = EditorGUILayout.Toggle("开启A通道", openA);

            if (openR) rType = (NoiseType)EditorGUILayout.EnumFlagsField("R通道噪声类型：", rType);
            if (openG) gType = (NoiseType)EditorGUILayout.EnumFlagsField("G通道噪声类型：", gType);
            if (openB) bType = (NoiseType)EditorGUILayout.EnumFlagsField("B通道噪声类型：", bType);
            if (openA) aType = (NoiseType)EditorGUILayout.EnumFlagsField("A通道噪声类型：", aType);

            rCreater = createrDic[rType];
            gCreater = createrDic[gType];
            bCreater = createrDic[bType];
            aCreater = createrDic[aType];

            if(openR) rOctave = EditorGUILayout.IntSlider("R通道 octave：", rOctave ,1, 10);
            if (openG) gOctave = EditorGUILayout.IntSlider("G通道 octave：", gOctave, 1, 10);
            if (openB) bOctave = EditorGUILayout.IntSlider("B通道 octave：", bOctave, 1, 10);
            if(openA)aOctave = EditorGUILayout.IntSlider("A通道 octave：", aOctave, 1, 10);

            if(openR)rA = EditorGUILayout.Slider("R通道 a：", rA, 0, 5);
            if(openG)gA = EditorGUILayout.Slider("G通道 a：", gA, 0, 5);
            if(openB)bA = EditorGUILayout.Slider("B通道 a：", bA, 0, 5);
            if(openA)aA = EditorGUILayout.Slider("A通道 a：", aA, 0, 5);

            if(openR)rF = EditorGUILayout.Slider("R通道 f：", rF, 0, 3);
            if(openG)gF = EditorGUILayout.Slider("G通道 f：", gF, 0, 3);
            if(openB)bF = EditorGUILayout.Slider("B通道 f：", bF, 0, 3);
            if(openA) aF = EditorGUILayout.Slider("A通道 f：", aF, 0, 3);


            if (GUILayout.Button("生成 2D Blend Noise 纹理"))
            {
                tex2D = ShowBlendNoiseTex2D();
                tex2dPath = "Assets/Temp/" + id + "_BlendTex2D.asset";
                AssetDatabase.CreateAsset(tex2D, tex2dPath);
                AssetDatabase.SaveAssets();
            }

            if(GUILayout.Button("生成 3D Blend Noise 纹理"))
            {
                tex3D = ShowBlendNoise3D();
                tex3dPath = "Assets/Temp/" + id + "_BlendTex3D.asset";
                AssetDatabase.CreateAsset(tex3D, tex3dPath);
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.LabelField("2D 纹理路径：" + tex2dPath);
            EditorGUILayout.ObjectField("2D 纹理预览：", tex2D, typeof(Texture), false);

            EditorGUILayout.LabelField("3D 纹理路径：" + tex3dPath);
            EditorGUILayout.ObjectField("3D 纹理预览：" , tex3D, typeof(Texture), false);

            EditorGUILayout.EndVertical();
        }

        private Texture3D ShowBlendNoise3D()
        {
            Texture3D tex = new Texture3D(width, height, depth, TextureFormat.ARGB32, false);
            tex.Apply();

            BlendPass3D(tex, rCreater, new Color(1, 0, 0, 0), width, height, depth, rOctave, rA, rF);
            BlendPass3D(tex, gCreater, new Color(0, 1, 0, 0), width, height, depth, gOctave, gA, gF);
            BlendPass3D(tex, bCreater, new Color(0, 0, 1, 0), width, height, depth, bOctave, bA, bF);
            BlendPass3D(tex, aCreater, new Color(0, 0, 0, 1), width, height, depth, aOctave, aA, aF);

            tex.Apply();
            return tex;
        }
        private void BlendPass3D(Texture3D tex, INoisCreater creater, Color mask, int width, int height, int depth , int octave, float a, float f)
        {
            if (creater != null)
            {
                Texture3D targetTex = NoiseGenerate.ShowNoise3D(width, height,depth , octave, f, a, creater);
                BlendPassColorTexture3D(tex, targetTex, mask);
            }
        }
        private void BlendPassColorTexture3D(Texture3D cur, Texture3D target, Color mask)
        {
            if (cur.width != target.width || cur.height != target.height || cur.depth != target.depth)
                Debug.LogError("无法复制纹理颜色，两个纹理的长或宽不相等！");

            for (int x = 0; x < cur.width; x++)
            {
                for (int y = 0; y < cur.height; y++)
                {
                    for(int z = 0; z < cur.depth; z++)
                    {
                        Color curCol = cur.GetPixel(x, y,z);
                        curCol *= (new Color(1, 1, 1, 1) - mask);

                        Color targetCol = target.GetPixel(x, y,z);
                        targetCol *= mask;

                        cur.SetPixel(x, y, z, curCol + targetCol);
                    }
                }
            }

            cur.Apply();
        }

        private Texture2D ShowBlendNoiseTex2D()
        {
            Texture2D tex = new Texture2D(width, height);
            tex.Apply();

            BlendPass2D(tex, rCreater, new Color(1, 0, 0, 0), width, height, rOctave, rA, rF);
            BlendPass2D(tex, gCreater, new Color(0, 1, 0, 0), width, height, gOctave, gA, gF);
            BlendPass2D(tex, bCreater, new Color(0, 0, 1, 0), width, height, bOctave, bA, bF);
            BlendPass2D(tex, aCreater, new Color(0, 0, 0, 1), width, height, aOctave, aA, aF);

            tex.Apply();
            return tex;
        }

        private void BlendPass2D(Texture2D tex ,INoisCreater creater, Color mask, int width, int height, int octave, float a, float f)
        {
            if (creater != null)
            {
                Texture2D targetTex = NoiseGenerate.ShowNoise2D(width, height, octave, f, a, creater);
                BlendPassColorTexture2D(tex, targetTex, mask);
            }
        }
        private void BlendPassColorTexture2D(Texture2D cur, Texture2D target, Color mask)
        {
            if (cur.width != target.width || cur.height != target.height) Debug.LogError("无法复制纹理颜色，两个纹理的长或宽不相等！");

            for (int x = 0; x < cur.width; x++)
            {
                for(int y = 0; y < cur.height; y++)
                {
                    Color curCol = cur.GetPixel(x, y);
                    curCol *= (new Color(1, 1, 1, 1) - mask);

                    Color targetCol = target.GetPixel(x, y);
                    targetCol *= mask;

                    cur.SetPixel(x, y, curCol + targetCol);
                }
            }

            cur.Apply();
        }
    }

    public enum NoiseType
    {
        None,
        Perlin,
        Worely
    }
}

