using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAction : IAction
{
    private Animal turtle;

    void Start()
    {
        turtle = GetComponentInParent<Animal>();
    }
    public override void Act()
    {
        if (turtle.animacion.GetBool("Hide") == false)
        {
            turtle.animacion.SetBool("Rest", false);
            turtle.animacion.SetBool("Walk", false);
            turtle.animacion.SetBool("Hide", true);
        }
        
    }
}
