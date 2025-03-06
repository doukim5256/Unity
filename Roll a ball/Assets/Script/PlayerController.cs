using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start는 첫 프레임 업데이트 이전에 호출됩니다.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // collect count

        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {   //other 의 태그를 확인하고 Active를 false로 
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);   
            count = count + 1; // 물체에 접촉했을 
            SetCountText();
        }
    }

}