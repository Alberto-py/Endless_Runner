using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private AudioSource audioPlayer;

    public AudioClip jumpAudio;
    public AudioClip dieAudio;
    public AudioClip slidingAudio;

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
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);

        if ((enSuelo || !dobleSalto) && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.UpArrow))))
        {
            if(!animator.GetBool("Agachar") && !animator.GetBool("Morir"))
            {
                enSuelo = false;
                rb2D.velocity = new Vector2(rb2D.velocity.x, fuerzaDeSalto);
                audioPlayer.clip = jumpAudio;
                audioPlayer.Play();
                if ((!dobleSalto & !enSuelo))
                {
                    dobleSalto = true;
                }
            }
        }

        if((enSuelo) && (Input.GetKeyDown(KeyCode.DownArrow)) && !animator.GetBool("Morir"))
        {
            animator.SetBool("Agachar", true);
            this.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
            this.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.24f, -1.25f);
            this.GetComponent<CapsuleCollider2D>().size = new Vector2(2.19f, 0.6f);
            audioPlayer.clip = slidingAudio;
            audioPlayer.Play();
        }
        if ((enSuelo) && (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            animator.SetBool("Agachar", false);
            this.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
            this.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.213896f, -0.59f);
            this.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4f, 2f);
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
            animator.SetBool("Morir", true);
            if (animator.GetBool("Morir"))
            {
                GameManager.Instance.ShowGameOverScreen();
                Camera.main.GetComponent<AudioSource>().Stop();
                audioPlayer.clip = dieAudio;
                audioPlayer.Play();
            }

        }

    }
}
