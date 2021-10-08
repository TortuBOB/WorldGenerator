using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public bool pollito = false;
    //algunos de estos valores pueden cambian en las generaciones
    [Header("Vision")]
    public float rangoVision;
    [Header("Velocidad")]
    public float velocidadAndar;
    public float velocidadCorrer;
    [Header("Energia")]
    public float energiaMaxima;

    public Animator animacion;
    public Rigidbody rb;

    public float tamaño;

    //color
    public Material[] colors;

    public GameObject model;
    public Material myColor;

    //grow
    private float growTime = 50.0f;
    private float growContador = 0.0f;
    public GameObject adultModel;

    private GameObject target;
    private float speed;
    private Player player;
    private float distance = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animacion = GetComponentInChildren<Animator>();
        animacion.SetBool("Rest", false);
        animacion.SetBool("Walk", false);
        if (CompareTag("ChildTurtle"))
        {
            animacion.SetBool("Hide", false);
            player = FindObjectOfType<Player>();
        }
        if (pollito)
        {
            distance = 0.5f;
        }
    }

    void Update()
    {
        //crecer
        growContador += Time.deltaTime;
        if (growContador >= growTime)
        {
            growContador = 0;
            Crecer();
        }
        //rotacion
        Vector3 look = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(look);

        //movimiento
        if (Vector3.Distance(target.transform.position, transform.position) > distance)
        {
            if (animacion.GetBool("Walk") == false)
            {
                animacion.SetBool("Walk", true);
            }
            Vector3 direc = target.transform.position - transform.position;
            direc = direc.normalized;
            direc.y = 0;
            transform.position += direc * speed * Time.deltaTime;
        }
        else
        {
            animacion.SetBool("Walk", false);
            if (CompareTag("ChildTurtle"))
            {
                if (Vector3.Distance(transform.position, player.gameObject.transform.position) < rangoVision)
                {
                    animacion.SetBool("Hide", true);
                }
                else
                {
                    animacion.SetBool("Hide", false);
                }
            }
            
        }

        
    }

    private void Crecer()
    {
        GameObject adult = Instantiate(adultModel, transform.position, Quaternion.identity);
        adult.GetComponent<Animal>().RecogerDatos(this);
        Destroy(this.gameObject);
    }

    public void RecogerDatos(Animal ani1, Animal ani2)
    {
        growContador = 0;
        target = ani1.gameObject;
        speed = ani1.velocidadAndar;
        //random
        rangoVision = Random.Range(ani1.rangoVision, ani2.rangoVision) + Random.Range(-1, 2);
        velocidadAndar = Random.Range(ani1.velocidadAndar, ani2.velocidadAndar) + Random.Range(-0.2f, 0.3f);
        velocidadCorrer = Random.Range(ani1.velocidadCorrer, ani2.velocidadCorrer) + Random.Range(-0.2f, 0.3f);
        energiaMaxima = Random.Range(ani1.energiaMaxima, ani2.energiaMaxima) + Random.Range(-5, 6);
        tamaño = Random.Range(ani1.tamaño, ani2.tamaño) + Random.Range(-0.2f, 0.3f);
        colors = ani1.colors;
        int i = Random.Range(0, colors.Length);
        myColor = colors[i];
        if (!pollito)
        {
            model.GetComponent<SkinnedMeshRenderer>().material = myColor;
        }
        

    }

}
