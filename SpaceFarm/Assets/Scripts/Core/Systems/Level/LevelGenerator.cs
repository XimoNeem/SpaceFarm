using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Tile[] _tilePrefabs;
    [SerializeField] private Tile _baseTile;
    public List<Tile> Tiles;

    [SerializeField] private int _lenght = 20;
    [SerializeField] private int _height = 20;

    [SerializeField] private Vector2 _tileSize;

    [SerializeField] private Transform _levelParent;

    [SerializeField] private float _noiseScale = 0.05f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void GenerateLevel()
    {
        _levelParent = new GameObject("LevelParent").transform;
        Tiles = new List<Tile>();

        for (int h = 0; h < _height; h++)
        {
            for (int l = 0; l < _lenght; l++)
            {
                float xPos = l * (_tileSize.x / 2) - h * (_tileSize.x / 2);
                float yPos = (h * _tileSize.y) + (_tileSize.y * l);

                float noise = Mathf.PerlinNoise(l * _noiseScale, h * _noiseScale);

                Vector3 pos = new(xPos, -yPos, _levelParent.transform.position.z);

                if (noise > 0.4f)
                {
                    float a = noise / 1.25f;
                    int b = (int)(a * 10) - 3;
                    pos += new Vector3(0, 0.15f * b, 0);
                }

                CreateTile(SelectTile(noise).gameObject, pos, (l + h) * 2);
            }
        }
    }

    private void CreateTile(GameObject prefab, Vector3 position, int depth)
    {
        GameObject newTile = Instantiate(prefab, _levelParent);
        newTile.transform.position = position;

        if (UnityEngine.Random.Range(0, 10) < 5)
        {
            newTile.transform.localScale = new Vector3(-1, 1, 1);
        }

        Tile tile = newTile.GetComponent<Tile>();
        tile.SetDepth(depth);
        Tiles.Add(tile);
    }

    private Tile SelectTile(float value)
    {
        List<Tile> tiles = new List<Tile>();
        float totalWeights = 0;

        foreach (var tile in _tilePrefabs)
        {
            if (value > tile.SpawnHeight.x & value < tile.SpawnHeight.y)
            {
                tiles.Add(tile);
                totalWeights += tile.SpawnWeight;
            }
        }

        if (tiles.Count == 0) { return _baseTile; }

        float randomPoint = UnityEngine.Random.Range(0, totalWeights);

        foreach (var item in tiles)
        {
            if (randomPoint < item.SpawnWeight) { return item; }
            else { randomPoint -= item.SpawnWeight; }
        }

        return tiles[UnityEngine.Random.Range(0, tiles.Count)];
    }
}