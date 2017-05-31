using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField]
    private float angle;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        if (GameManager.actualStage == GameManager.Stages.playershot)
        {
            angle = rigidBody.velocity.y == 0 ? //Há erro se tentar dividir 0 pela velocidade, o valor sai errado.
                angle = 0:Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x) * Mathf.Rad2Deg; //Dividindo a velocidade y pela velocidade x, se da a tangente.A atangente retorna em radianos, que é convertido para deg
            transform.eulerAngles = new Vector3(0, 0, angle-90); //roda o objeto para apontar na direção da velocidade, como em 0 graus a flecha aponta para cima, é necessario -90 para que ela esteja na horizontal.
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position += new Vector3(0.1f, -0.1f); //Enterra a flecha no alvo
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.simulated = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0));
            GameManager.actualStage = GameManager.actualStage == GameManager.Stages.playershot ? GameManager.Stages.enemy : GameManager.Stages.Player;
        }
        if(collision.CompareTag("Ground"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position += new Vector3(0.1f, -0.1f); //Enterra a flecha no alvo
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.simulated = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.transform.SetParent(collision.transform);
            GameManager.actualStage = GameManager.actualStage == GameManager.Stages.playershot ? GameManager.Stages.enemy : GameManager.Stages.Player; 
        }
    }
}
