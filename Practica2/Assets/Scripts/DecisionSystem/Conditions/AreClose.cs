using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreClose : ICondition
{
    public Animal animal;
    private GameObject[] animales;
    public GameObject target;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }
    public override bool Test()
    {
        animales = GameObject.FindGameObjectsWithTag(animal.gameObject.tag);
        foreach (GameObject ani in animales)
        {
            if(ani != animal.gameObject)
            {
                //mirar si esta en rango
                if (Vector3.Distance(transform.position, ani.transform.position) < animal.rangoAtaque)
                {
                    return true;
                }
            }
            
        }
        return false;
        
    }
}