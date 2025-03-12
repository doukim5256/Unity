using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f; // 최대 속도
    public float acceleration = 5f; // 자연스러운 움직임
    public float deceleration = 5f; // 자연스러운 움직임 
    public float turnSpeed = 60f;
    public float speedThreshold = 2f; // 일정 속도 이상일 때만 회전 가능 
    public TextMeshProUGUI countText;
    public GameObject EndText;
    public GameObject StartPanel; // 시작화면
    public GameObject EndPanel; // 종료화면 

    
    private Rigidbody rb;
    private int count; // 수집 카운트 
    private GameManager gameManager;


    private Vector2 moveInput;
    private float currentSpeed = 0f; // 현재 속도 저장 

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        count = 0;
        SetCountText();
        EndText.SetActive(false);

        gameManager = FindFirstObjectByType<GameManager>();

    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 3)
        {
            EndText.SetActive(true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
      //가속도 적용
        if(moveInput.y != 0)
        {
            currentSpeed += moveInput.y * acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed); // currentSpeed 값이 -maxSpeed보다 작아지거나 maxSpeed보다 커지는 걸 방지
        }
        else
        {  //감속  Mathf.MoveTowards()  값을 부드럽게 변화 시키는 함수. 여기서는 currentSpeed 를 0에 서서히 가까워지도록. 
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        Vector3 moveDirection = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        if(Mathf.Abs(currentSpeed) > speedThreshold) //특정 속도 이상일 때만 회전
        { //Mathf.Abs() → 절댓값 구하는 함수 (속도 비교할 때 유용)
            float adjustedTurnSpeed = turnSpeed * (Mathf.Abs(currentSpeed) / maxSpeed);
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime * Mathf.Sign(currentSpeed); //Mathf.Sign()  부호 반환 함수
            transform.Rotate(Vector3.up * turn);
        }

    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            gameManager.CollectObject();
            Destroy(other.gameObject);
        }
    
    }

    
}
