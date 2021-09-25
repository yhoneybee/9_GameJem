using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleSceneChange : MonoBehaviour
{
    public void SceneChangeButton(string sceneName)
    {
        Debug.Log(sceneName + " Load.");
        SceneMananagementClass.SMC.LoadSceneAsSync(sceneName);
        SceneMananagementClass.SMC.UnLoadSceneAsSync("LobbyScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game.");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneMananagementClass.SMC.LoadSceneAsSync("PicturePresentating");
            SceneMananagementClass.SMC.UnLoadSceneAsSync("IngameScene");
        }
    }
}
