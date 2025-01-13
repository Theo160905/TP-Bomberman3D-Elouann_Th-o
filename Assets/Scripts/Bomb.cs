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
        GameObject game = ObjectPoolExplosion.Instance.GetObject(gameObject);
        game.SetActive(true);
        game.transform.position = transform.position;

        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.SphereCast(transform.position + new Vector3(0, .5f, 0),0.25f, direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                GameObject obj = ObjectPoolExplosion.Instance.GetObject(gameObject);
                obj.SetActive(true);
                obj.transform.position = transform.position+ (i*direction);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
