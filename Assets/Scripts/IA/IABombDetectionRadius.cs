using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABombDetectionRadius : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private SpawnerBomb _bombSpawner;
    [SerializeField] private float _radius;
    public List<GameObject> DetectedDangerousBombs = new();

    private void Update()
    {
        this.transform.position = Target.position;
        foreach (GameObject bomb in _bombSpawner.OnMapBombs)
        {
            bomb.TryGetComponent(out Bomb bombScript);
            if (DetectedDangerousBombs.Contains(bomb))
            {
                // Remove bomb from list ?
                if(bombScript.IsOnMap | !bomb.gameObject.activeInHierarchy | Vector3.Distance(this.transform.position, bomb.transform.position) > _radius) 
                {
                    DetectedDangerousBombs.Remove(bomb); 
                }
            }
            else
            {
                // Add bomb to list ?
                if (!bombScript.IsOnMap && bomb.gameObject.activeInHierarchy && Vector3.Distance(this.transform.position, bomb.transform.position) <= _radius)
                {
                    DetectedDangerousBombs.Add(bomb);
                }
            }




            //if (Vector3.Distance(this.transform.position, bomb.transform.position) <= _radius && !DetectedDangerousBombs.Contains(bomb))
            //{
            //    if (!bombScript.CanBeRecup && bomb.activeInHierarchy && bombScript.IsOnMap)
            //    {
            //        DetectedDangerousBombs.Add(bomb);
            //    }
            //}

            //if (!bomb.activeInHierarchy)
            //{
            //    DetectedDangerousBombs.Remove(bomb);
            //}


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 7 && other.gameObject.TryGetComponent(out Bomb bomb))
    //    {
    //        print("first check" + other.gameObject.name);
    //        if (bomb.CanBeRecup) return;
    //        DetectedDangerousBombs.Add(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (DetectedDangerousBombs.Contains(other.gameObject))
    //    {
    //        DetectedDangerousBombs.Remove(other.gameObject);
    //    }
    //}
}
