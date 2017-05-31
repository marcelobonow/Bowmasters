using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {
    
    public static void Shot(float _shotPower, float _angle, GameObject _arrow)
    {
        Rigidbody2D arrowRigidBody = _arrow.GetComponent<Rigidbody2D>();
        arrowRigidBody.velocity= new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad) * _shotPower, Mathf.Sin(_angle * Mathf.Deg2Rad) * _shotPower);
        arrowRigidBody.simulated = true;
    }
}
