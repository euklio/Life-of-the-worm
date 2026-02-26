using UnityEngine;


public class Mover : MonoBehaviour
{
    public bool movementEnabled = true;
    [Header("Mode")]
    public bool playerControlled = false;

    [Header("Player settings (used if playerControlled)")]
    public float playerSpeed = 5f;
    public float playerSprintMultiplier = 2f;

    [Header("Auto settings (used if NOT playerControlled)")]
    public Vector3 direction = Vector3.forward;
    public float speed = 2f;

    [Header("Options")]
    public bool useLocalDirection = true;
    public bool stopOnCollision = false;

    Rigidbody rb;
    Vector3 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (playerControlled)
        {
            HandlePlayerMovement();
        }
        else
        {
            HandleAutoMovement();
        }
        if (playerControlled)
        {
            HandlePlayerMovement();
        }
        else
        {
            HandleAutoMovement();
        }
    }
    public float ubrzanje = 8f;
    public float usporavanje = 8f;
    public float maksimalnabrzina = 12f;

    Vector3 trenutnabrzina = Vector3.zero;

    void HandlePlayerMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Rotate(0f, h * 100f * Time.fixedDeltaTime, 0f);

        Vector3 target = transform.forward * v * maksimalnabrzina;

        trenutnabrzina = Vector3.MoveTowards(
            trenutnabrzina,
            target,
            (v != 0 ? ubrzanje : usporavanje) * Time.fixedDeltaTime
        );

       
        Vector3 flatVel = new Vector3(trenutnabrzina.x, 0, trenutnabrzina.z);
        if (flatVel.magnitude > maksimalnabrzina)
            flatVel = flatVel.normalized * maksimalnabrzina;

        trenutnabrzina = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);

        rb.linearVelocity = trenutnabrzina;
    }

    void HandleAutoMovement()
    {
        Vector3 dir = useLocalDirection ? transform.TransformDirection(direction.normalized) : direction.normalized;
        Vector3 desiredVel = dir * speed;
        desiredVel.y = rb.linearVelocity.y;

        rb.linearVelocity = desiredVel;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (stopOnCollision && !playerControlled)
        {
            rb.linearVelocity = Vector3.zero;
            enabled = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementEnabled = !movementEnabled;
            RenderSettings.fog = true;

            if (!movementEnabled)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                RenderSettings.fog = false;
            }
        }
    }
    public void StopMovement()
    {
        movementEnabled = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
   
       
  

}