using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    private string _winSceneName = "WinScene";
    private string _loseSceneName = "LoseScene";
    [SerializeField] private Menus _menus;
    private string _sceneToLoadName = "";

    public void LoadWinSceneAfterDelay(float delay)
    {
        _sceneToLoadName = _winSceneName;
        Invoke(nameof(LoadScene), delay);
    }

    public void LoadLoseSceneAfterDelay(float delay)
    {
        _sceneToLoadName = _loseSceneName;
        Invoke(nameof(LoadScene), delay);
    }

    private void LoadScene()
    {
        Cursor.lockState = CursorLockMode.None;
        _menus.LoadScene(_sceneToLoadName);
    }
}
