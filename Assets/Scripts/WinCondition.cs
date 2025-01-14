using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public void OnPlayerJoined(GameObject game)
    {
        Debug.Log(game.name);
    }
}
