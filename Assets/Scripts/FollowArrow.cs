using UnityEngine;

public class FollowArrow : MonoBehaviour {

    private const float ZCAMERA = -10;

    public static bool inPosition;

    [SerializeField]
    private GameObject arrow; //referencia a arrow que o GameManager possui
    [SerializeField]
    private Transform startPointPlayer;
    [SerializeField]
    private Transform startPointEnemy;
    private float moveTimer;

    private void Start()
    {
        gameObject.transform.position = new Vector3(startPointPlayer.position.x,startPointPlayer.position.y,ZCAMERA);
    }
    void FixedUpdate ()
    {
        arrow = GameManager.arrow;
        if (GameManager.staticStage == GameManager.Stage.playershot || GameManager.staticStage == GameManager.Stage.EnemyShot)
        {
            //Se estiver em um dos turnos de tiro, a camera seguirá a flecha disparada
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
            inPosition = false;
        }
        //se esta em um dos turnos de mirar do player ou do inimigo a camera vai para as posições pre-definidas
        else if(GameManager.staticStage == GameManager.Stage.Player && !inPosition)
        {
            moveTimer += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, startPointPlayer.transform.position.x, moveTimer/3),
                Mathf.Lerp(transform.position.y, startPointPlayer.transform.position.y, moveTimer/3), ZCAMERA);
            inPosition = moveTimer > 1;//mostrar o inimigo e depois voltar para o player
        }
        else if(GameManager.staticStage == GameManager.Stage.Enemy && !inPosition)
        {
            moveTimer += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, startPointEnemy.transform.position.x, moveTimer/3),
                Mathf.Lerp(transform.position.y, startPointEnemy.transform.position.y, moveTimer/3), ZCAMERA);
            inPosition = moveTimer > 1;
        }
        if (inPosition)
        {
            moveTimer = 0;
        }
	}
}
