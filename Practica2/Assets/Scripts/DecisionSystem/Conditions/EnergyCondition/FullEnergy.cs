using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullEnergy : ICondition
{
    public Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }

    public override bool Test()
    {
        if (animal.energiaActual < animal.energiaMaxima)
        {
            return false;
        }
        else
        {
            animal.energiaActual = animal.energiaMaxima;
            return true;
        }
    }
}
