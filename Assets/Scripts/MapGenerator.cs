using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof (Tilemap))]
public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public float scale;
    public int octaves;

    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public int seed;

    public GeneratorHeightRule[] generatorHeightRules;

    private static readonly int MIN_OCTAVE_OFFSET = -10000;
    private static readonly int MAX_OCTAVE_OFFSET = 10000;

    public void Start()
    {
        GenerateMap();
    }

    public void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }

        if (height < 1)
        {
            height = 1;
        }

        if (scale < 1)
        {
            scale = 1;
        }

        if (octaves < 1)
        {
            octaves = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
    }

    public void GenerateMap()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        Vector3Int tilemapBasePosition = tilemap.WorldToCell(Vector3.zero);
        tilemap.ClearAllTiles();

        float[,] map = GeneratePerlinNoiseMap();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var position = new Vector3Int(tilemapBasePosition.x + j, tilemapBasePosition.y + i);
                Tile tile = GetTileByPercentHeight(map[i, j]);

                tilemap.SetTile(position, tile);
            }
        }
    }

    private float[,] GeneratePerlinNoiseMap()
    {
        var map = new float[height, width];

        float minNoiseValue = float.MaxValue;
        float maxNoiseValue = float.MinValue;

        Vector2[] randomOctavesOffsets = getRandomOctavesOffsets();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseValue = 0;

                for (int k = 0; k < octaves; k++)
                {
                    float x = (j / scale) * frequency + randomOctavesOffsets[k].x;
                    float y = (i / scale) * frequency + randomOctavesOffsets[k].y;

                    float perlinNoiseSample = Mathf.PerlinNoise(x, y) * 2 - 1;

                    noiseValue += perlinNoiseSample * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseValue > maxNoiseValue)
                {
                    maxNoiseValue = noiseValue;
                }
                else if (noiseValue < minNoiseValue)
                {
                    minNoiseValue = noiseValue;
                }

                map[i, j] = noiseValue;
            }
        }

        map = normalizeMapValues(map, minNoiseValue, maxNoiseValue);

        return map;
    }

    private Vector2[] getRandomOctavesOffsets()
    {
        var offsets = new Vector2[octaves];
        var random = new System.Random(seed);

        for (int i = 0; i < octaves; i++)
        {
            offsets[i].x = random.Next(MIN_OCTAVE_OFFSET, MAX_OCTAVE_OFFSET);
            offsets[i].y = random.Next(MIN_OCTAVE_OFFSET, MAX_OCTAVE_OFFSET);
        }

        return offsets;
    }

    private float[,] normalizeMapValues(float[,] map, float minValue, float maxValue)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = Mathf.InverseLerp(minValue, maxValue, map[i, j]);
            }
        }

        return map;
    }

    private Tile GetTileByPercentHeight(float percentHeight)
    {
        var heightRules = new List<GeneratorHeightRule>(generatorHeightRules);
        var heightRule = heightRules.Find((heightRule) => percentHeight >= heightRule.minPercentHeight && percentHeight <= heightRule.maxPercentHeight);

        if (heightRule == null)
        {
            print(percentHeight);
        }

        return heightRule.tile;
    }

    [System.Serializable]
    public class GeneratorHeightRule
    {
        public Tile tile;

        [Range(0, 1)]
        public float minPercentHeight;

        [Range(0, 1)]
        public float maxPercentHeight;
    }
}
