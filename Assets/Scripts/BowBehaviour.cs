// O propósito deste script é de rotacionar o arco, e mostrar o sprite correlacionado com a força do tiro no momento

using UnityEngine;

public class BowBehaviour : MonoBehaviour {

    [SerializeField]
    private Sprite[] bowPositions; //Sprites para cada nível de força
    [SerializeField]
    private float[] arrowPositions; //Posições da flecha para cada posição da corda do arco
    [SerializeField]
    private float bowAngle;

    public void SetBowPosition(int _position)
    {

        if (_position >= 0 && _position < bowPositions.Length)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bowPositions[_position];
            GameManager.arrow.transform.localPosition = new Vector2(arrowPositions[_position],0f);
        }
    }
    public void SetBowRotation(float _angle)
    {
        transform.eulerAngles = new Vector3(0, 0, _angle*Mathf.Rad2Deg);
    }
}
