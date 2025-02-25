using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensScript : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public GameObject imageObject;
    public DisplayObjectManager DisplayObjectManager;

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
                DisplayObjectManager.moveLeft();
                break;
            case Utility.MoveRightEventCode:
                DisplayObjectManager.moveRight();
                break;
            case Utility.MoveUpEventCode:
                DisplayObjectManager.moveUp();
                break;
            case Utility.MoveDownEventCode:
                DisplayObjectManager.moveDown();
                break;
            case Utility.ScaleUpEventCode:
                DisplayObjectManager.scaleUp();
                break;
            case Utility.ScaleDownEventCode:
                DisplayObjectManager.scaleDown();
                break;
            case Utility.NextEventCode:
                DisplayObjectManager.nextImage();
                break;
            case Utility.PreviousEventCode:
                DisplayObjectManager.previousImage();
                break;
            case Utility.ToggleFlashingEventCode:
                DisplayObjectManager.toggleFlashing();
                break;
            case Utility.MoveLeftJEventCode:
                DisplayObjectManager.moveLeftJ();
                break;
            case Utility.MoveRightJEventCode:
                DisplayObjectManager.moveRightJ();
                break;
            case Utility.ToggleOneEyeEventCode:
                DisplayObjectManager.toggleOneEye();
                break;
            case Utility.ToggleOneEyeFlashEventCode:
                DisplayObjectManager.toggleOneEyeFlash();
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
