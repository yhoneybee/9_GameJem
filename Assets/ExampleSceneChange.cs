using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleSceneChange : MonoBehaviour
{
    public void SceneChangeButton(string sceneName)
    {
        Debug.Log(sceneName + " Load.");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game.");
        Application.Quit();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "IngameScene")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Game Over.");
                SceneManager.LoadScene("EndScene");
            }
        }
    }
}
