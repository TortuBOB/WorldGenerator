using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int map;
    public Vector2Int tileSize;
    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public List<Biome> biomeList;
    public GameObject tileMapPrefab;

    private GameObject[] tileList;
    private TerrainChunk[] terrainsList;

    // Start is called before the first frame update
    void Start()
    {
        if (biomeList.Count == 0)
        {
            Debug.LogError("Insert biomes in the MapGenerator");
        }

        terrainsList = new TerrainChunk[biomeList.Count];
        for(int aux = 0; aux < terrainsList.Length; aux++)
        {
            terrainsList[aux] = new TerrainChunk();
            terrainsList[aux].AsingBiome(biomeList[aux]);
        }

        tileList = new GameObject[map.x * map.y];
        int index = 0;
        GameObject tileMap = GameObject.Instantiate(tileMapPrefab);
        for (float x = 0; x < map.x; x += tileSize.x)
        {
            for (float y = 0; y < map.y; y += tileSize.y)
            {
                GameObject intance = GameObject.Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity, tileMap.transform);
                tileList[index] = intance;
                index++;
                //create walls
                if (y == 0)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.Euler(0, 180, 0));
                }
                if (y == map.y-1)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
                if (x == 0)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.Euler(0, 270, 0));
                }
                if (x == map.x - 1)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.Euler(0, 90, 0));
                }
            }
        }

        GenerateMap();
        foreach(TerrainChunk terrain in terrainsList)
        {
            terrain.Initialize();
        }
        FindObjectOfType<GameManager>().InitialiceItemText();
    }


    void GenerateMap()
    {
        Vector2Int[] centroids = new Vector2Int[biomeList.Count];

        for (int i = 0; i < biomeList.Count; i++)
        {
            centroids[i] = new Vector2Int(Random.Range(0, map.x), Random.Range(0, map.y));
        }

        foreach(GameObject tile in tileList)
        {
            // Asigna un bioma al tile
            int index = GetClosestCentroidIndex(new Vector2(tile.transform.position.x, tile.transform.position.z), centroids);
            terrainsList[index].AddTile(tile);
        }
    }

    int GetClosestCentroidIndex(Vector2 tilePos, Vector2Int[] biomeCenters)
    {
        float smallestDst = float.MaxValue;
        int index = 0;
        for (int i = 0; i < biomeCenters.Length; i++)
        {
            if (Vector2.Distance(tilePos, biomeCenters[i]) < smallestDst)
            {
                // Distancia euclidea
                //smallestDst = Vector2.Distance(tilePos, centroids[i]);

                // Distancia Manhattan
                smallestDst = Mathf.Abs(tilePos.x - biomeCenters[i].x) + Mathf.Abs(tilePos.y - biomeCenters[i].y);

                index = i;
            }
        }
        return index;
    }
}
