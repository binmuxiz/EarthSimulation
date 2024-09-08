using UnityEngine;

public class Rotate : MonoBehaviour
{
    float fh1 = 100.0f; // 회전 속도
    Vector3 previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭 중일 때
        {
            Vector3 mouseDelta = Input.mousePosition - previousMousePosition;

            // X축(수평) 회전 - 마우스 Y축 움직임을 기준으로 회전
            transform.Rotate(Vector3.up, -mouseDelta.x * fh1 * Time.deltaTime, Space.World);

            // Y축(수직) 회전 - 마우스 X축 움직임을 기준으로 회전
            transform.Rotate(Vector3.right, mouseDelta.y * fh1 * Time.deltaTime, Space.World);
        }

        // 현재 마우스 위치를 저장해서 다음 프레임에 사용할 수 있게 함
        previousMousePosition = Input.mousePosition;

    }
    
    
}
