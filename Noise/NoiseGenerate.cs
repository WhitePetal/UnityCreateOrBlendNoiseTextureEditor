using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace NoiseCreater
{
    public class NoiseGenerate
    {
        public static Texture2D ShowNoise2D(int width, int height, Vector3 offset, int octave, float f, float a, INoisCreater creater)
        {
            Texture2D tex = new Texture2D(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float frequency = 1;
                    float ambitient = 1;

                    for (int i = 0; i < octave; i++)
                    {
                        frequency *= f;
                        ambitient *= a;
                    }

                    float noise = creater.Get2D((x + offset.x) * frequency, (y + offset.y) * frequency) * ambitient;
                    tex.SetPixel(x, y, new Color(0.5f * noise, 0.5f * noise, 0.5f * noise, 0.5f * noise));
                }
            }

            //tex.wrapMode = TextureWrapMode.Repeat;
            tex.Apply();
            return tex;
        }

        public static Texture3D ShowNoise3D(int width, int height, int depth, Vector3 offset, int octave, float f, float a, INoisCreater creater)
        {
            Texture3D tex = new Texture3D(width, height, depth, TextureFormat.RGBA32, false);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float frequency = 1;
                        float ambitient = 1;


                        for (int i = 0; i < octave; i++)
                        {
                            frequency *= f;
                            ambitient *= a;
                        }

                        float noise = creater.Get3D((x + offset.x) * frequency, (y + offset.y) * frequency, (z + offset.z) * frequency) * ambitient;
                        tex.SetPixel(x , y , z , new Color(0.5f * noise, 0.5f * noise, 0.5f * noise, 0.5f * noise));
                    }
                }
            }
            tex.Apply();
            return tex;
        }

        public static Texture2D ShowNoise2DSeamless(int width, int height, Vector3 offset, int octave, float f, float a, INoisCreater creater)
        {
            //Texture3D uperTex = ShowNoise3D(width, width, height, offset, octave, f, a, creater);

            Texture2D tex = new Texture2D(width, height);

            float theta = 2 * Mathf.PI / (float)width;
            float width3D = width / (2 * Mathf.PI);
            int halfW = (int)width3D / 2;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float nx = halfW + Mathf.Sin(x * theta) * width3D;
                    float ny = halfW + Mathf.Cos(x * theta) * width3D;
                    int fx = (int)nx;
                    int fy = (int)ny;

                    float frequency = 1;
                    float ambitient = 1;


                    for (int i = 0; i < octave; i++)
                    {
                        frequency *= f;
                        ambitient *= a;
                    }


                    float noise = ambitient * creater.Get3D((fx + offset.x) * frequency, (fy + offset.y) * frequency, (y + offset.z) * frequency);
                    tex.SetPixel(x, y, new Color(0.5f * noise, 0.5f * noise, 0.5f * noise, 0.5f * noise));
                }
            }

            //tex.wrapMode = TextureWrapMode.Repeat;
            tex.Apply();
            return tex;
        }

        #region
        //public async static Task<Texture3D> ShowNoise3DSeamlessAsync(int width, int height, int depth, Vector3 offset, int octave, float f, float a, INoisCreater creater)
        //{
        //    Texture3D[] tex4D = new Texture3D[depth];

        //    await Task.Run(() =>
        //    {
        //        for (int i = 0; i < depth; i++)
        //        {
        //            tex4D[i] = ShowNoise3D(width, height, depth, offset, octave, f, a, creater);
        //        }
        //    });

        //    Texture3D tex = new Texture3D(width, height, depth, TextureFormat.RGBA32, false);

        //    float halfW = width / 2f;
        //    float theta = 2 * Mathf.PI / (float)width;

        //    await Task.Run(() =>
        //    {
        //        for (int x = 0; x < width; x++)
        //        {
        //            for (int y = 0; y < height; y++)
        //            {
        //                for (int z = 0; z < depth; z++)
        //                {
        //                    float nx = halfW + Mathf.Cos(x * theta) * halfW;
        //                    float ny = halfW + Mathf.Sin(y * theta) * halfW;
        //                    int fx = (int)nx;
        //                    int fy = (int)ny;
        //                    Color col = tex4D[z].GetPixel(fx, fy, y);
        //                    tex.SetPixel(x, y, z, col);
        //                }
        //            }
        //        }
        //    });

        //    tex.Apply();
        //    return tex;
        //}
        #endregion

        //private static IEnumerator ShowNoise3DSeamlessCor(int width, int height, int depth, Vector3 offset, int octave, float f, float a, INoisCreater creater, Action<Texture3D> callback)
        //{
        //    Texture3D tex = null;
        //    yield return tex = ShowNoise3DSeamless(width, height, depth, offset, octave, f, a, creater);
        //    if (tex != null) callback.Invoke(tex);
        //    else Debug.LogError("3D 无缝纹理 生成失败！");
        //}

        public static Texture3D ShowNoise3DSeamless(int width, int height, int depth, Vector3 offset, int octave, float f, float a, INoisCreater creater)
        {
            //Texture3D tex4D = null;
            Vector3 offset4D = Vector3.zero;
            int width4D = (int)(width / (2 * Mathf.PI) + 2);
            //for(int i = 0; i < depth; i++)
            //{
            //    offset4D += Vector3.one * depth;
            //    tex4D = ShowNoise3D(width4D, width4D, height, offset4D, octave, f, a, creater);
            //    AssetDatabase.CreateAsset(tex4D, "Assets/Temp/Temp/" + i + ".asset");
            //}
            //AssetDatabase.SaveAssets();

            Texture3D tex = new Texture3D(width, height, depth, TextureFormat.RGBA32, false);

            float halfW = width4D / 2f;
            float theta = 2 * Mathf.PI / (float)width;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float nx = halfW + Mathf.Cos(x * theta) * width4D;
                        float ny =  halfW + Mathf.Sin(y * theta) * width4D;
                        int fx = (int)nx;
                        int fy = (int)ny;

                        float frequency = 1;
                        float ambitient = 1;


                        for (int i = 0; i < octave; i++)
                        {
                            frequency *= f;
                            ambitient *= a;
                        }

                        float noise1 = creater.Get3D((fx + offset.x) * frequency, (fy + offset.y) * frequency, (y + offset.z) * frequency);
                        float noise2 = creater.Get3D((fx + offset.x) * frequency, (fy + offset.y) * frequency, (z + offset.z) * frequency);
                        float noise = ambitient * (noise1 + noise2) / 2f;
                        tex.SetPixel(x, y, z, new Color(0.5f * noise, 0.5f * noise, 0.5f * noise, 0.5f * noise));
                        //AssetDatabase.DeleteAsset("Assets/Resources/Temp/" + z + ".asset");
                    }
                }
            }
            
            tex.Apply();

            //for (int z = 0; z < depth; z++)
            //{
            //    AssetDatabase.DeleteAsset("Assets/Temp/Temp/" + z + ".asset");
            //}

            return tex;
        }
    }
}

