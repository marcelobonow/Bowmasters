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
        if(GameManager.enemyCanShot)
        {
            GameManager.enemyCanShot = false;
            StartCoroutine(Aim(Player.transform.position,20));
            StartCoroutine(BowDrawAnimation());

        }
    }
    IEnumerator Aim(Vector3 playerPosition, float TotalVelocity, float precison = 0.01f)
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
                Debug.Log("Angle: " + angle*Mathf.Rad2Deg);
                break;
            }
            if(i % 15 == 0)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        ShootingBehaviour.Shot(TotalVelocity, (180 - angle*Mathf.Rad2Deg), GameManager.arrow);
        gameManager.SetStage(GameManager.Stage.EnemyShot);
    }
    IEnumerator BowDrawAnimation() //Poderia ser feito numa animação (usando o animator) porém como ja tenho tudo pronto
                                   //por causa do player, decidi fazer por código
    {
        yield return new WaitForSeconds(0.5f);
    }
}
