using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // private Animator _animator;
    private const float _startHealth = 100;
    private float _currentHealth = 100;

    private WinLoseManager _winLoseManager;
    private SaveLoadSystem _saveLoadSystem;

    private void Awake() 
    {
        _winLoseManager = GameObject.FindWithTag("WinLoseManager").GetComponent<WinLoseManager>();
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            _saveLoadSystem.AddWinAndSave();
            Destroy(gameObject, 0.3f);
            _winLoseManager.LoadWinSceneAfterDelay(0.5f);
        }
    }

    public float GetHealthFactor()
    {
        return _currentHealth / _startHealth;
    }
}
