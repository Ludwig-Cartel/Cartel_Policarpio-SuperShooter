using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void retry() {
        Application.Quit();
    }

    public void starGame() {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
