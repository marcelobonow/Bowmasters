//esse script vai no próprio arco.
//separar em um scrpit pro arco e outro para atirar
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private float bowAngle;

    [SerializeField]
    private float shotPower = 1000;
    [SerializeField]
    private Sprite[] bowPositions;
    [SerializeField]
    private float[] arrowPositions;
    

    private void Start()
    {
        if (arrow == null)
            arrow = gameObject.transform.FindChild("Arrow").gameObject;
    }
    public void SetShotPower(float _power)
    {
        shotPower = _power;

    }
    public void Shot(float _shotPower, float _angle, GameObject _arrow)
    {
        GameManager.inAir = true;
        Rigidbody2D arrowRigidBody = arrow.GetComponent<Rigidbody2D>();
        arrowRigidBody.AddForce(new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad) * _shotPower, Mathf.Sin(_angle * Mathf.Deg2Rad) * _shotPower));
        arrowRigidBody.simulated = true;
        //_arrow.transform.Rotate(0, 0, _angle - OFFSET);
    }
    public void SetBow(int _position)
    {

        if (_position >= 0 && _position < bowPositions.Length)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bowPositions[_position];
        }
        else
            Debug.Log("Position invalid: " + _position);
    }
}
