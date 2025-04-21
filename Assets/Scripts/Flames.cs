using UnityEngine;

public class Flames : MonoBehaviour
{
    [SerializeField] private float damage = 100f;
    private bool _isPlayerInFlames;
    private Coroutine _damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInFlames = true;
            _damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInFlames = false;
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
            }
        }
    }

    private System.Collections.IEnumerator DamageOverTime()
    {
        while (_isPlayerInFlames)
        {
            PlayerHealth playerHealth = Object.FindAnyObjectByType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}