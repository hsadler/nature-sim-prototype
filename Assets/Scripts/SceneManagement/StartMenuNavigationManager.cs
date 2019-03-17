using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuNavigationManager : MonoBehaviour
{

    public void ExitGame() {
        Application.Quit();
    }

    public void GoTo2dDemo() {
        SceneManager.LoadScene("2dDemo");
    }

    public void GoTo3dDemo() {
        SceneManager.LoadScene("3dDemo");
    }

}
