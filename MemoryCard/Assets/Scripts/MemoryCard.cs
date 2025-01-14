using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack; //��������� ��� ����������� ������ � ������, ����� ������ �������(�����)
    [SerializeField] SceneController controller;

    private int _id;
    public int Id // ������� ������ ������ ��� �#
    {
        get {return _id;} 
    }
    public void SetCard(int Id, Sprite images) //�������� �����, ��� �������� ���������� ������� ����� �������
    {
        _id = Id;
        GetComponent<SpriteRenderer>().sprite = images; //������������ ������ ����������
    }
 
    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal) // ���������� ������� ���������� �� ������ Inspector && ��������� �������� ����������� canReveal, ������������� ��� ������������ ����� ������� ������ 2 �����
        {
            cardBack.SetActive(false); // ������ �� ��������(�� �������)
            controller.CardRevealed(this); //���������� �� �������� �����
        }
    }
    public void Unreveal() //�������� �����, ����������� ����� ������� �����, ������ �� ����� ������ �������
    {
        cardBack.SetActive(true);
    }
}   
