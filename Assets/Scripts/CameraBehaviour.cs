/*o Objetivo deste script é controlar a camera, para que ela siga a flecha enquanto estiver em algum dos estágios de shot
 *e para que, quando saia deste estágio, ela de um lerp até a posição dos personagens
 */
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject arrow; //referencia a arrow que o GameManager possui
    [SerializeField] private Transform startPointPlayer;//Posição do personagem do player
    [SerializeField] private Transform startPointEnemy;//Posição do personagem do inimigo
    [SerializeField] private float moveTimer; //Timer usado para fazer o lerp

    private bool inPosition;
    private const float ZCAMERA = -10;

    private void Start()
    {
        gameObject.transform.position = new Vector3(startPointPlayer.position.x, startPointPlayer.position.y, ZCAMERA);
    }
    void Update()
    {
        arrow = GameManager.arrow;
        if(GameManager.GetStage() == GameManager.Stage.playershot || GameManager.GetStage() == GameManager.Stage.EnemyShot)
        {
            //Se estiver em um dos turnos de tiro, a camera seguirá a flecha disparada
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
            inPosition = false;
        }

        //se esta em um dos turnos de mirar do player ou do inimigo a camera vai para as posições pre-definidas
        if(GameManager.GetStage() == GameManager.Stage.PlayerAim && !inPosition)
            SmoothFollow(startPointPlayer.transform.position);
        if(GameManager.GetStage() == GameManager.Stage.EnemyAim && !inPosition)
            SmoothFollow(startPointEnemy.transform.position);
    }
    void SmoothFollow(Vector2 position)
    {
        moveTimer += Time.deltaTime;
        //Quando esta função for chamada é porque esta indo em direção ao player, então, diminuirá seu tamanho para que de a sensação
        //de que se aproximou do player no eixo z
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, moveTimer / 20);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, moveTimer / 60),
                Mathf.Lerp(transform.position.y, position.y + 1, moveTimer / 60), ZCAMERA);//offset no eixo y da camera
        if(moveTimer > 4) //Limite do timer
        {
            moveTimer = 0;
            GameManager.cameraInPosition = true;
            inPosition = true;
        }
    }
}
