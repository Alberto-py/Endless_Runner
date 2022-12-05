using UnityEngine;

public class ControladorCamara : MonoBehaviour
{

    [SerializeField] private Transform personaje;
    [SerializeField] private float separacion = 6.75f;
    public Renderer fondo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(personaje.position.x + separacion, transform.position.y, transform.position.z);
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.020f, 0) * Time.deltaTime;
    }
}
