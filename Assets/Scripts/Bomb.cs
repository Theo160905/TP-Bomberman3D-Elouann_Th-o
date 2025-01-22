using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : MonoBehaviour
{
    public bool CanBeRecup;

    public GameObject Explosion;

    public LayerMask levelMask;

    public bool IsOnMap;

    public MeshRenderer MeshRenderer;

    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _litColor;

    public Collider Collider;

    public GameObject VerticalNavMeshModifier;
    public GameObject HorizontalNavMeshModifier;
    public NavMeshObstacle Obstacle;

    public GameObject Smoke_VFX;
    public AudioClip clip;

    public event Action OnSpawn;
    public event Action OnStartExplode;
    public event Action OnExplode;

    public void Start()
    {
        CanBeRecup = true;
    }

    public void Update()
    {
        this.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y/* - 0.5f*/, Mathf.RoundToInt(gameObject.transform.position.z));
        if (IsOnMap)
        {
            CanBeRecup = true;
        }
    }

    [ContextMenu("Explode Bomb")]
    public async void ExplodeBomb()
    {
        Smoke_VFX.SetActive(true);
        Initialize();
        StartCoroutine(CreateExplosion(Vector3.forward));
        StartCoroutine(CreateExplosion(Vector3.back));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.left));
        OnStartExplode?.Invoke();
        await Task.Delay(3000);
        SoundManager.Instance.PlaySound(clip);
        OnExplode?.Invoke();
    }

    public void Reset()
    {
        MeshRenderer.material.color = _baseColor;
        IsOnMap = true;
        Obstacle.enabled = false;
        VerticalNavMeshModifier.SetActive(false);
        HorizontalNavMeshModifier.SetActive(false);
        OnSpawn?.Invoke();
    }

    private void Initialize()
    {
        StartCoroutine(VisualTimer());
        Obstacle.enabled = true;
        Collider.isTrigger = false;

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
        Smoke_VFX.SetActive(false);
        StopCoroutine(VisualTimer());
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

    private IEnumerator VisualTimer()
    {
        while (true)
        {
            MeshRenderer.material.DOColor(_litColor, 0.2f);
            yield return new WaitForSeconds(0.2f);
            MeshRenderer.material.DOColor(_baseColor, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") | CanBeRecup) return;
        Collider.isTrigger = false;
    }
}
