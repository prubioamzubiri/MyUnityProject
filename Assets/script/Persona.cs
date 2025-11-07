using UnityEngine;

public class Persona : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 10f;
    private Rigidbody2D rb;
    private bool enSuelo = true;
    
    [Header("Disparo")]
    [SerializeField] private GameObject flechaPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float fuerzaFlecha = 15f;
    [SerializeField] private float tiempoEntreDisparos = 0.5f;
    private float tiempoUltimoDisparo;
    
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 100;
    private int vidaActual;
    
    private bool mirandoDerecha = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicialización
        Debug.Log("Inicialización");
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            Saltar();
        }
        
        if (Input.GetButtonDown("Fire1") && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
        }
    }
    
    private void Mover()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.velocity.y);
        
        // Voltear sprite según dirección
        if (movimientoHorizontal > 0 && !mirandoDerecha)
        {
            Voltear();
        }
        else if (movimientoHorizontal < 0 && mirandoDerecha)
        {
            Voltear();
        }
    }
    
    private void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        enSuelo = false;
    }
    
    private void Disparar()
    {
        if (flechaPrefab != null && puntoDisparo != null)
        {
            GameObject flecha = Instantiate(flechaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            Rigidbody2D rbFlecha = flecha.GetComponent<Rigidbody2D>();
            
            if (rbFlecha != null)
            {
                float direccion = mirandoDerecha ? 1f : -1f;
                rbFlecha.velocity = new Vector2(direccion * fuerzaFlecha, 0);
            }
            
            tiempoUltimoDisparo = Time.time;
        }
    }
    
    private void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = true;
        }
    }
    
    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Vida actual: " + vidaActual);
        
        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    
    private void Morir()
    {
        Debug.Log("Game Over");
        // Aquí puedes añadir lógica de muerte (reiniciar nivel, mostrar pantalla de game over, etc.)
        gameObject.SetActive(false);
    }
}
