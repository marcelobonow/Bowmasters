using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject Player;
    private BowBehaviour bow;
    private bool hasFinished;

    private void FixedUpdate()
    {
        if(GameManager.enemyCanShot == true)
        {
            bow = GameObject.Find("EnemyBow").GetComponent<BowBehaviour>();
            hasFinished = false;
            GameManager.enemyCanShot = false;
            StartCoroutine(BowDrawAnimation());
            StartCoroutine(Aim(Player.transform.position));

        }
    }
    IEnumerator BowDrawAnimation() //Poderia ser feito numa animação (usando o animator) porém como ja tenho tudo pronto
                                   //por causa do player, decidi fazer por código
    {
        for (int i = 0; i < 5; i++)
        {
            bow.SetBowPosition(i);
            yield return new WaitForSeconds(0.5f);
        }
        hasFinished = true;
    }
    IEnumerator Aim(Vector3 playerPosition, float precison = 0.002f)
    {
        float distance = Vector3.Distance(gameObject.transform.position,playerPosition);
        float angle = 0;
        float time = 0;
        float horizontalVelocity=0;
        int totalVelocity;
        for (totalVelocity = 5; totalVelocity < 20; totalVelocity++)
        {
            angle = 0;
            for (angle = 0; angle < 0.8;)//adicionar raycast
            {
                angle += precison;
                horizontalVelocity = totalVelocity * Mathf.Cos(angle);
                float verticalVelocity = totalVelocity * Mathf.Sin(angle);
                time = -(verticalVelocity * 2 / Physics.gravity.y); //Gravidade no unity no eixo y = -9.8
                if (time > (distance / horizontalVelocity))
                {
                    //Debug.Log("time: " + time);
                    //Debug.Log("Vertical Velocity: " + verticalVelocity);
                    Debug.Log("Distance: " + distance);
                    Debug.Log("Angle: " + angle);
                    break;
                }
            }
            if (Mathf.Round(totalVelocity) % 2 == 0)
            {
                bow.SetBowRotation(90+(angle * Mathf.Rad2Deg));
                yield return new WaitForSeconds(0.2f);
            }
            if (time > (distance / horizontalVelocity))
            {
                break;
            }
        }
        while (!hasFinished)
            yield return new WaitForSeconds(1f);
        Debug.Log(totalVelocity);
        ShootingBehaviour.Shot(totalVelocity, (180 - angle * Mathf.Rad2Deg), GameManager.arrow);
        gameManager.SetStage(GameManager.Stage.EnemyShot);
    }
}
