using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    [SerializeField]
    private Text angleText;
    [SerializeField]
    private Text powerText;
    [SerializeField]
    private GameObject popUp;

    private void Start()
    {
        popUp.SetActive(false);
    }
    public void CreatePopUp(Vector2 position)
    {
        RectTransform rectTransform = popUp.GetComponent<RectTransform>();
        Debug.Log(position.y + rectTransform.rect.height);
        rectTransform.position = position;
        if ((position.x - rectTransform.rect.width / 2) < 0)
        {
            rectTransform.position = new Vector2(rectTransform.rect.width / 2 + 3, rectTransform.position.y);
        }
        else if ((position.x + rectTransform.rect.width / 2 + 3) > Screen.width)
        {
            rectTransform.position = new Vector2(Screen.width - (rectTransform.rect.width / 2 + 3), rectTransform.position.y);
        }
        if ((position.y + rectTransform.rect.height / 2 + 3) > Screen.height)
        {
            //Se o popup estiver muito em cima, o script o fará visivel
            rectTransform.position = new Vector2(rectTransform.position.x,Screen.height - (rectTransform.rect.height / 2 + 3));
        }
        else if ((position.y - rectTransform.rect.height / 2) < 0)
        {
            rectTransform.position = new Vector2(rectTransform.position.x,rectTransform.rect.height / 2 + 3);
        }
    }
    public void UpdatePopUp(float _angle, float _power)
    {
        angleText.text = Mathf.Round((_angle * Mathf.Rad2Deg)).ToString() + "°";
        powerText.text = _power.ToString();
    }
    public void EnablePopUp()
    {
        popUp.SetActive(true);
    }
    public void DisablePopUp()
    {
        popUp.SetActive(false);
    }
}
