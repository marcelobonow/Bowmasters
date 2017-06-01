using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField]
    private float angle;
    private Rigidbody2D rigidBody;
    public GameManager gameManager;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate () {
        if (GameManager.staticStage == GameManager.Stage.playershot)
        {
            angle = rigidBody.velocity.y == 0 ? //Há erro se tentar dividir 0 pela velocidade, o valor sai errado.
                angle = 0:Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x); //Dividindo a velocidade y pela velocidade x, se da a tangente.A atangente retorna em radianos, que é convertido para deg
            transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90); //roda o objeto para apontar na direção da velocidade, como em 0 graus a flecha aponta para cima, é necessario -90 para que ela esteja na horizontal.
        }
        if(GameManager.staticStage == GameManager.Stage.EnemyShot)
        {
            angle = rigidBody.velocity.y == 0 ?
                            angle = 0 : Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x); 
            transform.eulerAngles = new Vector3(0, 0, angle*Mathf.Rad2Deg + 90); //+90 graus pois ele esta indo da direita para esquerda
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")||collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(rigidBody.velocity.x/30,0);
            collision.GetComponent<Player>().TakeDamage(gameObject.GetComponent<Arrow>().damage);
        }
        transform.position += new Vector3(0.1f, -0.1f); //Enterra a flecha no alvo
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.simulated = false;
        gameObject.transform.SetParent(collision.transform);
        if (GameManager.staticStage == GameManager.Stage.playershot)
        {
            gameManager.SetStage(GameManager.Stage.Enemy);
            gameManager.SetEnemy();
            GameManager.enemyCanShot = true;
        }
        if (GameManager.staticStage == GameManager.Stage.EnemyShot)
        {
            gameManager.SetStage(GameManager.Stage.Player);
            gameManager.SetPlayer();
        }
        RemoveComponents();
        
    }
    private void RemoveComponents()
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(this);
    }
}
