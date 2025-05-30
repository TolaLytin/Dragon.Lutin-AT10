using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int scoreValue = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
            ScoreManager.Instance.EnemyEliminated();
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}