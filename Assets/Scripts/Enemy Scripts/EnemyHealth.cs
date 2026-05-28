using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    private bool isDead = false;

    public Animator animator;

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;

        Debug.Log(gameObject.name + " Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        GameManager gm =
            FindObjectOfType<GameManager>();

        if (gm != null)
        {
            gm.AddKill(gameObject.tag);
        }

        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        Destroy(gameObject, 3f);
    }
}