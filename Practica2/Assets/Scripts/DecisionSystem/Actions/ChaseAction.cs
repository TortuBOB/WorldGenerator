using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : IAction
{
    private Animal animal;
    private GameObject[] presas;
    public GameObject target;

    private GameObject anim;

    private float contador = 0f;
    private float time = 1f;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
        anim = animal.gameObject;
    }

    public override void Act()
    {
        //perseguir presa más cercana
        presas = GameObject.FindGameObjectsWithTag("Presa");
        foreach (GameObject presa in presas)
        {
            //mirar si esta en rango
            if (Vector3.Distance(anim.transform.position, presa.transform.position) < animal.rangoVision)
            {
                //si no hay aún nada cogemos el primero que este a rango
                if (target == null)
                {
                    target = presa;
                }
                //comprobar cual esta más cerca
                if (Vector3.Distance(anim.transform.position, presa.transform.position) <
                    Vector3.Distance(anim.transform.position, target.transform.position))
                {
                    target = presa;
                }
            }
        }

        //animación
        if (animal.animacion.GetBool("Run") == false)
        {
            animal.animacion.SetBool("Rest", false);
            animal.animacion.SetBool("Walk", false);
            animal.animacion.SetBool("Run", true);
        }

        //perseguir
        Vector3 direc = target.transform.position - anim.transform.position;
        direc = direc.normalized;
        direc.y = 0;
        anim.transform.position += direc * animal.velocidadCorrer * Time.deltaTime;
        //rotacion
        Vector3 look = new Vector3(target.transform.position.x, anim.transform.position.y, target.transform.position.z);
        anim.transform.LookAt(look);

        //energía.
        contador += Time.deltaTime;
        if (contador >= time)
        {
            animal.energiaActual--;
            contador = 0;

        }
    }
}
