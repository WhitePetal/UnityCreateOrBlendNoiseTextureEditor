using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCreater
{
    public class NoiseGenerate
    {
        public static Texture2D ShowNoise2D(int width, int height, int octave, float f, float a, INoisCreater creater)
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
                    float noise = creater.Get2D((float)x * frequency, (float)y * frequency) * ambitient;
                    tex.SetPixel(x, y, new Color(0.5f * noise, 0.5f * noise, 0.5f * noise));
                }
            }

            //tex.wrapMode = TextureWrapMode.Repeat;
            tex.Apply();
            return tex;
        }

        public static Texture3D ShowNoise3D(int width, int height, int depth, int octave, float f, float a, INoisCreater creater)
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
                        float noise = creater.Get3D((float)x * frequency, (float)y * frequency, (float)z * frequency) * ambitient;
                        tex.SetPixel(x, y, z, new Color(0.5f * noise, 0.5f * noise, 0.5f * noise));
                    }
                }
            }
            tex.Apply();
            return tex;
        }
    }
}

