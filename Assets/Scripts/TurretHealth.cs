using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float damageAmount = 25f;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private GameObject explosionEffectPrefab;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    private void TakeDamage(float damage, Vector3 hitPosition)
    {
        _currentHealth -= damage;

        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
            Destroy(effect, 0.5f);
        }

        if (_currentHealth <= 0f)
        {
            if (explosionEffectPrefab != null)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(damageAmount, collision.contacts[0].point);
            Destroy(collision.gameObject);
        }
    }
}