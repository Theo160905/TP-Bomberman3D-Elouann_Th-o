using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeRecup = true;

    public GameObject Explosion;

    public LayerMask levelMask;

    private void Update()
    {
        
    }

    public void ExplodeBomb()
    {
        CanBeRecup = false;
        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.back));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.left));
    }

    public IEnumerator TimeToExplode()
    {
        yield return new WaitForSeconds(3);
    }

    public IEnumerator CreateExplosion(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,i,levelMask);

            if (!hit.collider)
            {
                Instantiate(Explosion, transform.position + (i * direction),
                Explosion.transform.rotation);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(.05f);
        }

    }
}
