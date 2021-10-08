using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnergy : ICondition
{
    public Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }

    public override bool Test()
    {
        if (animal.energiaActual <= 0)
        {
            animal.energiaActual = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}