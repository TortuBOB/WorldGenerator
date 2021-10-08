using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSeeSomething : ICondition
{
    public string tagToSee = "Cazador";
    public Animal animal;
    private GameObject[] animalesWithTag;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }

    public override bool Test()
    {
        animalesWithTag = GameObject.FindGameObjectsWithTag(tagToSee);
        foreach (GameObject presa in animalesWithTag)
        {
            //mirar si esta en rango
            if (Vector3.Distance(transform.position, presa.transform.position) < animal.rangoVision)
            {
                return false;
            }

        }
        return true;
    }
}