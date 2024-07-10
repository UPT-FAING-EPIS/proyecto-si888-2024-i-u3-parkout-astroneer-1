using UnityEngine;

public class AsteroidReset : MonoBehaviour
{
    [SerializeField] private Transform checkpoint; // Asegúrate de asignar esto en el Inspector
    public Vector3 initialPosition;
    public Vector3 initialForce = new Vector3(2f, -2f, 0f);
    public float resetDelay = 0.5f;
    public float gravityScale = 0.5f;
    [SerializeField] private MonoBehaviour VistaScript; // Referencia al script de movimiento del jugador


    private Rigidbody rb;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(initialForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Invoke("ResetAsteroid", resetDelay);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out LavaTrigger controller))
            {
                controller.OnCollisionEnter(collision);
            }
            Invoke("ResetAsteroid", resetDelay);
        }
    }

    void ResetAsteroid()
    {
        gameObject.SetActive(false);

        transform.position = initialPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.SetActive(true);
        rb.AddForce(initialForce, ForceMode.Impulse);
    }
}