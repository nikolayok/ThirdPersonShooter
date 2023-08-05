using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private string _winAmountName = "WinAmount";
    private string _loseAmountName = "LoseAmount";
    private int _currentWinAmount = 0;
    private int _currentLoseAmount = 0;

    public void AddWinAndSave()
    {
        LoadWinAmount();
        _currentWinAmount += 1;
        PlayerPrefs.SetInt(_winAmountName, _currentWinAmount);
    }

    public void AddLoseAndSave()
    {
        LoadLoseAmount();
        _currentLoseAmount += 1;
        PlayerPrefs.SetInt(_loseAmountName, _currentLoseAmount);
    }

    private void LoadWinAmount()
    {
        _currentWinAmount = PlayerPrefs.GetInt(_winAmountName);
    }

    private void LoadLoseAmount()
    {
        _currentLoseAmount = PlayerPrefs.GetInt(_loseAmountName);
    }

    public int GetWinsAmount()
    {
        LoadWinAmount();
        return _currentWinAmount;
    }

    public int GetLosesAmount()
    {
        LoadLoseAmount();
        return _currentLoseAmount;
    }
}
