using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 50f;

    // This rotates the camera smoothly
    void Update()
    {
		transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
