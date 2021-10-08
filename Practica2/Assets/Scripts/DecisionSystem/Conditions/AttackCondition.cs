using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCondition : ICondition
{
    public Animal hunter;
    private GameObject[] presas;

    void Start()
    {
        hunter = GetComponentInParent<Animal>();
    }
    public override bool Test()
    {
        presas = GameObject.FindGameObjectsWithTag("Presa");
        foreach (GameObject presa in presas)
        {
            //mirar si esta en rango
            if (Vector3.Distance(transform.position, presa.transform.position) < hunter.rangoAtaque - 0.5)
            {
                return true;
            }

        }
        return false;
        
    }
}