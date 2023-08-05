using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    bool _isPlayerAlive = true;
    private WinLoseManager _winLoseManager;
    private SaveLoadSystem _saveLoadSystem;

    private void Awake() 
    {
        _winLoseManager = GameObject.FindWithTag("WinLoseManager").GetComponent<WinLoseManager>();
        _saveLoadSystem = GameObject.FindWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "EnemyBullet" && _isPlayerAlive)
        {
            // Debug.Log("PlayerDead");
            _isPlayerAlive = false;
            _saveLoadSystem.AddLoseAndSave();
            _winLoseManager.LoadLoseSceneAfterDelay(0.5f);
            Destroy(gameObject, 0.5f);
        }
    }
}
