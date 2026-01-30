using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildActivityController : MonoBehaviour
{
    [SerializeField] public bool isActive = true;
    void Start()
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(isActive);
        }
    }
    void Update()
    {

    }
    public void Enable()
    {
        isActive = true;
        Start();
    }

    public void Disable()
    {
        isActive = false;
        Start();
    }

    public void Toggle()
    {
        isActive = !isActive;
        Start();
    }
}
