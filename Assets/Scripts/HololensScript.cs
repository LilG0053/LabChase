using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensScript : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public GameObject imageObject;
    public displayObject displayObject;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("parthandpriyankaarebullies", roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        switch (eventCode)
        {
            case Utility.PauseTrackerCode:
                // Get the Path component from the tracker GameObject
                Path.toggleTrackerMovement();
                break;
            case Utility.MoveLeftEventCode:
                displayObject.moveLeft();
                break;
            case Utility.MoveRightEventCode:
                displayObject.moveRight();
                break;
            case Utility.MoveUpEventCode:
                displayObject.moveUp();
                break;
            case Utility.MoveDownEventCode:
                displayObject.moveDown();
                break;
            case Utility.ScaleUpEventCode:
                displayObject.scaleUp();
                break;
            case Utility.ScaleDownEventCode:
                displayObject.scaleDown();
                break;
            case Utility.NextEventCode:
                displayObject.nextImage();
                break;
            case Utility.PreviousEventCode:
                displayObject.previousImage();
                break;
            case Utility.ToggleFlashingEventCode:
                displayObject.toggleFlashing();
                break;
            case Utility.MoveLeftJEventCode:
                displayObject.moveLeftJ();
                break;
            case Utility.MoveRightJEventCode:
                displayObject.moveRightJ();
                break;
            case Utility.ToggleOneEyeEventCode:
                displayObject.toggleOneEye();
                break;
            case Utility.ToggleOneEyeFlashEventCode:
                displayObject.toggleOneEyeFlash();
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
