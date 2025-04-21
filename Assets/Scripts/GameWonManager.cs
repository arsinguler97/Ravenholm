using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameWonManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject headcrabImage;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource jumpscareSound;

    void Start()
    {
        winText.gameObject.SetActive(false);
        headcrabImage.SetActive(false);
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        winText.gameObject.SetActive(true);
        menuSound.Play();

        yield return new WaitForSeconds(5f);

        winText.gameObject.SetActive(false);
        headcrabImage.SetActive(true);
        jumpscareSound.Play();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }
}