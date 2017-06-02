/*A principal função deste script é fazer com que quando a flecha esteja no ar sempre aponte para a direção de movimento
 * e que quando colidir com algo mude o estágio do jogo
 */
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField]
    private float angle;
    private Rigidbody2D rigidBody;
    public GameManager gameManager; //Objeto do GameManager para setar e resetar o player e o inimigo na posição de aim após a flecha acertar algo

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate () {
        if (GameManager.GetStage()== GameManager.Stage.playershot)
        {
            angle = rigidBody.velocity.y == 0 ? //Há erro se tentar dividir 0 pela velocidade, o valor sai errado.
                angle = 0:Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x); //Dividindo a velocidade y pela velocidade x, se da a tangente.A atangente retorna em radianos, que é convertido para deg
            transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90); //roda o objeto para apontar na direção da velocidade, como em 0 graus a flecha aponta para cima, é necessario -90 para que ela esteja na horizontal.
        }
        if(GameManager.GetStage() == GameManager.Stage.EnemyShot)
        {
            angle = rigidBody.velocity.y == 0 ?
                            angle = 0 : Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x); 
            transform.eulerAngles = new Vector3(0, 0, angle*Mathf.Rad2Deg + 90); //+90 graus pois ele esta indo da direita para esquerda
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Se o collision for o player ou o inimigo, estes tomam dano e sofrem stagger;
        if (collision.CompareTag("Player")||collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(rigidBody.velocity.x/30,0);
            collision.GetComponent<Player>().TakeDamage(gameObject.GetComponent<Arrow>().damage);
        }
        transform.position += new Vector3(0.1f, -0.1f); //Enterra a flecha no alvo
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.simulated = false;
        gameObject.transform.SetParent(collision.transform);
        if (GameManager.GetStage() == GameManager.Stage.playershot)
        {
            GameManager.SetStage(GameManager.Stage.EnemyAim);
            gameManager.SetEnemy(); //Coloca o inimigo na posição de mira e o permite atirar
            GameManager.enemyCanShot = true;
        }
        if (GameManager.GetStage() == GameManager.Stage.EnemyShot)
        {
            GameManager.SetStage(GameManager.Stage.PlayerAim);
            gameManager.SetPlayer(); //Cooca o player na posição de mira
        }
        RemoveComponents(); //Deixa apenas o transform e o sprite na flecha
        
    }
    private void RemoveComponents()//Há a possibilidade de passar um vetor de componentes para serem desabilitados
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<Collider2D>());
        //Adicionar aqui mais algum componente que a flecha venha a ter
        Destroy(this);
    }
}
