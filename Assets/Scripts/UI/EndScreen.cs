using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class EndScreen : MonoBehaviour
{

    public void UIEVENT_OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UIEVENT_OnExitGame()
    {
        Application.Quit();
    }
}
