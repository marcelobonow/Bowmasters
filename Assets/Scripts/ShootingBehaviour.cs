﻿using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {
    
    public static void Shot(float _shotPower, float _angle, GameObject _arrow)
    {
        Rigidbody2D arrowRigidBody = _arrow.GetComponent<Rigidbody2D>();
        //Adiciona velocidade na flecha que foi passada de acordo com a força e o angulo passados
        arrowRigidBody.velocity= new Vector2(Mathf.Cos(_angle) * _shotPower, Mathf.Sin(_angle) * _shotPower);
        arrowRigidBody.simulated = true;
    }
}
