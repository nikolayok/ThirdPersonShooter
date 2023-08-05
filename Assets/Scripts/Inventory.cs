using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject[] _weapons;
    private GameObject _currentGun;

    private void Start() 
    {
        _inventory.SetActive(false);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }    
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            _inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SelectWeapon(int choice)
    {
        if (_currentGun != null)
        {
            _currentGun.SetActive(false);
        }

        _currentGun = _weapons[choice];
        _currentGun.SetActive(true);
    }
}
