using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages asset instantiation and terrain chunks overall
/// </summary>
public class TerrainChunk
{
    // Nos interesaría tener el offset en una clase estática
    float offset = 0.2f;
    
    private Biome biome;
    private List<GameObject> tileList;

    // Start is called before the first frame update
    public TerrainChunk()
    {
        biome = null;
        tileList = new List<GameObject>();
    }

    public void AsingBiome(Biome newBiome)
    {
        biome = newBiome;
    }

    public void AddTile(GameObject tile)
    {
        tileList.Add(tile);
    }

    public void Initialize()
    {
        foreach(GameObject tile in tileList)
        {
            tile.GetComponent<MeshRenderer>().material.mainTexture = biome.floorTexture;
        }

        PopulateTerrain();
    }

    public void PopulateTerrain()
    {
        foreach(GameObject tileGO in tileList)
        {
            Tile tile = tileGO.GetComponent<Tile>();
            if(tile.CanInstantiate())
            {
                if(biome.biomeAssets.Count != 0)
                {
                    int randomIndex = Random.Range(0, biome.biomeAssets.Count);
                    GameObject assetToInstantiateGO = biome.biomeAssets[randomIndex];
                    Asset assetToInstantiate = assetToInstantiateGO.GetComponent<Asset>();

                    if(assetToInstantiate == null)
                    {
                        Debug.LogError("One of the assets has no Asset component attached");
                    }
                    if (HasSpace(assetToInstantiate.size, tile))
                    {
                        IntanciateAsset(assetToInstantiate, tile);
                    }
                    else
                    {
                        tile.CantInstantiate();
                    }
                }
                
            }
            
            
        }
        foreach (GameObject tileGO in tileList)
        {
            Tile tile = tileGO.GetComponent<Tile>();
            //comprobar que se puede instanciar un animal
            if (tile.CanAnimal())
            {
                if (biome.animalAssets.Count != 0)
                {
                    //elegir animal a instanciar
                    int randomIndex = Random.Range(0, biome.animalAssets.Count);
                    GameObject animalToInstantiate = biome.animalAssets[randomIndex];
                    Asset animal = animalToInstantiate.GetComponent<Asset>();

                    if (animalToInstantiate == null)
                    {
                        Debug.LogError("One of the assets has no Asset component attached");
                    }
                    if (AnimalHasSpace(animal.size, tile))
                    {
                        //crear los animales
                        IntanciateAnimal(animal, tile);
                    }
                    else
                    {
                        tile.ChangeAnimal(false);
                    }
                        
                }
            }
        }
            
    }

    private void IntanciateAsset(Asset asset, Tile tile)
    {
        asset.Instance(tile.transform.position);
        asset.CanInstantiateAnimal(tile);

        // Closing all the tiles to avoid double instantiation
        float intialX = tile.transform.position.x;
        float intialY = tile.transform.position.z;

        for (float posX = intialX; posX < intialX + asset.size.x; posX++)
        {
            for (float posY = intialY; posY < intialY + asset.size.y; posY++)
            {
                Tile currentTile = GetTileFromCoordinates(posX, posY);
                currentTile.CantInstantiate();
            }
        }
    }

    

    private bool HasSpace(Vector2 assetSize, Tile tile)
    {
        float intialX = tile.transform.position.x;
        float intialY = tile.transform.position.z;

        for (float posX = intialX; posX < intialX + assetSize.x; posX++)
        {
            for(float posY = intialY; posY < intialY + assetSize.y; posY++)
            {
                Tile currentTile = GetTileFromCoordinates(posX, posY);
                if(currentTile == null)
                {
                    return false;
                }
                if (!currentTile.CanInstantiate())
                {
                    return false;
                }
            }
        }

        return true;
    }
    private void IntanciateAnimal(Asset animal, Tile tile)
    {
        animal.Instance(tile.transform.position);

        float intialX = tile.transform.position.x;
        float intialY = tile.transform.position.z;

        for (float posX = intialX; posX < intialX + animal.size.x; posX++)
        {
            for (float posY = intialY; posY < intialY + animal.size.y; posY++)
            {
                Tile currentTile = GetTileFromCoordinates(posX, posY);
                currentTile.ChangeAnimal(false);
            }
        }
    }
    private bool AnimalHasSpace(Vector2 assetSize, Tile tile)
    {
        float intialX = tile.transform.position.x;
        float intialY = tile.transform.position.z;

        for (float posX = intialX; posX < intialX + assetSize.x; posX++)
        {
            for (float posY = intialY; posY < intialY + assetSize.y; posY++)
            {
                Tile currentTile = GetTileFromCoordinates(posX, posY);
                if (currentTile == null)
                {
                    return false;
                }
                if (!currentTile.CanAnimal())
                {
                    return false;
                }
            }
        }

        return true;
    }
    private Tile GetTileFromCoordinates(float coordinateX, float coordinateY)
    {
        Vector2 coordinates = new Vector2(coordinateX, coordinateY);

        foreach (GameObject tileGO in tileList)
        {
            if(Vector2.Distance(new Vector2(tileGO.transform.position.x, tileGO.transform.position.z), coordinates) <= offset)
            {
                return tileGO.GetComponent<Tile>();
            }
        }

        // Debug.Log("La coordenada que buscas no tiene casilla asociada");
        return null;
    }
}
