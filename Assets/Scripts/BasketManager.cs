using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float maxTriggerDelay = 1f;

    private HashSet<Collider> _triggeredBottom = new HashSet<Collider>();
    private bool _doorOpened = false;

    public void RegisterTrigger(BasketTrigger.TriggerType type, Collider col)
    {
        if (_doorOpened) return;

        if (type == BasketTrigger.TriggerType.Top)
        {
            StartCoroutine(CheckBottomTrigger(col));
        }
        else if (type == BasketTrigger.TriggerType.Bottom)
        {
            _triggeredBottom.Add(col);
        }
    }

    private IEnumerator CheckBottomTrigger(Collider col)
    {
        float timer = 0f;
        while (timer < maxTriggerDelay)
        {
            if (_triggeredBottom.Contains(col))
            {
                Debug.Log("Kobe!");
                door.SetActive(false);
                _doorOpened = true;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }
}