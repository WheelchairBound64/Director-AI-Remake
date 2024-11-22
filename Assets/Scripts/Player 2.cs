using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
// comment test

public class Player : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    public SpawnEnemy enemySpawnScript;
    [SerializeField] public float speed;
    [SerializeField] Animator animator;
    [SerializeField] float sensitivity;
    [SerializeField] GameObject HasKey;
    [SerializeField] Transform lookPos;

    [SerializeField] GameObject keyhold;
    private float normalSpeed;
    private float rotX;
    private float rotY;

    private void Awake()
    {
        enemySpawnScript.Spawner();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        normalSpeed = speed;
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
        animator.SetBool("Key", true);
        gameObject.SetActive(true);
        keyhold.SetActive(true);
    }
    public void DropKey(bool hasKey)
    {
        speed = normalSpeed;
    }


}
