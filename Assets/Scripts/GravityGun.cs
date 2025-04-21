using UnityEngine;

public class GravityGun : MonoBehaviour
{
    private GameObject _heldObject;
    private Rigidbody _heldObjectRb;

    [SerializeField] private float pickUpRange = 2f;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AudioSource throwSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        if (Input.GetMouseButtonDown(0) && _heldObject != null)
        {
            ThrowObject();
        }
    }

    private void TryPickUpObject()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange, pickUpLayer))
        {
            if (hit.transform.CompareTag("canPickUp"))
            {
                PickUpObject(hit.transform.gameObject);
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        _heldObject = obj;
        _heldObjectRb = obj.GetComponent<Rigidbody>();
        _heldObjectRb.isKinematic = true;
        _heldObject.transform.SetParent(holdPosition);
        _heldObject.transform.localPosition = Vector3.zero;
        _heldObject.transform.localRotation = Quaternion.identity;
        _heldObject.layer = LayerMask.NameToLayer("holdLayer");
    }

    private void DropObject()
    {
        if (_heldObject != null)
        {
            _heldObject.layer = LayerMask.NameToLayer("Default");
            _heldObjectRb.isKinematic = false;
            _heldObject.transform.SetParent(null);
            _heldObject = null;
        }
    }

    private void ThrowObject()
    {
        if (_heldObject != null && _heldObjectRb != null)
        {
            _heldObject.transform.SetParent(null);
            _heldObject.layer = LayerMask.NameToLayer("Default");
            _heldObjectRb.isKinematic = false;
            _heldObjectRb.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);
            if (throwSound != null)
            {
                throwSound.Play();
            }
            _heldObject = null;
            _heldObjectRb = null;
        }
    }
}
