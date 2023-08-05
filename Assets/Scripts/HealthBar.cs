using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Health _healthComponent;
    [SerializeField] private RectTransform _foreground;
    Vector3 _tempVector3 = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        _healthComponent = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _foreground.localScale = new Vector3(Mathf.Max(0, _healthComponent.GetHealthFactor()), 1, 1);
    }
}
