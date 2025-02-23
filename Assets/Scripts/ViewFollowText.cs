using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFollowText : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject textToFollow;
    // Update is called once per frame
    void Update()
    {
        //Follow the camera's position
        textToFollow.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z + 10f);
    }
}
