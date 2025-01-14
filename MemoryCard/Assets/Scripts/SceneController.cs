using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;  // список значений указывающих кол-во ячеек и расстояние между ними
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private MemoryCard firstRevealed; 
    private MemoryCard secondRevealed;

    private int score = 0;

    
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] MemoryCard originalCard; //ссылка для карты на сцене
    [SerializeField] Sprite[] images; // массив для ссылок на ресурсы-спрайты

    public bool canReveal
    {
        get {return secondRevealed == null;} //возвращаем False если вторая карта уже открыта
    }
    public void CardRevealed(MemoryCard card) //сохраняем карты в одну из переменных в зависимости от того, какая из них свободна.
    {
        if(firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch()); //вызываем функция открытия 2 карт
        }
    }
    private void Start()
    {   
        Vector3 startPos = originalCard.transform.position; // положение первой карты, остальные будут отчитываться от нее

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3}; // создаем целочисленный массив с парами идентификаторов для 4 спрайтов(карт)
        numbers = ShuffleArray(numbers); // функция перемешивани массива

        for (int i = 0; i < gridCols; i++) //цикл указывающий колонки сетки
        {
            for(int j = 0; j < gridRows; j++)//цикл указывающий строки сетки
            {
                MemoryCard card; // ссылка на контейнер для исходной карты или копии
                if(i == 0 && j == 0) // выбираем исходную карту для первой ячейки или клонируем его для остальных сеток
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]); //вызов открытого метода, добавленного в сценарий MemoryCard

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        } 

    }

    private int[] ShuffleArray(int[] numbers) //реализация алгоритма тусования Кнута(алгоритм Фишера - Йетса)
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
        if(firstRevealed.Id == secondRevealed.Id) // если равны
        {
            score++; // прибавляем очко
           scoreLabel.text = $"Score: {score}"; // вывод очков
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            firstRevealed.Unreveal(); //закрываем не совпадающие карты
            secondRevealed.Unreveal();
        }
        firstRevealed = null; // очищаем переменные вне зависимости были ли совпадения или нет
        secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
