using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SetAmountOfWinLose : MonoBehaviour
{
    private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private Text _amountOfWinLoseText;

    private void Awake() 
    {
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();

        SetWinLose();
    }

    public void SetWinLose()
    {
        int _amountOfWins = 0;
        _amountOfWins = _saveLoadSystem.GetWinsAmount();
        int _amountOfLoses = _saveLoadSystem.GetLosesAmount();

        string text = _amountOfWinLoseText.text;

        string amountOfWinsString = Convert.ToString(_amountOfWins);
        string amountOfLosesString = Convert.ToString(_amountOfLoses);

        text = text.Replace("%", amountOfWinsString);
        text = text.Replace("&", amountOfLosesString);

        _amountOfWinLoseText.text = text;
    }
}
