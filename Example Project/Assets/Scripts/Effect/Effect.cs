using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] float _time;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Cor_OffEffect());
    }

    IEnumerator Cor_OffEffect()
    {
        yield return new WaitForSeconds(_time);

        gameObject.SetActive(false);
    }
}
