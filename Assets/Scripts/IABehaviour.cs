using System.Collections;
using UnityEngine;

public class IABehaviour : MonoBehaviour {
    [Header("IA Configuration")]

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject Player;
    private BowBehaviour bow;
    private bool hasFinished;
    [SerializeField][Range(0,0.2f)]
    private float erro = 0.1f; //Porcentagem de erro da IA, neste caso o padrão é 10%

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
            for (angle = 0; angle < 1;)
            {
                angle += precison;
                horizontalVelocity = totalVelocity * Mathf.Cos(angle);
                float verticalVelocity = totalVelocity * Mathf.Sin(angle);
                time = -(verticalVelocity * 2 / Physics.gravity.y); //Gravidade no unity no eixo y = -9.8
        if (time > (distance / horizontalVelocity))
                {
                    break;
                }
            }
                bow.SetBowRotation(Mathf.PI-angle);
            if (time > (distance / horizontalVelocity))
            {
                break;
            }
        }
        while(animationTimer < 1)
        {
            animationTimer += 0.03f;
            Camera.main.orthographicSize = Mathf.Lerp(3, totalVelocity / 8f + 3, animationTimer);
            bow.SetBowRotation(Mathf.Lerp(Mathf.PI, Mathf.PI - (angle), animationTimer));
            bow.SetBowPosition(Mathf.FloorToInt(4 * animationTimer + 1));
            yield return new WaitForSeconds(0.03f);
        }
        ShootingBehaviour.Shot(totalVelocity + totalVelocity*Random.Range(-erro,erro), (Mathf.PI - angle) + (Mathf.PI-angle)*Random.Range(-erro,erro), GameManager.arrow);
        GameManager.cameraInPosition = false;
        gameManager.SetStage(GameManager.Stage.EnemyShot);
    }
}
