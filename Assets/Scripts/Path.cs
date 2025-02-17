using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Runtime.CompilerServices;

namespace UnityEngine
{


    public class Path : MonoBehaviour
    {
        public Transform[] Points;
        public InputActionReference toggleReference;
        public InputActionReference rotateReference;
        public GameObject path;
        public GameObject pathParent;

        public GameObject pathStart;
        public displayObject displayObject;

        [SerializeField]
        private float rotationSpeed = 30f;
        [SerializeField]
        private float startThreshold = 0.1f;
        [SerializeField]
        private float cornerThreshold = 2.50f;

        private string filePath;
        private string outputString;
        private GameObject mainCamera;
        private float[] moveSpeeds = { 0.8759f, 1.8271f, 1.4777f, 1.6173f, 0.9857f, 1.9980f, 0.8831f, 1.5290f, 1.8528f, 0.9548f, 0.6855f, 1.3045f, 1.5628f, 0.5291f, 0.8537f, 1.8775f, 1.2766f, 1.3836f, 1.3689f, 1.9237f};
        private int speedIndex;
        private int pointsIndex;
        private float tDelt;
        private float timeThresh = 2.0f;
        private float csvTimer;
        private bool toggleState = true;
        private bool isRotating = false;
        private static bool isMoving = true;
        private Transform facing;
        private static bool previousMovingState = true;
        void Start()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
            isMoving = false;
            facing = this.transform;
            previousMovingState = isMoving;
            tDelt = 0;
            csvTimer = 0;
            filePath = System.IO.Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("HH-mm-ss") + ".csv");
            Debug.Log("Filepath is: " + filePath);
            pointsIndex = 0;
            transform.position = Points[pointsIndex].transform.position;
            outputString = "";
            toggleState = true;
            speedIndex = 0;
            mainCamera = GameObject.Find("XR Origin (XR Rig)/Camera Offset/Main Camera");
            if (!mainCamera)
            {
                Debug.LogError("No camera found");
            }
            if (!displayObject)
            {
                displayObject = GameObject.Find("XR Origin (XR Rig)/Canvas/Holder").GetComponent<displayObject>();
            }
            toggleReference.action.started += TogglePathMesh;
            rotateReference.action.started += RotatePath;
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(new Vector3(mainCamera.transform.position.x, facing.position.y, mainCamera.transform.position.z));

            if (tDelt >= timeThresh)
            {
                print("speed Changed");
                tDelt = 0;
                //Gets a random index from the array
                speedIndex = Random.Range(0, moveSpeeds.Length);
                Debug.Log(speedIndex);
                //puts time threshold to a random plus or minus .5 value
                timeThresh = 2 + Random.Range(-0.5f, 0.5f);
            }

            if (pointsIndex <= Points.Length - 1)
            {
                if (isMoving)
                {
                    tDelt += Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeeds[speedIndex] * Time.deltaTime);
                }

                if (transform.position == Points[pointsIndex].transform.position)
                {
                    pointsIndex++;
                }

                if (pointsIndex == Points.Length)
                {
                    pointsIndex = 0;
                }
            }

            if (isRotating)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                pathParent.transform.Rotate(Vector3.up, rotationAmount);
            }

            csvTimer += Time.deltaTime;
            if (csvTimer >= 0.05f) // 1/20th of a second
            {
                outputString = "";

                float distance = Vector3.Distance(mainCamera.transform.position, this.transform.position);
                
                outputString += distance.ToString() + ',';
                outputString += System.DateTime.Now.ToString("HH-mm-ss.fff") + ',';

                float distanceFromNearestCorner = float.PositiveInfinity;
                foreach (Transform point in Points)
                {
                    float distanceFromCorner = Vector3.Distance(mainCamera.transform.position, point.transform.position);
                    distanceFromNearestCorner = Math.Min(distanceFromNearestCorner, distanceFromCorner);
                }

                RecordData(distanceFromNearestCorner <= cornerThreshold, "Within corner thresh");

                float distanceFromStart = Vector3.Distance(mainCamera.transform.position, pathStart.transform.position);

                if (displayObject.flashingToggle == displayObject.FlashingToggle.FlashingOn)
                {
                    outputString += "Flashing On" + ',';
                    displayObject.flashingToggle = displayObject.FlashingToggle.NoToggle;
                }
                else if (displayObject.flashingToggle == displayObject.FlashingToggle.FlashingOff)
                {
                    outputString += "Flashing Off" + ',';
                    displayObject.flashingToggle = displayObject.FlashingToggle.NoToggle;
                } else
                {
                    outputString += ',';
                }

                if (isMoving != previousMovingState)
                {
                    //if the moving state has changed then add it to the output string
                    outputString += (isMoving) ? "Start" : "Stop";
                    previousMovingState = isMoving;
                } else
                {
                    outputString += ',';
                }

                //only executes this part of the code when outside of yth
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(outputString);

                if (!File.Exists(filePath))
                    File.WriteAllText(filePath, sb.ToString());
                else
                    File.AppendAllText(filePath, sb.ToString());

                csvTimer = 0; // Reset timer after writing
            }
        }
        private void OnDestroy()
        {
            toggleReference.action.started -= TogglePathMesh;
            rotateReference.action.performed -= RotatePath;
        }

        private void TogglePathMesh(InputAction.CallbackContext context)
        {
            if (toggleState)
            {
                path.SetActive(false);
                toggleState = !toggleState;
            }
            else
            {
                path.SetActive(true);
                toggleState = !toggleState;
            }
        }

        private void RecordData(bool conditional, string str)
        {
            if (conditional)
            {
                outputString += str + ',';
            } else
            {
                outputString += ',';
            }
        }
        private void RotatePath(InputAction.CallbackContext context)
        {
            isRotating = !isRotating;

        }

        public static void toggleTrackerMovement() // New method to toggle movement
        {
            isMoving = !isMoving;
        }
    }
}
