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
        if (GameManager.inAir)
        {
            angle = rigidBody.velocity.y == 0 ? //Há erro se tentar dividir 0 pela velocidade, o valor sai errado.
                angle = 0:Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x) * Mathf.Rad2Deg; //Dividindo a velocidade y pela velocidade x, se da a tangente.A atangente retorna em radianos, que é convertido para deg
            transform.eulerAngles = new Vector3(0, 0, angle-90); //roda o objeto para apontar na direção da velocidade, como em 0 graus a flecha aponta para cima, é necessario -90 para que ela esteja na horizontal.
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position += new Vector3(0.1f, -0.1f);
            rigidBody.velocity = new Vector2(0, 0);
            GameManager.SetinAir(false);
            rigidBody.simulated = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (collision.transform.CompareTag("Player"))
        {
            /* não foi possivel manter tanto o alvo quando a flecha sem trigger e fazer a flecha penetrar
             * no objeto que acertar */
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position += new Vector3(0.1f, -0.1f);
            rigidBody.velocity = new Vector2(0, 0);
            GameManager.SetinAir(false);
            rigidBody.simulated = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //restart();
        GameManager.SetCameraAnimatorState(-1);
    }
}
