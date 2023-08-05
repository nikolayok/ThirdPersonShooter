using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBulletController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelTransform;
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private float _bulletHitMissDistance = 25f;
    PlayerMoveController _playerMoveController;
    AnimatorController _animatorController;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private float _aimDistance = 10f;

    private void Awake() 
    {
        _playerMoveController = GetComponent<PlayerMoveController>();
        _animatorController = GetComponent<AnimatorController>();
    }

    public void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(_bulletPrefab, _barrelTransform.position, Quaternion.identity, _bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        Transform cameraTransform = _playerMoveController.GetCameraTransform();

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController._target = hit.point;
            bulletController.hit = true;

            Vector3 dir = (hit.point - _barrelTransform.position).normalized;
            Debug.DrawLine (_barrelTransform.position, _barrelTransform.position + dir * 10, Color.red, Mathf.Infinity);
            if (Physics.Raycast(_barrelTransform.position, dir, out hit, Mathf.Infinity))
            {
                bulletController._target = hit.point;
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Health>().TakeDamage(50);
                }
            }
        }
        else
        {
            bulletController._target = cameraTransform.position + cameraTransform.forward * _bulletHitMissDistance;
            bulletController.hit = false;
        }

        // need some fix animation falling pose before use it
        //////////////////////////////////////_animatorController.PistolShoot(); 
    }

    private void Update()  
    {
        Transform cameraTransform = _playerMoveController.GetCameraTransform();
        _aimTarget.position = cameraTransform.position + cameraTransform.forward * _aimDistance;    
    }
}
