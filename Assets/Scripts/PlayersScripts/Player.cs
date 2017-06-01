using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {
    public Arrow arrow; //scripts que descendem deste terão um script que descende de arrow
                        //ou seja, cada personagem tem suas caracteristicas e uma flecha com suas caracteristicas
    public string name = "Player";
    private float maxHealth = 15f;
    private float health = 15f;
    [SerializeField]
    private Image healthImage;      //Imagem que se refere a sua vida

    public void TakeDamage(float _damage)
    {
        health -= _damage;//delta sizebar
        if (health <= 0)
        {
            health = 0;
            GameManager.Die(gameObject.name);
        }
        healthImage.fillAmount = health / maxHealth; 
    }
}
