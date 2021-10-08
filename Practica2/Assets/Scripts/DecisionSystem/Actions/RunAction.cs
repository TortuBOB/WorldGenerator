using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : IAction
{
    public Animal animal;
    private GameObject[] cazadores;
    public GameObject target;

    private GameObject anim;

    private float contador = 0f;
    private float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
        anim = animal.gameObject;
    }

    public override void Act()
    {
        //mirar los cazadores
        cazadores = GameObject.FindGameObjectsWithTag("Cazador");
        foreach (GameObject cazador in cazadores)
        {
            //mirar si esta en rango
            if (Vector3.Distance(anim.transform.position, cazador.transform.position) < animal.rangoVision)
            {
                //si no hay aún nada cogemos el primero que este a rango
                if (target == null)
                {
                    target = cazador;
                }
                //comprobar cual esta más cerca
                if (Vector3.Distance(anim.transform.position, cazador.transform.position) <
                    Vector3.Distance(anim.transform.position, target.transform.position))
                {
                    target = cazador;
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

        //huir
        Vector3 direc = anim.transform.position - target.transform.position;
        direc.y = 0;
        //rotacion
        Vector3 look = anim.transform.position + direc;
        anim.transform.LookAt(look);
        //movimiento
        anim.transform.position += direc.normalized * animal.velocidadCorrer * Time.deltaTime;
        
        

        //energía
        contador += Time.deltaTime;
        if (contador >= time)
        {
            animal.energiaActual--;
            contador = 0;
            
        }


    }
}