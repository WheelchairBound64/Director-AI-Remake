using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] Stat current;
    [SerializeField] Stat max;
    [SerializeField] public float speed;
    [SerializeField] Animator animator;
    [SerializeField] float sensitivity;
    [SerializeField] GameObject HasKey;
    [SerializeField] Transform lookPos;

    [SerializeField] GameObject keyhold;
    [SerializeField] GameObject gun;
    [SerializeField] Transform gunEnd;
    [SerializeField] GameObject crosshair;
    PlayerInput playerMovement;

    [SerializeField] LineRenderer tracer;
    List<Vector3> tracerPoints = new List<Vector3>();

    public float normalSpeed;
    public float maxSpeed;
    private float rotX;
    private float rotY;

    private void Awake()
    {
        current.amount = max.amount;
        tracer.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        normalSpeed = speed;
        maxSpeed = speed;
        gameObject.tag = "Player";
        tracerPoints.Clear();
        gun.SetActive(true);
        playerMovement = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("MoveSpeed", Mathf.Abs(input.magnitude));

        // rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90, 81);

        transform.eulerAngles = new Vector3(0, rotY, 0);
        lookPos.eulerAngles = new Vector3(rotX, rotY, 0);

        if (Physics.Raycast(gunEnd.position, gunEnd.right, out RaycastHit hit, 9999999.0f))
        {
            crosshair.transform.forward = hit.normal;
            crosshair.transform.position = hit.point + hit.normal * 0.1f;
            crosshair.SetActive(true);
        }
        else
        {
            crosshair.SetActive(false);
        }
        if(current.amount <= 0)
        {
            SceneManager.LoadScene("Game");
        }
    }
    private void FixedUpdate()
    {
        var newInput = GetCameraBasedInput(input, Camera.main);
        var newVelocity = new Vector3(newInput.x * speed, rb.velocity.y, newInput.z * speed);

        rb.velocity = newVelocity;
    }
    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + input.y * camForward;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void WalkingKey(bool hasKey)
    {
        animator.SetBool("Key", hasKey);
        //gameObject.SetActive(hasKey);
        keyhold.SetActive(hasKey);
        
    }
    public void DropKey(bool hasKey)
    {
        speed = normalSpeed;
    }

    public void ShootGun(bool shooting)
    {
        playerMovement.actions.Disable();
        tracer.enabled = true;
        animator.SetBool("Shoot", true);
        gameObject.SetActive(true);
        //firing.SetActive(true);
        tracerPoints.Clear();
        tracerPoints.Add(gunEnd.position);
        if (Physics.Raycast(gunEnd.position, gunEnd.right, out RaycastHit hit, 9999999.0f))
        {
            tracerPoints.Add(hit.point);
            EnemyAI enemy;
            hit.transform.TryGetComponent<EnemyAI>(out enemy);
            if (enemy != null)
            {
                enemy.Damage();
            }

        }

        tracer.positionCount = tracerPoints.Count;
        for (int i = 0; i < tracer.positionCount; i++)
        {
            tracer.SetPosition(i, tracerPoints[i]);
        }

        StartCoroutine(Shoot());
    }

    public void disableGun()
    {
        if (gameObject.TryGetComponent<Player>(out Player player))    
        {
            if (player.speed <= 2)
            {
                player.animator.SetBool("Shoot", false);
            }
        }
    }

    IEnumerator Shoot()
    {
        speed = 0;
        yield return new WaitForSeconds(.5f);
        playerMovement.actions.Enable();
        speed = normalSpeed;
        animator.SetBool("Shoot", false);
        tracer.enabled = false;
        tracerPoints.Clear();
        StopCoroutine(Shoot());
        //player.ShootGun(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "KillTrigger")
        {
            SceneManager.LoadScene("Game");
        }
    }
}
