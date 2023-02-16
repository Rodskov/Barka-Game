using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Transform cam;

    // The UI follows the player's camera
    void LateUpdate()
    {
		transform.LookAt(transform.position + cam.forward);
    }
}
