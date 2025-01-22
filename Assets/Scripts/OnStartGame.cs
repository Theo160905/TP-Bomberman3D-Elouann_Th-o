using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnStartGame : MonoBehaviour
{
    public TextMeshProUGUI textTimeToStart;
    public AudioClip audioClip;
    public WinCondition condition;
    public GameChrono chrono;

    public GameObject Player1;
    public GameObject Player2;

    private int time = 4;

    public void Onstart()
    {
        SoundManager.Instance.PlaySound(audioClip);
        StartCoroutine(TimerToStartGame());
    }

    public IEnumerator TimerToStartGame()
    {
        for (int i = 0; i < 4 ; i++)
        {
            time--;
            textTimeToStart.gameObject.SetActive(true);
            if (time > 0)
            {
                textTimeToStart.text = time.ToString() ;
                yield return new WaitForSeconds(1f);
            }
            else if (time == 0)
            {
                textTimeToStart.text = "GO !";
                yield return new WaitForSeconds(1f);
            }

        }
        textTimeToStart.gameObject.SetActive(false);
        Player1.SetActive(true);
        Player2.SetActive(true);
        condition.enabled = true;
        chrono.enabled = true;
    }

}
