using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject Player;

    private void FixedUpdate()
    {
        if(GameManager.enemyCanShoot && gameManager.GetStage() == GameManager.Stage.Enemy)
        {
            //Debug.Log("Entrou");
            GameManager.enemyCanShoot = false;
            StartCoroutine(Aim(Player.transform.position,20));
            //ShootingBehaviour.Shot(shotpower,);
            //gameManager.SetStage(GameManager.Stage.Player);
        }
    }
    IEnumerator Aim(Vector3 playerPosition, float TotalVelocity, float precison = 0.1f)
    {
        float i = 0;
        float distance = Vector3.Distance(gameObject.transform.position,playerPosition);
        float angle = 0;
        float horizontalVelocity;
        float verticalVelocity;
        float time;
        for (; i < 90;)
        {
            i += precison;
            angle = i;
            horizontalVelocity = TotalVelocity * Mathf.Cos(angle);
            verticalVelocity = TotalVelocity * Mathf.Sin(angle);
            time = -(verticalVelocity * 2 / Physics.gravity.y);
            if (time > (distance/horizontalVelocity))
            {
                //Debug.Log("time: " + time);
                //Debug.Log("Vertical Velocity: " + verticalVelocity);
                //Debug.Log("Distance: " + distance);
                //Debug.Log("Angle: " + angle);
                break;
            }
            yield return new WaitForSeconds(0.02f);
        }
        //ShootingBehaviour.Shot(TotalVelocity, angle, GameManager.arrow);
    }
}
