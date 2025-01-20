using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{
    [Header("System")]
    public GameObject PlayerTarget;
    public NavMeshAgent Agent;
    public IABombDetectionRadius bombDetector;

    private Ray _sensorRayTop;
    private Ray _sensorRayBottom;
    private Ray _sensorRayLeft;
    private Ray _sensorRayRight;

    [Header("Detected objects")]
    public GameObject DetectedTop;
    public GameObject DetectedBottom;
    public GameObject DetectedLeft;
    public GameObject DetectedRight;

    [Header("Debug")]
    public Color TargetColor;
    public Color AdditionalTargetColor;
    public Vector3 AdditionalTarget;
    [field: SerializeField]
    public Queue<GameObject> Bombs { get; private set; } = new();
    private bool _canUseBomb;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        //// TEMPORAIRE
        //// Si le navmesh agent atteint sa destination, il se stoppe
        //if (Vector3.Distance(Agent.destination, this.transform.position) <= 1.5f)
        //{
        //    if (Agent.isStopped) return;
        //}

        print("Current amount of bombs : " + Bombs.Count);
        print("Is it stopped : " + Agent.isStopped);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, PlayerTarget.transform.position) > 4) return;

        _sensorRayTop = new Ray(this.transform.position, Vector3.forward * 3);
        _sensorRayBottom = new Ray(this.transform.position, -Vector3.forward * 3);
        _sensorRayLeft = new Ray(this.transform.position, -Vector3.right * 3);
        _sensorRayRight = new Ray(this.transform.position, Vector3.right * 3);

        if (Physics.SphereCast(_sensorRayTop, 0.3f, out RaycastHit hit1))
        {
            DetectedTop = hit1.collider.gameObject;
            if (Vector3.Distance(DetectedTop.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRayBottom, 0.3f, out RaycastHit hit2))
        {
            DetectedBottom = hit2.collider.gameObject;
            if (Vector3.Distance(DetectedBottom.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRayLeft, 0.3f, out RaycastHit hit3))
        {
            DetectedLeft = hit3.collider.gameObject;
            if (Vector3.Distance(DetectedLeft.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }

        if (Physics.SphereCast(_sensorRayRight, 0.3f, out RaycastHit hit4))
        {
            DetectedRight = hit4.collider.gameObject;
            if (Vector3.Distance(DetectedLeft.transform.position, this.gameObject.transform.position) > 5.5f) return;
        }
    }

    // Permet à l'IA de ramasser les bombes
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && other.TryGetComponent(out Bomb bomb))
        {
            if (!bomb.CanBeRecup) return;
            bomb.CanBeRecup = false;
            bomb.IsOnMap = false;
            Bombs.Enqueue(other.gameObject);
            other.gameObject.SetActive(false);
            bomb.Collider.isTrigger = true;
            IASeekBombState.Check();
        }
    }

    // Permet à l'IA de poser une bombe
    public void UseBomb()
    {
        if (Bombs.Count > 0)
        {
            if (_canUseBomb) return;
            SpawnerBomb.Instance.spawnCount--;
            StartCoroutine(Wait());
            GameObject bomb = Bombs.Dequeue();
            bomb.SetActive(true);
            bomb.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y - 0.5f, Mathf.RoundToInt(gameObject.transform.position.z));
            bomb.gameObject.TryGetComponent(out Bomb bombScript);
            bombScript.ExplodeBomb();
            return;
        }
    }

    private IEnumerator Wait()
    {
        _canUseBomb = true;
        yield return new WaitForSeconds(3f);
        _canUseBomb = false;
    }

    #region Geolocation
    public GameObject DetectGameObjectByLayer(int layer)
    {
        GameObject result = null;
        if (DetectedTop != null)
        {
            if (DetectedTop.layer == layer)
            {
                result = DetectedTop;
                return result;
            }
        }
        if (DetectedBottom != null)
        {
            if (DetectedBottom.layer == layer)
            {
                result = DetectedBottom;
                return result;
            }
        }
        if (DetectedLeft != null)
        {
            if (DetectedLeft.layer == layer)
            {
                result = DetectedLeft;
                return result;
            }
        }
        if (DetectedRight != null)
        {
            if (DetectedRight.layer == layer)
            {
                result = DetectedRight;
                return result;
            }
        }
        return result;
    }

    public GameObject GetNearestBomb()
    {
        if (SpawnerBomb.Instance.OnMapBombs.Count == 0)
        {
            return null;
        }

        GameObject nearestBomb = SpawnerBomb.Instance.OnMapBombs[0];

        foreach (GameObject bomb in SpawnerBomb.Instance.OnMapBombs)
        {
            if (Vector3.Distance(bomb.transform.position, this.transform.position) < Vector3.Distance(nearestBomb.transform.position, this.transform.position))
            {
                if (!Agent.CalculatePath(bomb.transform.position, Agent.path) | !bomb.activeSelf) return nearestBomb;
                nearestBomb = bomb;
            }
        }

        //print($"Nearest bomb : {nearestBomb}, {nearestBomb.transform.position}");
        return nearestBomb;
    }
#endregion
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_sensorRayTop);
        Gizmos.DrawRay(_sensorRayBottom);
        Gizmos.DrawRay(_sensorRayLeft);
        Gizmos.DrawRay(_sensorRayRight);
        Gizmos.color = TargetColor;
        Gizmos.DrawSphere(Agent.destination, 0.5f);
        Gizmos.color = AdditionalTargetColor;
        Gizmos.DrawSphere(AdditionalTarget, 0.5f);
    }
}
