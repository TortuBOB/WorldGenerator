using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveChild : IAction
{
    public Animal animal;
    private GameObject[] animales;
    public GameObject eleccion;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }
    public override void Act()
    {
        eleccion = null;

        if (animal.gameObject.CompareTag("Turtle"))
        {
            animal.animacion.SetBool("Hide", false);
        }
        else
        {
            animal.animacion.SetBool("Run", false);
        }
        animal.animacion.SetBool("Walk", false);
        animal.animacion.SetBool("Rest", false);

        animales = GameObject.FindGameObjectsWithTag(animal.gameObject.tag);
        foreach (GameObject ani in animales)
        {
            if(ani != animal.gameObject)
            {
                //mirar si esta en rango 
                if (Vector3.Distance(transform.position, ani.transform.position) < (animal.rangoAtaque + 1))
                {
                    //si no hay aún nada cogemos el primero que este a rango
                    if (eleccion == null)
                    {
                        eleccion = ani;
                    }
                    //comprobar cual esta más cerca
                    if (Vector3.Distance(transform.position, ani.transform.position) <
                        Vector3.Distance(transform.position, eleccion.transform.position))
                    {
                        eleccion = ani;
                    }
                }
            }
            
        }
        if(eleccion != null)
        {
            if (animal.canHaveChild)
            {
                animal.HaveChild();
                Vector3 placeToCreate = animal.gameObject.transform.position;
                placeToCreate.y += 1;
                GameObject child = Instantiate(animal.childPrefab, placeToCreate, Quaternion.identity);
                Child childComponent = child.GetComponent<Child>();
                childComponent.RecogerDatos(animal, eleccion.GetComponent<Animal>());
            }
            
        }
       
    }
}
