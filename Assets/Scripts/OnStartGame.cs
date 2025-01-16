using UnityEngine;
using UnityEngine.InputSystem;

public class OnStartGame : MonoBehaviour
{
    public int PlayerCount;

    public GameObject prefabIA;

    [SerializeField]
    private Transform SpawnPointA;

    [SerializeField]
    private Transform SpawnPointB;

    public void OnPlayerJoined(PlayerInput input)
    {
        PlayerCount++;

        var index = input.playerIndex;
        if (index == 0)
        {
            Debug.Log("Player 1 joined");
            input.gameObject.transform.position = SpawnPointA.position;
        }
        else if (index == 1)
        {
            Debug.Log("Player 2 Joined");
            input.gameObject.transform.position = SpawnPointB.position;
        }
    }

    public void OnStart()
    {
        if (PlayerCount == 1)
        {
            prefabIA.SetActive(true);
        }
    }
}
