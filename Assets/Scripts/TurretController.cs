using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float laserSpeed = 20f;
    [SerializeField] private float detectionRange = 30f;
    [SerializeField] private AudioSource fireAudio;

    private Transform _player;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
            StartCoroutine(FireLoop());
        }
    }

    void Update()
    {
        if (_player == null) return;

        Vector3 directionToPlayer = _player.position - firePoint.position;
        Vector3 flatDirection = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z);

        Quaternion targetRotation = Quaternion.LookRotation(flatDirection);
        rotatingPart.rotation = Quaternion.Slerp(rotatingPart.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            if (_player != null && HasLineOfSight())
            {
                Shoot();
            }
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (_player.position + Vector3.up * 1f) - firePoint.position;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, direction.normalized, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Değişiklik: linearVelocity yerine velocity kullanıyoruz
            rb.linearVelocity = firePoint.forward * laserSpeed;
        }

        fireAudio.Play();
    }
}
