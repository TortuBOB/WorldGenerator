using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAction : IAction
{
    private Animal animal;

    private float contador = 0f;
    private float time = 0.5f;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }

    public override void Act()
    {
        if (animal.animacion.GetBool("Rest") == false)
        {
            if (animal.gameObject.CompareTag("Turtle"))
            {
                animal.animacion.SetBool("Hide", false);
            }
            else
            {
                animal.animacion.SetBool("Run", false);
            }
            animal.animacion.SetBool("Walk", false);
            animal.animacion.SetBool("Rest", true);
        }

        //subir energía
        contador += Time.deltaTime;
        if (contador >= time)
        {
            animal.energiaActual++;
            contador = 0;
        }
    }
}
