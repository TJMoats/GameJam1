using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NPS
{
    public static class TextureHelper
    {
        /// <summary>
        /// Returns a 2d float array that contans normalized values from 0-1 based on the colors in the provided texture.
        /// </summary>
        /// <param name="_tex"></param>
        /// <returns></returns>
        public static float[,] ToHeightMap(Texture2D _tex)
        {
            Color[] heightmapData = _tex.GetPixels();
            int xSize = _tex.width;
            int ySize = _tex.height;

            float[,] heightmapArray = new float[xSize, ySize];

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    int targetHeightIndex = (y * xSize) + x;
                    Color targetHeightColor = heightmapData[targetHeightIndex];
                    float targetHeight = (targetHeightColor.r + targetHeightColor.g + targetHeightColor.b) / 3;
                    heightmapArray[x, y] = targetHeight;
                }
            }

            return heightmapArray;
        }

        public static Texture2D FromHeightMap(float[,] _heightmap)
        {
            throw new NotImplementedException();
        }

        public static Texture2D CreateGradientTexture(int _width, int _height, Color _start, Color _stop)
        {
            Color[] colorArray = new Color[_width * _height];
            Texture2D textureOut = new Texture2D(_width, _height);
            Gradient g;
            GradientColorKey[] gck;
            GradientAlphaKey[] gak;

            g = new Gradient();
            gck = new GradientColorKey[2];
            gck[0].color = _start;
            gck[0].time = 0.0F;
            gck[1].color = _stop;
            gck[1].time = 1.0F;

            gak = new GradientAlphaKey[2];
            gak[0].alpha = 1.0F;
            gak[0].time = 0.0F;
            gak[1].alpha = 0.0F;
            gak[1].time = 1.0F;

            g.SetKeys(gck, gak);

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    colorArray[(i * _width) + j] = g.Evaluate((float)i / (float)_height);
                }
            }

            textureOut.SetPixels(colorArray);
            textureOut.Apply();
            return textureOut;
        }

        public static Texture2D GenerateTexture(int _width, int _height, Color _color)
        {
            Texture2D texture2D = new Texture2D(_width, _height);
            texture2D.SetPixels(ArrayHelper.FilledArray<Color>(_color, _width * _height));
            return texture2D;
        }

        /*public static Texture2D GenerateRandomTexture(int _width, int _height)
        {
            float[,] noiseMap = RandomGeneration.Noise.NoiseMap(_width, _height, 0, 1);
            Color[] colors = new Color[_width * _height];
            for (int i = 0; i < noiseMap.GetLength(0); i++)
            {
                for (int j = 0; j < noiseMap.GetLength(1); j++)
                {
                    colors[i * noiseMap.GetLength(0) + j] = new Color(noiseMap[i, j], noiseMap[i, j], noiseMap[i, j]);
                }
            }
            Texture2D texture2D = new Texture2D(_width, _height);
            texture2D.SetPixels(colors);
            return texture2D;
        }*/

        #region IO
        public static Texture2D SaveTexture(Texture2D _tex, string _path)
        {
            byte[] bytes = _tex.EncodeToPNG();
            string fullPath = Application.dataPath + "/" + _path;
            Debug.Log($"Saving texture to {fullPath}");
            File.WriteAllBytes(fullPath, bytes);

            return LoadFromPath(_path);
        }

        public static Texture2D LoadFromPath(string _path)
        {
            _path = $"Assets/{_path}";

            AssetDatabase.ImportAsset(_path);
            TextureImporter A = (TextureImporter)AssetImporter.GetAtPath(_path);
            if (A == null)
            {
                return null;
            }

            A.isReadable = true;
            A.textureType = TextureImporterType.Default;
            A.npotScale = TextureImporterNPOTScale.None;

            AssetDatabase.ImportAsset(_path, ImportAssetOptions.ForceUpdate);
            return AssetDatabase.LoadAssetAtPath<Texture2D>(_path);
        }
        #endregion
    }

}