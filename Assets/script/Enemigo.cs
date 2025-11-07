using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float distanciaPatrulla = 5f;
    private Vector3 puntoInicial;
    private bool moviendoDerecha = true;
    
    [Header("Combate")]
    [SerializeField] private int danio = 10;
    [SerializeField] private float tiempoEntreDanios = 1f;
    private float tiempoUltimoDanio;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puntoInicial = transform.position;
    }

    void Update()
    {
        Patrullar();
    }
    
    private void Patrullar()
    {
        float distanciaRecorrida = transform.position.x - puntoInicial.x;
        
        if (moviendoDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            
            if (distanciaRecorrida >= distanciaPatrulla)
            {
                Voltear();
            }
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            
            if (distanciaRecorrida <= -distanciaPatrulla)
            {
                Voltear();
            }
        }
    }
    
    private void Voltear()
    {
        moviendoDerecha = !moviendoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= tiempoUltimoDanio + tiempoEntreDanios)
        {
            Persona persona = collision.gameObject.GetComponent<Persona>();
            if (persona != null)
            {
                persona.RecibirDanio(danio);
                tiempoUltimoDanio = Time.time;
            }
        }
    }
    
    public void RecibirDanio(int cantidad)
    {
        Destroy(gameObject);
    }
}
