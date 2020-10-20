using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset1 : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Part I");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
