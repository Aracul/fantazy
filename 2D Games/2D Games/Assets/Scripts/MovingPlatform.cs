using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 finishPos = Vector3.zero; //целевое положение
    [SerializeField] private float speed = 0.5f;
    private Vector3 startPos;
    private float trackPercent = 0; //насколько далеко наше движение между старотом и финишем
    private int direction = 1; //направение движения в данный момент

    private void Start()
    {
        startPos = transform.position; //положение в сцене - это точка, от которой начинается движение 
    }
    private void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        if ((direction == 1 && trackPercent > .9f) ||
           (direction == -1 && trackPercent < .1f))    //меняем направление как в начале так и в конце
        {
            direction *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishPos);
    }
}
