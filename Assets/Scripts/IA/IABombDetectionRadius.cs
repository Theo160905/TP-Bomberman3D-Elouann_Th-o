using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABombDetectionRadius : MonoBehaviour
{
    public Transform Target;
    public List<GameObject> DetectedDangerousBombs;

    private void Update()
    {
        this.transform.position = Target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && other.gameObject.TryGetComponent(out Bomb bomb))
        {
            if (bomb.CanBeRecup) return;
            DetectedDangerousBombs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DetectedDangerousBombs.Contains(other.gameObject))
        {
            DetectedDangerousBombs.Remove(other.gameObject);
        }
    }
}
