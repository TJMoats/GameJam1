using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class QuickSnap : SerializedMonoBehaviour
{
    private void OnValidate()
    {
        CleanUp();
    }

    public void Snap()
    {
        for (int y = 0; y < transform.childCount; y++)
        {
            Transform row = transform.GetChild(y);
            row.localPosition = new Vector3(0, y * -1, 0);
            row.name = $"Row_{y}";
            Reorder(row);
            for (int x = 0; x < row.childCount; x++)
            {
                Transform cell = row.GetChild(x);
                cell.localPosition = new Vector3(x, 0, 0);
                cell.name = $"Sprite_{x}_{y}";
            }
        }
    }

    public void Reorder(Transform _transform)
    {
        for (int i = 0; i < _transform.childCount - 1; i++)
        {
            for (int j = 0; j < _transform.childCount - i - 1; j++)
            {
                if (_transform.GetChild(j).position.x > _transform.GetChild(j + 1).position.x)
                {
                    Swap(_transform.GetChild(j), _transform.GetChild(j + 1));
                }
            }
        }
    }

    private void Swap(Transform _a, Transform _b)
    {
        _a.SetSiblingIndex(_a.GetSiblingIndex() + 1);
    }

    private void GeneratePNG()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers.Length == 0)
        {
            return;
        }
        VerifyAllAssets(spriteRenderers);
        Vector2 dimensions = GetDimensions(spriteRenderers);
        int ppu = Mathf.FloorToInt(GetPPU(spriteRenderers));

        Texture2D sheet = GenerateBlankTexture(Mathf.FloorToInt(dimensions.x * ppu), Mathf.FloorToInt(dimensions.y * ppu));
        sheet = AddSpritesToSheet(sheet, spriteRenderers);
        string spriteName = gameObject.name;
        string assetPath = $"Assets/{spriteName}.png";
        SavePng(sheet, assetPath);
        FixFinalSheet(assetPath, ppu);
    }

    private void VerifyAllAssets(SpriteRenderer[] _spriteRenderers)
    {
        verifiedAssets = new List<string>();
        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            if (renderer.sprite != null)
            {
                VerifyAsset(AssetDatabase.GetAssetPath(renderer.sprite.texture));
            }
        }
    }

    private List<string> verifiedAssets = new List<string>();
    private void VerifyAsset(string _assetPath)
    {
        if (verifiedAssets.Contains(_assetPath))
        {
            return;
        }
        Debug.Log($"Verifying {_assetPath}");
        AssetDatabase.ImportAsset(_assetPath);
        TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(_assetPath);
        textureImporter.isReadable = true;

        TextureImporterSettings textureSettings = new TextureImporterSettings();
        textureImporter.ReadTextureSettings(textureSettings);
        textureSettings.spriteMeshType = SpriteMeshType.FullRect;
        textureImporter.SetTextureSettings(textureSettings);
        AssetDatabase.ImportAsset(_assetPath, ImportAssetOptions.ForceUpdate);

        verifiedAssets.Add(_assetPath);
    }

    private Vector2 GetDimensions(SpriteRenderer[] _spriteRenderers)
    {
        float x = float.PositiveInfinity, y = float.PositiveInfinity, x2 = float.NegativeInfinity, y2 = float.NegativeInfinity;

        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            float spriteMinX = renderer.transform.position.x - renderer.sprite.bounds.extents.x;
            float spriteMinY = renderer.transform.position.y - renderer.sprite.bounds.extents.y;
            float spriteMaxX = renderer.transform.position.x + renderer.sprite.bounds.extents.x;
            float spriteMaxY = renderer.transform.position.y + renderer.sprite.bounds.extents.y;

            x = x > spriteMinX ? spriteMinX : x;
            y = y > spriteMinY ? spriteMinY : y;
            x2 = x2 < spriteMaxX ? spriteMaxX : x2;
            y2 = y2 < spriteMaxY ? spriteMaxY : y2;
        }

        return new Vector2(x2 - x, y2 - y);
    }

    private float GetPPU(SpriteRenderer[] _spriteRenderers)
    {
        float ppu = _spriteRenderers[0].sprite.pixelsPerUnit;
        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            if (ppu != renderer.sprite.pixelsPerUnit)
            {
                throw new System.Exception("PPU Missmatch");
            }
        }
        return ppu;
    }

    private Texture2D GenerateBlankTexture(int _width, int _height)
    {
        Debug.Log($"Creating a {_width} x {_height} texture");
        Texture2D tex = new Texture2D(_width, _height);
        Color[] colors = new Color[_width * _height];
        NPS.ArrayHelper.PopulateArray<Color>(colors, (_index) => new Color(1, 1, 1, 0));
        tex.SetPixels(colors);
        return tex;
    }

    private Texture2D AddSpritesToSheet(Texture2D _sheet, SpriteRenderer[] _spriteRenderers)
    {
        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            Vector2Int position = new Vector2Int(Mathf.FloorToInt(renderer.transform.position.x), Mathf.FloorToInt(renderer.transform.position.y));
            _sheet = AddSpriteToSheet(_sheet, renderer.sprite, position);
        }
        return _sheet;
    }

    private Texture2D AddSpriteToSheet(Texture2D _sheet, Sprite _sprite, Vector2Int _position)
    {
        Color[] textureToAdd = _sprite.texture.GetPixels((int)_sprite.textureRect.x, (int)_sprite.textureRect.y, (int)_sprite.textureRect.width, (int)_sprite.textureRect.height);
        if (textureToAdd != null)
        {
            int xStart = Mathf.FloorToInt(_position.x * _sprite.rect.width);
            int yStart = Mathf.FloorToInt((_sheet.height + ((_position.y) * _sprite.rect.height)) - _sprite.rect.height);
            int blockWidth = (int)_sprite.rect.width;
            int blockHeight = (int)_sprite.rect.height;

            // blend the transparent parts of the textures
            Color[] textureToMerge = _sheet.GetPixels(xStart, yStart, blockWidth, blockHeight);
            for (int i = 0; i < textureToAdd.Length; i++)
            {
                if (textureToAdd[i].a != 1)
                {
                    textureToAdd[i] = Color.Lerp(textureToMerge[i], textureToAdd[i], textureToAdd[i].a);
                }
            }
            _sheet.SetPixels(xStart, yStart, blockWidth, blockHeight, textureToAdd);
        }
        return _sheet;
    }

    private void SavePng(Texture2D _texture, string _path)
    {
        byte[] bytes = _texture.EncodeToPNG();
        File.WriteAllBytes(_path, bytes);
        Debug.Log("File written to " + _path);
    }

    private void FixFinalSheet(string _path, int _ppu)
    {
        AssetDatabase.ImportAsset(_path);
        TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(_path);
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spriteImportMode = SpriteImportMode.Single;
        textureImporter.spritePixelsPerUnit = _ppu;
    }

    [Button, HorizontalGroup]
    public void CleanUp()
    {
        Snap();
    }

    [Button, HorizontalGroup]
    public void Flatten()
    {
        GeneratePNG();
    }
}
