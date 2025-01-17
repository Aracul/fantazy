using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); //сохраняем координату Z, меняем  значения X,Y
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
