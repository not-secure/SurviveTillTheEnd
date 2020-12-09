using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughStamina : MonoBehaviour
{
    public void Awake()
    {
        StartCoroutine(Disappear());
    }

    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
