using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool dobleSalto = false;

    [Header("Movimiento")]
    [SerializeField] private float velocidadDeMovimiento;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);
        //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");

        if ((enSuelo || !dobleSalto) && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            enSuelo = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, fuerzaDeSalto);
            //rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
            if (!dobleSalto & !enSuelo)
            {
                dobleSalto = true;
            }
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        if (enSuelo)
        {
            dobleSalto = false;
        }
        animator.SetBool("esPiso", enSuelo);
        animator.SetFloat("VelocidadX", rb2D.velocity.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            animator.SetTrigger("Morir");
            GameManager.Instance.ShowGameOverScreen();
            Time.timeScale = 0f;
        }
    }
    /*private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }*/
}