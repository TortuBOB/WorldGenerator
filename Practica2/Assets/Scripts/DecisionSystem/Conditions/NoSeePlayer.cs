using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSeePlayer : ICondition
{
    private Animal animal;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
        player = FindObjectOfType<Player>();
    }
    public override bool Test()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < animal.rangoVision)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

