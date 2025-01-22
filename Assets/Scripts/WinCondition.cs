using TMPro;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject PanelWin;
    public TextMeshProUGUI winnerText;

    public PlayerHealth player1;
    public PlayerHealth player2;

    public GameChrono chrono;
    public GameObject WinObject;

    public AudioClip clip;

    void FixedUpdate()
    {
        if (player1 == null && player2 == null) return;

        if (player1.Health <= 0)
        {
            DisplayWinner("Joueur 2 a gagné !");
            chrono.enabled = false;
        }
        else if (player2.Health <= 0)
        {
            DisplayWinner("Joueur 1 a gagné !");
            chrono.enabled = false;
        }
    }

    void DisplayWinner(string message)
    {
        PanelWin.SetActive(true);
        winnerText.text = message;
        WinObject.SetActive(true);
    }
}
