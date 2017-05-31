//esse script vai no próprio arco.
//separar em um scrpit pro arco e outro para atirar
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {
    
    public static void Shot(float _shotPower, float _angle, GameObject _arrow)
    {
        Rigidbody2D arrowRigidBody = _arrow.GetComponent<Rigidbody2D>();
        arrowRigidBody.AddForce(new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad) * _shotPower, Mathf.Sin(_angle * Mathf.Deg2Rad) * _shotPower));
        arrowRigidBody.simulated = true;
    }
   
}
