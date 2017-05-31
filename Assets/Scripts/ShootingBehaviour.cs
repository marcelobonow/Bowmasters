//esse script vai no próprio arco.
//separar em um scrpit pro arco e outro para atirar
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {

    [SerializeField]
    private float shotPower = 500;
    
    public void Shot(float _shotPower, float _angle, GameObject _arrow)
    {
        Debug.Log(_shotPower);
        Rigidbody2D arrowRigidBody = _arrow.GetComponent<Rigidbody2D>();
        arrowRigidBody.AddForce(new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad) * _shotPower, Mathf.Sin(_angle * Mathf.Deg2Rad) * _shotPower));
        arrowRigidBody.simulated = true;
    }
   
}
