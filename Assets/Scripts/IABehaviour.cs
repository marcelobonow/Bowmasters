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
        if(GameManager.enemyCanShot == true && GameManager.cameraInPosition)
        {
            bow = GameObject.Find("EnemyBow").GetComponent<BowBehaviour>();
            hasFinished = false;
            GameManager.enemyCanShot = false;
            StartCoroutine(Aim(Player.transform.position));

        }
    }
    IEnumerator Aim(Vector3 playerPosition, float precison = 0.002f)
    {
        float distance = Vector3.Distance(gameObject.transform.position,playerPosition);
        float angle = 0;
        float time = 0;
        float horizontalVelocity=0;
        int totalVelocity;
        float animationTimer = 0;
        for (totalVelocity = 4; totalVelocity <= 20; totalVelocity++)
        {
            angle = 0;
            for (angle = 0; angle < 1;)//adicionar raycast
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
                bow.SetBowRotation(90+(angle * Mathf.Rad2Deg));
            if (time > (distance / horizontalVelocity))
            {
                break;
            }
        }
        while(animationTimer < 1)
        {
            animationTimer += 0.03f;
            Camera.main.orthographicSize = Mathf.Lerp(3, totalVelocity / 8f + 3, animationTimer);
            bow.SetBowRotation(Mathf.Lerp(180, 180 - (angle*Mathf.Rad2Deg), animationTimer));
            bow.SetBowPosition(Mathf.FloorToInt(4 * animationTimer + 1));
            yield return new WaitForSeconds(0.03f);
        }
        Debug.Log(totalVelocity);
        ShootingBehaviour.Shot(totalVelocity, (180 - angle * Mathf.Rad2Deg), GameManager.arrow);
        GameManager.cameraInPosition = false;
        gameManager.SetStage(GameManager.Stage.EnemyShot);
    }
}
