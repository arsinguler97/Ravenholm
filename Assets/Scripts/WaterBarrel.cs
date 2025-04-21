using UnityEngine;

public class WaterBarrel : MonoBehaviour
{
    [SerializeField] private GameObject splashEffectPrefab;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float velocityThreshold = 5f;

    private bool _hasExploded = false;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasExploded) return;

        if (_rb.linearVelocity.magnitude >= velocityThreshold)
        {
            Explode();
        }
    }

    private void Explode()
    {
        _hasExploded = true;

        GameObject splash = Instantiate(splashEffectPrefab, transform.position, Quaternion.identity);

        Destroy(splash, 1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, ~0, QueryTriggerInteraction.Collide);
        foreach (Collider nearby in colliders)
        {
            if (nearby.CompareTag("Fire"))
            {
                Destroy(nearby.gameObject);
            }
        }

        Destroy(gameObject);
    }
}