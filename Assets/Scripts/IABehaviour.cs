/*Este script faz os calculos e o comportamento da inteligencia artificial
 *o correto seria dividir este script em um script de comportamento e um script de calculo (static)
 *porém, como o código do calculo é pequeno e o código do comportamento também, preferi por fazer em um único script
 *se este receber continuidade é interessante que isto seja feito 
 */
using System.Collections;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject Player; //Player, para saber onde deve mirar
    private BowBehaviour bow;
    [Header("IA Configuration")]
    [SerializeField][Range(0,0.2f)]
    private float erro = 0.1f; //Porcentagem de erro da IA, neste caso o padrão é 10%

    private void FixedUpdate()
    {
        if(GameManager.enemyCanShot == true && GameManager.cameraInPosition)
        {
            bow = GameObject.Find("EnemyBow").GetComponent<BowBehaviour>();
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
        //Quando a animação for executada a inteligencia artificial ja vai ter completado os calculos
        //Esta animação poderia ser feita pelo animator, mas como ja possuia um script que fazia a troca de sprites
        //(BowBehaviour), preferi aproveita-lo e fazer por código
        while(animationTimer < 1)
        {
            animationTimer += 0.03f;
            //Camera se afasta, da mesma forma de como quando o player atira, dando a impressão de que o inimigo esta aumentando a força
            Camera.main.orthographicSize = Mathf.Lerp(3, totalVelocity / 8f + 3, animationTimer);
            //Arco rotaciona, dando a impressão de que a ia esta mirando
            bow.SetBowRotation(Mathf.Lerp(Mathf.PI, Mathf.PI - (angle), animationTimer));
            //Muda o sprite do arco, incrementalmente na direção do sprite com a corda mais puxada
            bow.SetBowPosition(Mathf.FloorToInt(4 * animationTimer + 1));
            yield return new WaitForSeconds(0.03f);
        }
        //Adiciona erro tanto na velocidade do tiro quando no angulo de disparo.
        ShootingBehaviour.Shot(totalVelocity + totalVelocity*Random.Range(-erro,erro), (Mathf.PI - angle) + (Mathf.PI-angle)*Random.Range(-erro,erro), GameManager.arrow);
        //Atira e muda o estágio do jogo para EnemyShot
        GameManager.cameraInPosition = false;
        GameManager.SetStage(GameManager.Stage.EnemyShot);
    }
}
