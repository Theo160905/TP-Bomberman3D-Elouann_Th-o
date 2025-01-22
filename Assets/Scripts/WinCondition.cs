using TMPro;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject PanelWin;
    public TextMeshProUGUI winnerText;

    public PlayerHealth player1;
    public PlayerHealth player2;

    public GameObject WinObject;

    void FixedUpdate()
    {
        if (player1 == null && player2 == null) return;

        if (player1.Health <= 0)
        {
            DisplayWinner("Joueur 2 a gagné !");
        }
        else if (player2.Health <= 0)
        {
            DisplayWinner("Joueur 1 a gagné !");
            player2.gameObject.SetActive(false);
        }
    }

    void DisplayWinner(string message)
    {
        PanelWin.SetActive(true);
        winnerText.text = message;
        WinObject.SetActive(true);
    }
}
