using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f; //��������
    [SerializeField] private float jumpForce = 10.0f;//������ ������
    private Rigidbody2D _rigibody;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>(); // �����, ����� � ������� GameObject ��� ���������� 2 ��������
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _rigibody.linearVelocityY); //������ ������ �������������� ��������, ������������ ���������
        _rigibody.linearVelocity = movement;

        Vector3 max = _boxCollider.bounds.max;
        Vector3 min = _boxCollider.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f); //   ���� ��������� �������� ����������� Y ����������
        Vector2 corner2 = new Vector2(max.x, min.y - .2f); //   ���� ��������� �������� ����������� Y ����������
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;

        if (hit != null)
        {
            grounded = true;
        }
        _rigibody.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;//�������� ������������ ���������� �� ����������� � ����������� ���������
        if (grounded && Input.GetKeyDown(KeyCode.Space))   //���� ������ ����������� ������ ��� ������� ������
        {
            _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //��������� ��������� ���� ������������ �����
        }
        MovingPlatform platform = null;
        if (hit != null) 
        {
            platform = hit.GetComponent<MovingPlatform>(); //��������� ����� �� ��������� ��������� ��������� ��� ����������
        }
        if (platform != null)  //��������� ���������� � ���������� ��� ������� ���������� transform.parent
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        _animator.SetFloat("speed", Mathf.Abs(deltaX)); //�������� ������ ���� ���� ��� ������������� ��������� Velocity

        Vector3 pScale = Vector3.one; //��� ���������� ��� ���������� ��������� ������� �� �������� ����� 1
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0)) //����� ���� float �� ������ ��������� ���������, ������� �� ���������� ������� �pproximately
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1/pScale.y, 1); //� �������� �������� ������������ ������������� ��� ������������� 1 ��� �������� ������� ��� ������
        }
    }
}
