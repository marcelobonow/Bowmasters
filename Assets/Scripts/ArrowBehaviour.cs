using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

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
                angle = 0:Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x) * Mathf.Rad2Deg; //Dividindo a velocidade y pela velocidade x, se da a tangente.A atangente retorna em radianos, que é convertido para deg
            transform.eulerAngles = new Vector3(0, 0, angle-90); //roda o objeto para apontar na direção da velocidade, como em 0 graus a flecha aponta para cima, é necessario -90 para que ela esteja na horizontal.
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        transform.position += new Vector3(0.1f, -0.1f); //Enterra a flecha no alvo
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.simulated = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.SetParent(collision.transform);
        //restart();
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0));
            gameManager.SetStage(GameManager.staticStage == GameManager.Stage.playershot ? GameManager.Stage.Enemy : GameManager.Stage.Player);
        }
        if(collision.CompareTag("Ground"))
        {
            gameManager.SetStage(GameManager.staticStage == GameManager.Stage.playershot ? GameManager.Stage.Enemy : GameManager.Stage.Player); 
        }
    }
}
