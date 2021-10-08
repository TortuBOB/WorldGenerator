using UnityEngine;

public class Tile : MonoBehaviour
{
    bool canIntantiate;

    bool canInstantiateAnimal;

    private void Awake()
    {
        canIntantiate = true;
        canInstantiateAnimal = false;
    }

    public bool CanInstantiate()
    {
        return canIntantiate;
    }

    public void CantInstantiate()
    {
        canIntantiate = false;
    }
    public bool CanAnimal()
    {
        return canInstantiateAnimal;
    }
    public void ChangeAnimal(bool b)
    {
        canInstantiateAnimal = b;
    }
}
