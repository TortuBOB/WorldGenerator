using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //algunos de estos valores pueden cambian en las generaciones
    [Header("Vision")]
    public float rangoVision = 8f;
    public float rangoAtaque = 1f; 
    [Header("Velocidad")]
    public float velocidadAndar = 2f;
    public float velocidadCorrer = 3.5f;
    [Header("Energia")]
    public float energiaActual = 20f;
    public float energiaMaxima = 50f;

    public Animator animacion;
    public Rigidbody rb;

    //muerte con el tiempo
    private float deadTime = 200.0f;
    private float deadContador = 0.0f;

    //para controlar cuando pueden tener hijos
    [Header("Hijos")]
    public bool canHaveChild = false;
    private float childTime = 30.0f;
    private float childContador = 0.0f;
    

    public GameObject childPrefab;

    //mutaciones del aspecto
    public float tamaño = 1.0f;

    public Material[] colors;

    private Material myColor;

    void Start()
    {
        energiaActual = Random.Range(0, energiaMaxima);

        transform.localScale = new Vector3(tamaño, tamaño, tamaño);
        animacion = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        if (CompareTag("Cazador"))
        {
            childTime = 40.0f;
        }

    }

    private void Update()
    {
        childContador += Time.deltaTime;
        deadContador += Time.deltaTime;

        if (childContador >= childTime)
        {
            childContador = 0;
            if (canHaveChild)
            {
                canHaveChild = false;
            }
            else
            {
                canHaveChild = true;
            }
            
        }

        if(deadContador >= deadTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void HaveChild()
    {
        canHaveChild = false;
        childContador = -50;
    }

    public void RecogerDatos(Child child)
    {
        rangoVision = child.rangoVision;
        velocidadAndar = child.velocidadAndar;
        velocidadCorrer = child.velocidadCorrer;
        energiaMaxima = child.energiaMaxima;
        tamaño = child.tamaño;
        transform.localScale = new Vector3(tamaño, tamaño, tamaño);
        colors = child.colors;
        myColor = child.myColor;
        GetComponentInChildren<SkinnedMeshRenderer>().material = myColor;

    }

    private void OnDrawGizmosSelected()
    {
        //un gizmo para ver el rango
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoVision);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}
