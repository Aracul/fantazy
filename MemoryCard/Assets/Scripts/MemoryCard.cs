using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack; //Запускаем код деактивации только в случае, когда объект активен(видим)
    [SerializeField] SceneController controller;

    private int _id;
    public int Id // функция чтения идиома как С#
    {
        get {return _id;} 
    }
    public void SetCard(int Id, Sprite images) //Открытый метод, для передачи указанному объекту новые спрайты
    {
        _id = Id;
        GetComponent<SpriteRenderer>().sprite = images; //Сопостовляем спрайт компоненту
    }
 
    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal) // переменная которая появляется на панели Inspector && проверяем свойство контроллера canReveal, гарантирующая что одновременно можно открыть только 2 карты
        {
            cardBack.SetActive(false); // делаем не активным(не видимым)
            controller.CardRevealed(this); //уведомляем об открытии карты
        }
    }
    public void Unreveal() //открытый метод, позволяющий снова открыть карту, вернув на место спрайт обложки
    {
        cardBack.SetActive(true);
    }
}   
