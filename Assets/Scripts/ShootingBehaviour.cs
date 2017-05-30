//esse script vai no próprio arco.
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {

    private const float OFFSET = -90;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private float angle;

    [SerializeField]
    private float shootPower = 100;
    [SerializeField]
    private Sprite[] bowPositions;
    [SerializeField]
    private float[] arrowPositions;
    

    private void Start()
    {
        if (arrow == null)
            arrow = gameObject.transform.FindChild("Arrow").gameObject;
    }
    public void OnPowerChange(float _power)
    {
        shootPower = _power;
    }

    public void Shoot(float _angle, GameObject _arrow)
    {
        Rigidbody2D arrowRigidBody = arrow.GetComponent<Rigidbody2D>();

        if (arrowRigidBody == null)
        {
            Debug.Log("Faltando Rigidbody na flecha");
        }
        else
            Debug.Log("Remover esta linha");
        arrowRigidBody.AddForce(new Vector2(-Mathf.Cos(angle) * shootPower,
            -Mathf.Sin(angle) * shootPower));
        arrowRigidBody.simulated = true;
        _arrow.transform.Rotate(0, 0, angle - OFFSET);
    }
}
