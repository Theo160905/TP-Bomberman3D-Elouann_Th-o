using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseBomb : MonoBehaviour
{
    public BombUI _bombUI;

    private GameObject[] bombList = new GameObject[3];
    private bool usebomb = false;

    public void UseBomb(InputAction.CallbackContext context)
    {
        if (bombList[0] != null)
        {
            if (usebomb) return;
            SpawnerBomb.Instance.spawnCount--;
            StartCoroutine(Wait());
            bombList[0].gameObject.SetActive(true);
            _bombUI.OnDropJuice(bombList.Length - 1);
            //bombList[0].transform.position = gameObject.transform.position;
            bombList[0].transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y - 0.5f, Mathf.RoundToInt(gameObject.transform.position.z));
            bombList[0].gameObject.GetComponent<Bomb>().ExplodeBomb();
            bombList[0] = bombList[1];
            bombList[1] = bombList[2];
            bombList[2] = null;
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if (other.TryGetComponent(out Bomb bomb))
            {
                if (!bomb.CanBeRecup) return;
                for(int i = 0; i < bombList.Length; i++)
                {
                    if (bombList[i] == null)
                    {
                        bombList[i] = other.gameObject;
                        other.gameObject.SetActive(false);
                        bomb.IsOnMap = false;
                        bomb.CanBeRecup = false;
                        bomb.Collider.isTrigger = true;
                        _bombUI.OnPickUpJuice(bombList.Length);
                        return;
                    }
                }             
            }
        }
    }

    private IEnumerator Wait()
    {
        usebomb = true;
        yield return new WaitForSeconds(0.25f);
        usebomb = false;
    }
}
