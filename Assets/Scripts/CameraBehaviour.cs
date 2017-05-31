using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    private const float ZCAMERA = -10;

    public static bool inPosition;

    [SerializeField]
    private GameObject arrow; //referencia a arrow que o GameManager possui
    [SerializeField]
    private Transform startPointPlayer;
    [SerializeField]
    private Transform startPointEnemy;

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
            inPosition = true;
            StartCoroutine(SmoothFollow(startPointPlayer.transform.position));
        }
        else if(GameManager.staticStage == GameManager.Stage.Enemy && !inPosition)
        {
            inPosition = true;
            StartCoroutine(SmoothFollow(startPointEnemy.transform.position));
        }
	}
    IEnumerator SmoothFollow(Vector2 position)
    {
        float moveTimer = 0;
        while(moveTimer<1)
        {
            moveTimer += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, moveTimer / 10),
                Mathf.Lerp(transform.position.y, position.y, moveTimer / 10), ZCAMERA);
            yield return new WaitForEndOfFrame();
        }
        GameManager.enemyCanShoot = true;
        inPosition = true;
    }
}
