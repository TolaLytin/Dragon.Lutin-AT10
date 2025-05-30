using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Настройки урона")]
    [SerializeField] private int damage = 3;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); 
            }
            Destroy(gameObject); 
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}