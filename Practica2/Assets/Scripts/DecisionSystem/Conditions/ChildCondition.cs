using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCondition : ICondition
{
    public Animal animal;
    private GameObject[] animalesWithTag;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }
    public override bool Test()
    {
        if (!animal.canHaveChild)
        {
            return false;
        }
        animalesWithTag = GameObject.FindGameObjectsWithTag(animal.gameObject.tag);
        foreach(GameObject ani in animalesWithTag)
        {
            //que no sea el
            if(ani != animal.gameObject)
            {
                //mirar si esta en rango
                if (Vector3.Distance(transform.position, ani.transform.position) < animal.rangoVision)
                {
                    if (ani.GetComponent<Animal>().canHaveChild)
                    {
                        return true;
                    }
                }
            }
            
        }
        return false;
    }
}
