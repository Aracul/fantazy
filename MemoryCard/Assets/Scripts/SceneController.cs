using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;  // ������ �������� ����������� ���-�� ����� � ���������� ����� ����
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private MemoryCard firstRevealed; 
    private MemoryCard secondRevealed;

    private int score = 0;

    
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] MemoryCard originalCard; //������ ��� ����� �� �����
    [SerializeField] Sprite[] images; // ������ ��� ������ �� �������-�������

    public bool canReveal
    {
        get {return secondRevealed == null;} //���������� False ���� ������ ����� ��� �������
    }
    public void CardRevealed(MemoryCard card) //��������� ����� � ���� �� ���������� � ����������� �� ����, ����� �� ��� ��������.
    {
        if(firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch()); //�������� ������� �������� 2 ����
        }
    }
    private void Start()
    {   
        Vector3 startPos = originalCard.transform.position; // ��������� ������ �����, ��������� ����� ������������ �� ���

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3}; // ������� ������������� ������ � ������ ��������������� ��� 4 ��������(����)
        numbers = ShuffleArray(numbers); // ������� ������������ �������

        for (int i = 0; i < gridCols; i++) //���� ����������� ������� �����
        {
            for(int j = 0; j < gridRows; j++)//���� ����������� ������ �����
            {
                MemoryCard card; // ������ �� ��������� ��� �������� ����� ��� �����
                if(i == 0 && j == 0) // �������� �������� ����� ��� ������ ������ ��� ��������� ��� ��� ��������� �����
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]); //����� ��������� ������, ������������ � �������� MemoryCard

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        } 

    }

    private int[] ShuffleArray(int[] numbers) //���������� ��������� ��������� �����(�������� ������ - �����)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, numbers.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;

    }

    private IEnumerator CheckMatch()
    {
        if(firstRevealed.Id == secondRevealed.Id) // ���� �����
        {
            score++; // ���������� ����
           scoreLabel.text = $"Score: {score}"; // ����� �����
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            firstRevealed.Unreveal(); //��������� �� ����������� �����
            secondRevealed.Unreveal();
        }
        firstRevealed = null; // ������� ���������� ��� ����������� ���� �� ���������� ��� ���
        secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
