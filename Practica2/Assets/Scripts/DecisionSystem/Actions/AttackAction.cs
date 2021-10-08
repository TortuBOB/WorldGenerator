using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : IAction
{
    public Animal hunter;
    private GameObject[] presas;
    public GameObject eleccion;

    void Start()
    {
        hunter = GetComponentInParent<Animal>();
    }
    public override void Act()
    {
        hunter.animacion.SetBool("Rest", false);
        hunter.animacion.SetBool("Walk", false);
        hunter.animacion.SetBool("Run", false);

        presas = GameObject.FindGameObjectsWithTag("Presa");
        foreach (GameObject presa in presas)
        {
            //mirar si esta en rango 
            //(añado uno al rango de ataque porque como la presa se sigue moviendo a veces no la cogia)
            if (Vector3.Distance(transform.position, presa.transform.position) < (hunter.rangoAtaque + 1))
            {
                //si no hay aún nada cogemos el primero que este a rango
                if (eleccion == null)
                {
                    eleccion = presa;
                }
                //comprobar cual esta más cerca
                if (Vector3.Distance(transform.position, presa.transform.position) <
                    Vector3.Distance(transform.position, eleccion.transform.position))
                {
                    eleccion = presa;
                }
            }
        }
        //destruir la presa
        Destroy(eleccion);
        //se queda sin energía
        hunter.energiaActual = 0;
    }
}

