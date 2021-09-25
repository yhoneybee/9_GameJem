using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTotalManager : MonoBehaviour
{
    [SerializeField] int _endingNumber;
    [SerializeField] float _waitTimeForShowGoBackToLobbyButton = 5f;
    [SerializeField] GameObject _goBackToRobbyButtonGo;
    [SerializeField] GameObject[] _endingCutScenes;

    [SerializeField] AudioStorage[] audioStorages;

    private void Start()
    {
        _endingNumber = ListContainer.LC.EndingNumber;

        switch(_endingNumber)
        {
            default:
            case 0:
                SoundManager.SM.RequestPlayBGM(audioStorages[0].name);
                _endingCutScenes[0].SetActive(true);
                break;
            case 1:
                SoundManager.SM.RequestPlayBGM(audioStorages[1].name);
                _endingCutScenes[1].SetActive(true);
                break;
            case 2:
                SoundManager.SM.RequestPlayBGM(audioStorages[2].name);
                _endingCutScenes[2].SetActive(true);
                break;
            case 3:
                SoundManager.SM.RequestPlayBGM(audioStorages[3].name);
                _endingCutScenes[3].SetActive(true);
                break;
        }

        StartCoroutine(ShowGoBackToLobbyButtonIE());

    }

    IEnumerator ShowGoBackToLobbyButtonIE()
    {
        yield return new WaitForSeconds(_waitTimeForShowGoBackToLobbyButton);
        _goBackToRobbyButtonGo.SetActive(true);
    }

    public void GoToLobby()
    {
        SceneMananagementClass.SMC.LoadSceneAsSync("LobbyScene");
        SceneMananagementClass.SMC.UnLoadSceneAsSync("EndingScene");
    }
}
