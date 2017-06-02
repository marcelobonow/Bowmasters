//Script responsavel pelo controle da janela que aparece quando o player mirar, que lhe informa o angulo e a força
using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Text angleText; //Text onde irá o valor atual do angulo
    [SerializeField]
    private Text powerText; //Text onde irá o valor atual da força
    [SerializeField]
    private GameObject popUp; //Prefab do popUp

    private void Start()
    {
        popUp.SetActive(false);
    }
    public void CreatePopUp(Vector2 position)
    {
        RectTransform rectTransform = popUp.GetComponent<RectTransform>();
        //Por padrão o valor é setado ao valor passado
        rectTransform.position = position;
        //Esse bloco de if/else if verifica se algum valor esta fora da tela, se estiver,
        //ele o corrige para dentro da tela
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
            rectTransform.position = new Vector2(rectTransform.position.x,Screen.height - (rectTransform.rect.height / 2 + 3));
        }
        else if ((position.y - rectTransform.rect.height / 2) < 0)
        {
            rectTransform.position = new Vector2(rectTransform.position.x,rectTransform.rect.height / 2 + 3);
        }
    }
    //Mostra no PopUp os valores do angulo e da força
    public void UpdatePopUp(float _angle, float _power)
    {
        angleText.text = Mathf.Round((_angle * Mathf.Rad2Deg)).ToString() + "°";
        powerText.text = _power.ToString();
    }
    //PopUp visivel
    public void EnablePopUp()
    {
        popUp.SetActive(true);
    }
    //Pop não visivel
    public void DisablePopUp()
    {
        popUp.SetActive(false);
    }
}
