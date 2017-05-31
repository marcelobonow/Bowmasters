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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log(collision.transform.tag);
            GameManager.SetinAir(false);
        }
        if (collision.transform.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position += new Vector3(rigidBody.velocity.x,transform.position.y)*0.04f;
            rigidBody.velocity = new Vector2(0, 0);
            GameManager.SetinAir(false);
            rigidBody.simulated = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
