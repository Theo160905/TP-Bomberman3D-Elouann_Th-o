using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABombDetectionRadius : MonoBehaviour
{
    public List<GameObject> DetectedDangerousBombs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 && other.gameObject.TryGetComponent(out Bomb bomb))
        { 
            print("truc");
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
