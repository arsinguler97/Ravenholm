using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private AudioSource fireSound;

    private Camera _cam;
    private float _nextFireTime;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitLayers))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;

        if (fireSound != null)
        {
            fireSound.Play();
        }
    }
}