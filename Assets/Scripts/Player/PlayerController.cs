using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * -1;

        gameObject.transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0));
        gameObject.transform.Rotate(new Vector3(0, 0, horizontal));
    }
}
