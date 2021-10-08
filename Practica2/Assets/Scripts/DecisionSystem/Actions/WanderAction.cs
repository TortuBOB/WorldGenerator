using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : IAction
{
    private Animal animal;

    private float contador = 0f;
    private float time = 1f;

    public Vector3 destination;

    private float wanderTime = 5f;
    private float wanderContador = 5f;

    private GameObject anim;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
        anim = animal.gameObject;
        
    }

    public override void Act()
    {
        //animación
        if (animal.animacion.GetBool("Walk") == false)
        {
            if (anim.CompareTag("Turtle"))
            {
                animal.animacion.SetBool("Hide", false);
            }
            else
            {
                animal.animacion.SetBool("Run", false);
            }
            animal.animacion.SetBool("Rest", false);
            animal.animacion.SetBool("Walk", true);
        }

        //andar
        wanderContador += Time.deltaTime;
        if (wanderContador >= wanderTime || destination == transform.position)
        {
            wanderContador = 0;
            destination = new Vector3(anim.transform.position.x + Random.Range(-10, 10), 
                anim.transform.position.y, anim.transform.position.z + Random.Range(-10, 10));

            anim.transform.LookAt(destination);
            
        }
        anim.transform.position += anim.transform.forward * animal.velocidadAndar * Time.deltaTime;

        //energía.
        contador += Time.deltaTime;
        if (contador >= time)
        {
            animal.energiaActual--;
            contador = 0;
            
        }
    }
}
