using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

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
    public void UpdatePopUp(Vector2 position, float _angle,float _power)
    {
        popUp.GetComponent<RectTransform>().position = position;
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
