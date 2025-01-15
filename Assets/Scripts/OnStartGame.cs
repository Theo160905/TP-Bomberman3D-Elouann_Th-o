using UnityEngine;

public class OnStartGame : MonoBehaviour
{
    public int PlayerCount;

    public void OnPlayerJoined()
    {
        PlayerCount++;
        Debug.Log(PlayerCount);
    }

    public void OnStart()
    {
        if (PlayerCount == 1)
        {
            //Faire Spawner L'IA
        }
    }
}
