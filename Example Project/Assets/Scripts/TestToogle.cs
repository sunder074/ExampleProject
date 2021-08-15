using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestToogle : MonoBehaviour
{
    [SerializeField] Toggle _toggle_on;
    [SerializeField] Toggle _toggle_off;

    private void Start()
    {
        _toggle_on.onValueChanged.AddListener(OnClick_On);
        _toggle_off.onValueChanged.AddListener(OnClick_Off);
    }

    void OnClick_On(bool ison)
    {

    }

    void OnClick_Off(bool ison)
    {

    }
}
