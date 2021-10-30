using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCloserAction : IAction
{
    private Animal animal;
    private GameObject[] animales;
    public GameObject target;

    private GameObject anim;

    private float contador = 0f;
    private float time = 2f;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
        anim = animal.gameObject;
    }

    public override void Act()
    {
        //perseguir presa más cercana
        animales = GameObject.FindGameObjectsWithTag(anim.tag);
        foreach (GameObject ani in animales)
        {
            //que no sea el
            if(ani != anim)
            {
                //mirar si esta en rango
                if (Vector3.Distance(this.anim.transform.position, ani.transform.position) < animal.rangoVision)
                {
                    //si no hay aún nada cogemos el primero que este a rango
                    if (target == null)
                    {
                        target = ani;
                    }
                    //comprobar cual esta más cerca
                    if (Vector3.Distance(this.anim.transform.position, ani.transform.position) <
                        Vector3.Distance(this.anim.transform.position, target.transform.position))
                    {
                        target = ani;
                    }
                }
            }
            
        }

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

        if (target != null)
        {
            //perseguir
            Vector3 direc = target.transform.position - anim.transform.position;
            direc = direc.normalized;
            direc.y = 0;
            anim.transform.position += direc * animal.velocidadCorrer * Time.deltaTime;
            //rotacion
            Vector3 look = new Vector3(target.transform.position.x, anim.transform.position.y, target.transform.position.z);
            anim.transform.LookAt(look);
        }
        

        //energía.
        contador += Time.deltaTime;
        if (contador >= time)
        {
            animal.energiaActual--;
            contador = 0;

        }
    }
}