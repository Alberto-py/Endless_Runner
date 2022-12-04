using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{

    [SerializeField] private Transform personaje;
    [SerializeField] private float separacion = 6.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(personaje.position.x + separacion, transform.position.y, transform.position.z);
    }
}
