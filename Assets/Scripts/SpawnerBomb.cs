using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerBomb : MonoBehaviour
{
    private float spawnRadius = 5f;
    public int spawnCount = 0;
    public List<GameObject> OnMapBombs = new();

    //Singleton
    #region Singleton
    private static SpawnerBomb _instance;

    public static SpawnerBomb Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SpawnerBomb");
                _instance = go.AddComponent<SpawnerBomb>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion

    public void OnSpawnBomb()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        bool spawned = false;
        int attempts = 0;

        while (!spawned && attempts < 10 && spawnCount < 5)
        {
            Vector3 randomPosition = spawnPosition + new Vector3(Random.Range(-spawnRadius, spawnRadius), 1, Random.Range(-spawnRadius, spawnRadius));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, NavMesh.AllAreas))
            {
                GameObject game = ObjectPoolBomb.Instance.GetBomb();
                game.GetComponent<Bomb>().IsOnMap = true;
                game.GetComponent<Bomb>().Obstacle.enabled = false;
                game.GetComponent<Bomb>().VerticalNavMeshModifier.SetActive(false);
                game.GetComponent<Bomb>().HorizontalNavMeshModifier.SetActive(false);
                //game.GetComponent<Bomb>().CanBeRecup = true;

                if (!OnMapBombs.Contains(game)) OnMapBombs.Add(game);

                game.transform.position = new Vector3(Mathf.RoundToInt(hit.position.x), hit.position.y + 0.25f, Mathf.RoundToInt(hit.position.z));
                //game.transform.position = randomPosition;
                spawned = true;
                spawnCount++;
            }
            else
            {
                attempts++;
            }
        }

        if (!spawned && spawnCount < 5)
        {
            Debug.LogError("Aucun point valide trouvé sur la NavMesh après plusieurs tentatives");
        }
    }
}
