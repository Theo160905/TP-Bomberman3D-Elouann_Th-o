using TMPro;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int pvJoueur1;
    public int pvJoueur2;

    public GameObject PanelWin;
    public TextMeshProUGUI winnerText;

    public PlayerHealth player1;
    public PlayerHealth player2;

    void FixedUpdate()
    {
        pvJoueur1 = player1.Health;
        pvJoueur2 = player2.Health;
        if (pvJoueur1 <= 0)
        {
            AfficherGagnant("Joueur 2 a gagné !");
        }
        else if (pvJoueur2 <= 0)
        {
            AfficherGagnant("Joueur 1 a gagné !");
        }
    }

    void AfficherGagnant(string message)
    {
        PanelWin.SetActive(true);
        winnerText.text = message;
        Time.timeScale = 0;
    }
}
