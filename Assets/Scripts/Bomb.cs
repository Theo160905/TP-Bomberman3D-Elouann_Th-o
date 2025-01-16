using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeRecup;

    public GameObject Explosion;

    public LayerMask levelMask;

    public bool IsOnMap;

    public Collider Collider;

    public void Start()
    {
        CanBeRecup = true;
        //TryGetComponent(out Collider);
    }

    public void Update()
    {
        this.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y/* - 0.5f*/, Mathf.RoundToInt(gameObject.transform.position.z));
        if (IsOnMap)
        {
            CanBeRecup = true;
        }
    }

    public void ExplodeBomb()
    {
        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.back));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.left));
    }

    public IEnumerator CreateExplosion(Vector3 direction)
    {
        yield return new WaitForSeconds(3f);
        GameObject game = ObjectPoolExplosion.Instance.GetObject(gameObject);
        game.SetActive(true);
        game.transform.position = transform.position;

        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), 0.25f, direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                GameObject obj = ObjectPoolExplosion.Instance.GetObject(gameObject);
                obj.SetActive(true);
                obj.transform.position = transform.position + (i * direction);
            }
            yield return new WaitForSeconds(.05f);
        }
        ObjectPoolBomb.Instance.ReturnObject(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Collider.isTrigger = false;
    }
}
