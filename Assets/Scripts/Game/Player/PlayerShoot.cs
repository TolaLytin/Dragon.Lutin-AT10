using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShoot : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float reloadTime = 4f; // Время перезарядки в секундах
    
    [Header("UI Элементы")]
    [SerializeField] private Slider reloadSlider; // Ссылка на UI Slider
    
    private bool isReloading = false;
    private float reloadTimer;

    private void Start()
    {
        // Настройка слайдера
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = reloadTime;
            reloadSlider.value = 0;
            reloadSlider.gameObject.SetActive(false);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && !isReloading)
        {
            Shoot();
            StartReload();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Не назначен префаб пули или точка выстрела!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }
        
        Destroy(bullet, 3f);
    }

    private void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
        
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = reloadTime;
        }
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            
            // Обновляем слайдер
            if (reloadSlider != null)
            {
                reloadSlider.value = reloadTimer;
            }

            // Завершение перезарядки
            if (reloadTimer <= 0)
            {
                isReloading = false;
                if (reloadSlider != null)
                {
                    reloadSlider.gameObject.SetActive(false);
                }
            }
        }
    }
}