/* há no script a variavel nome que não é utilizado, porém, se houver mais personagens, pode-se mostrar o nome do personagem
 * junto com a vida do player no HUD
 */
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public string name = "Player";
    private float maxHealth = 15f;
    private float health = 15f;
    //Imagem que se refere a sua vida, isso permite que a barra de vida estejam diretamente correlacionados, e que certos
    //personagens tenham barras de vida diferentes
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private GameManager gameManager;
    

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            health = 0;
            gameManager.Die(gameObject.name);
        }
        healthImage.fillAmount = health / maxHealth;//Coloca o fill amount a mesma porcentagem de vida atual 
    }
}
