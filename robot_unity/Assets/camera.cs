using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // PlayerのTransformを指定
    public float smoothSpeed = 0.125f; // カメラの追従速度
    public Vector3 offset; // カメラのオフセット

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
