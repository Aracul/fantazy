using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f; //скорость
    [SerializeField] private float jumpForce = 10.0f;//высота прыжка
    private Rigidbody2D _rigibody;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>(); // Нужен, чтобы к объекту GameObject был прикреплен 2 компонет
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _rigibody.linearVelocityY); //задаем только горизонтальное движение, вертикальное сохраняем
        _rigibody.linearVelocity = movement;

        Vector3 max = _boxCollider.bounds.max;
        Vector3 min = _boxCollider.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f); //   Ниже проверяем значение минимальной Y координаты
        Vector2 corner2 = new Vector2(max.x, min.y - .2f); //   Ниже проверяем значение минимальной Y координаты
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;

        if (hit != null)
        {
            grounded = true;
        }
        _rigibody.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;//проверка одновременно нахождение на поверхности и бездействия персонажа
        if (grounded && Input.GetKeyDown(KeyCode.Space))   //сила прыжка добавляется только при нажатии пробел
        {
            _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //импульсно добавляем силу направленную вверх
        }
        MovingPlatform platform = null;
        if (hit != null) 
        {
            platform = hit.GetComponent<MovingPlatform>(); //проверяем может ли двигаться платформа находящая под персонажем
        }
        if (platform != null)  //выполняем связывание с платформой или очищаем переменную transform.parent
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        _animator.SetFloat("speed", Mathf.Abs(deltaX)); //Скорость больше нуля даже при отрицательных значениях Velocity

        Vector3 pScale = Vector3.one; //при нахождении вне движущейся платформы масштаб по умолчнию равен 1
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0)) //числа типа float не всегда полностью совпадают, поэтому их сравниваем методом Аpproximately
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1/pScale.y, 1); //В процессе движения масштабируем положительную или отрицательную 1 для поворота направо или налево
        }
    }
}
