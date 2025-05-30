using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("Ссылки")]
    public Slider healthSlider;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        
        UpdateHealthBar();
    }
    
    void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    void Die()
    {
        if(ScoreManager.Instance != null)
        {
            ScoreManager.Instance.GameOver();
        }
        
        Debug.Log("Игрок умер!");
    }
}