using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleInvisibility : MonoBehaviour
{
    
    private GameObject playerCamera; // The player's camera
    [SerializeField]
    private GameObject Robot;
    private Material robotMaterial;
    public LayerMask raycastLayer; // Layer mask for the raycast

    private Vector3 directionToCapsule;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        directionToCapsule = Vector3.zero;
        if (Robot != null)
        {
            robotMaterial = Robot.GetComponent<Renderer>().material;
            originalColor = robotMaterial.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform mainCamTrasnform = Robot.GetComponent<Path>().GetMainCamera().transform;
        directionToCapsule = Robot.transform.position - mainCamTrasnform.position;

        Ray ray = new Ray(mainCamTrasnform.position, directionToCapsule);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayer))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                MakeRobotInvisible();
            }
            else
            {
                MakeRobotVisible();
            }
        }
    }

    private void MakeRobotInvisible()
    {
        if (robotMaterial != null)
        {
            if (originalColor.a != 0f)
            {
                Color transparentColor = originalColor;
                transparentColor.a = 0f;
                robotMaterial.color = transparentColor;
            }
        }
    }

    private void MakeRobotVisible()
    {
        if (robotMaterial != null)
        {
            robotMaterial.color = originalColor;
        }
    }
}
