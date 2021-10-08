using UnityEngine;

public class Asset : MonoBehaviour
{
    public Vector2 size;
    public GameObject assetPrefab;
    public int chance = 100;

    private bool canInstantiateAnimal = false;

    public void Instance(Vector3 position)
    {
        int rand = Random.Range(0, 100);
        if(rand <= chance)
        {
            Instantiate(assetPrefab, position, Quaternion.identity);
        }
        else
        {
            canInstantiateAnimal = true;
        }
    }
    public void CanInstantiateAnimal(Tile tile)
    {
        tile.ChangeAnimal(canInstantiateAnimal);
    }
}
