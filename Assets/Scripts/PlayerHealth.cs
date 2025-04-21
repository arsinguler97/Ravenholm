using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;

    [SerializeField] private Image healthBarImage;

    private void Start()
    {
        _currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damageReceived)
    {
        _currentHealth -= (int)damageReceived;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void UpdateHealthUI()
    {
        float healthPercentage = _currentHealth / (float)maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }
}