using UnityEngine;

public class Player : MonoBehaviour {
    public Arrow arrow;
    private float health = 15f;
	//healthbar

    public void TakeDamage(float _damage)
    {
        health -= _damage;//delta sizebar
        if (health <= 0)
        {
            health = 0;
            GameManager.Die(gameObject.name);
        }
    }
}
