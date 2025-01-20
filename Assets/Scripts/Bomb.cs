using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : MonoBehaviour
{
    public bool CanBeRecup;

    public GameObject Explosion;

    public LayerMask levelMask;

    public bool IsOnMap;

    public Collider Collider;

    public GameObject VerticalNavMeshModifier;
    public GameObject HorizontalNavMeshModifier;
    public NavMeshObstacle Obstacle; 

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
        Initialize();
        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.back));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.left));
    }

    private void Initialize()
    {
        Obstacle.enabled = true;

        HorizontalNavMeshModifier.SetActive(true);
        HorizontalNavMeshModifier.TryGetComponent(out NavMeshObstacle horizObstacle);
        horizObstacle.size.Set(4.55f, 1, -1);
        VerticalNavMeshModifier.SetActive(true);
        VerticalNavMeshModifier.TryGetComponent(out NavMeshObstacle vertObstacle);
        vertObstacle.size.Set(-1, 1, 4.55f);

        RaycastHit hitTop;
        RaycastHit hitBot;
        RaycastHit hitLeft;
        RaycastHit hitRight;
        Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), 0.25f, Vector3.forward, out hitTop, 3, levelMask);
        Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), 0.25f, Vector3.back, out hitBot, 3, levelMask);
        Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), 0.25f, Vector3.left, out hitLeft, 3, levelMask);
        Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), 0.25f, Vector3.right, out hitRight, 3, levelMask);

        if (hitTop.collider | hitBot.collider)
        {
            vertObstacle.size.Set(vertObstacle.size.x, vertObstacle.size.y, -1);
        }

        if (hitLeft.collider | hitRight.collider)
        {
            horizObstacle.size.Set(-1, horizObstacle.size.y, horizObstacle.size.z);
        }
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
