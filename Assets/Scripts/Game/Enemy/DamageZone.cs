using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Header("Настройки урона")]
    public int damageAmount = 33;
    public PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(damageAmount);

            if (ScoreManager.Instance != null)
            {
                Debug.Log("Враг прошёл через зону, считаем уничтоженным");
                ScoreManager.Instance.EnemyEliminated();
            }

            Destroy(other.gameObject);
        }
    }
}
