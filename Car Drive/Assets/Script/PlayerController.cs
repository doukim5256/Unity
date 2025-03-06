using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    public float turnSpeed = 50f;

    private Vector2 moveInput;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // 앞뒤 이동 (Vector2.y 사용)
        Vector3 moveDirection = transform.forward * moveInput.y * speed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        // 좌우 회전 (Vector2.x 사용)
        if (moveInput.y != 0) // 이동 중일 때만 회전 적용
        {
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime* Mathf.Sign(moveInput.y);;
            transform.Rotate(Vector3.up * turn);
        }
    }
}
