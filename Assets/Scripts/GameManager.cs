/* O Gamemanager funciona em estágios, o jogo possui o estagio de player mirando, player atirando, inimigo mirando,
 * inimigo atirando, e volta para player mirando. Esta ordem é fixa como numa lista encadeada. A mudança de PlayerAim
 * para PlayerShot se da quando o player atira, ou seja, o playershot acontece após o tiro,
 * e dura até que a flecha colida com algo. Nesse momento o estágio do jogo passa para EnemyAim, quando a inteligencia
 * artificial terminar de calcular e realizar a animação, o inimigo irá atirar, passando o estágio do jogo para EnemyShot
 * e, quando esta flecha acertar algo, voltamos para o playerAim
 */
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Stage
    {
        PlayerAim, playershot, EnemyAim, EnemyShot
    }
    public static GameObject arrow;
    public static float angle;
    public static float shotPower;
    public static bool cameraInPosition; //É colocada como verdadeira quando a camera esta em posição e quando o player estiver setado
    public static bool enemyCanShot;
    [SerializeField]
    private GameObject playerArrow;   //Pode ser um vetor de flechas diferentes, com danos diferentes, por exemplo
    [SerializeField]
    private GameObject enemyArrow;
    [SerializeField]
    private GameManager gameManager; //pode deixar de ser serializado se no start localizar o GameManager
    [SerializeField]
    private BowBehaviour bowBehaviour; //Muda entre o bow do player e do inimigo
    [SerializeField]
    private IABehaviour iaBehaviour;
    [SerializeField]
    private UIManager uiManager; //Objeto do UIManager para colocar tela de vitoria ou tela de derrota
    private static Stage stage;


    private void Awake()
    {
        stage = Stage.PlayerAim;//Inicia do estagio do player mirando
        SetPlayer();
    }

    private void FixedUpdate()
    {
        //todo fixed update muda o sprite do arco de acordo com a força, e rotaciona o arco de acordo com o angulo da mira atual
        if(InputManager.hasSnap && stage == Stage.PlayerAim)
        {
            bowBehaviour.SetBowRotation(angle);
            if(shotPower < 1)
                bowBehaviour.SetBowPosition(0);
            else if(shotPower >= 1 && shotPower < 5)
                bowBehaviour.SetBowPosition(1);
            else if(shotPower >= 5 && shotPower < 10)
                bowBehaviour.SetBowPosition(2);
            else if(shotPower >= 10 && shotPower < 15)
                bowBehaviour.SetBowPosition(3);
            else
                bowBehaviour.SetBowPosition(4);
        }
    }
    //Coloca o player na posição de tiro
    public void SetPlayer()
    {
        arrow = playerArrow;
        cameraInPosition = false;
        enemyCanShot = false;
        arrow = Instantiate(arrow);
        arrow.AddComponent<Arrow>().damage = 5;
        GameObject playerBow = GameObject.Find("PlayerBow");
        //Reseta a rotação do arco, se não ele vai ter a rotação de quando atirou
        playerBow.transform.eulerAngles = new Vector3(0, 0, 0);
        arrow.transform.SetParent(playerBow.transform);
        //a flecha precisa do gameManager para mudar o estagio e resetar para posição de tiro o player e o inimigo
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        arrow.GetComponent<ArrowBehaviour>().enabled = true;
        playerBow.GetComponent<BowBehaviour>().SetBowPosition(0);
        InputManager.hasSnap = false;
    }
    //Coloca o inimigo na posição de tiro
    public void SetEnemy()
    {
        arrow = enemyArrow;
        arrow = Instantiate(arrow);
        arrow.AddComponent<Arrow>().damage = 5;
        GameObject enemyBow = GameObject.Find("EnemyBow");
        enemyBow.transform.eulerAngles = new Vector3(0, 0, 180);
        arrow.transform.SetParent(enemyBow.transform);
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        arrow.GetComponent<ArrowBehaviour>().enabled = true;
        enemyBow.GetComponent<BowBehaviour>().SetBowPosition(0);
        InputManager.hasSnap = false;
    }

    public void Die(string looser)
    {
        if(looser == "Player")
        {
            SceneManager.LoadScene(1);
            //Aqui aconteceria uma animação do player morrendo
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
    /*Metodos Set e Get foram usados principalmente para Debugar onde havia maior probabilidade de erro
    O correto seria todos os metodos privados com um Setter e getter, porém, como algumas variaveis são staticas
    e alguns metodos também, seria fácil de perder a organização lógica*/
    public static void SetAngle(float _angle)
    {
        angle = _angle;
    }
    public static void SetStage(Stage _stage)
    {
        stage = _stage;
    }
    public static Stage GetStage()
    {
        return stage;
    }
}
