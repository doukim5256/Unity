using UnityEngine;

public class CarCameraFollow : MonoBehaviour
{
    public Transform target;  // 자동차(CarBody) 지정
    public Vector3 offset = new Vector3(0, 5, -7); // 자동차 기준 오프셋 위치
    public float smoothSpeed = 5f; // 이동 속도 조절
    public float rotationSmoothSpeed = 5f; // 회전 속도 조절

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 설정 (자동차 위치 + 오프셋)
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        
        // 부드러운 이동 (Lerp)        Mathf.Lerp() (선형 보간, MoveTowards()와 비슷하지만 감속이 있음)  
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // 부드러운 회전 (Slerp)
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
