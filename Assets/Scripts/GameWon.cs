using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameWon");
        }
    }
}