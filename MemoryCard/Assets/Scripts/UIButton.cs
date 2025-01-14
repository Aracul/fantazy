using UnityEngine;

public class UIButton : MonoBehaviour
{

    [SerializeField] GameObject targetObject; //ссылка на объект для информирования о щелчках
    [SerializeField] string targetMessage;
    public Color highlightColor = Color.cyan;
    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor; //меняем цвет кнопки при наведении указателя мыши
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = Color.white;
        }

    }
    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); // в момент щелчка размер кнопки слегка увеличивается
    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage); // после щелчка отправялем сообщение по целевому объекту
        }

    }
}
