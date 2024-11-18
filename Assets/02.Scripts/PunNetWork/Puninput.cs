using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Puninput : MonoBehaviourPunCallbacks
{
    public string Version = "v1.0";
    public Button startbutton;
    public InputField userID;
    public InputField roomname;
   
    void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = Version;
            PhotonNetwork.ConnectUsingSettings();
            roomname.text = "ROOM" + Random.Range(0, 999).ToString("000");
        }
      
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("로비입장");
        PhotonNetwork.JoinLobby();//로비 도착
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("아이디 입력중");
        userID.text = UserId();
    }
    
    public void OncilckJoinRandomRoom()
    {
        PhotonNetwork.NickName = userID.text;
        PlayerPrefs.SetString("user_id",userID.text);
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnclickCreateRoom()
    {
        string _roomName = roomname.text;
        if (string.IsNullOrEmpty(roomname.text))
        {
            _roomName = "Room_" + Random.Range(0, 999).ToString("000");
        }
        PhotonNetwork.NickName = userID.text;
        PlayerPrefs.SetString("USER_ID",userID.text);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방없음");
        PhotonNetwork.CreateRoom("HostRoom", new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방제작");
        StartCoroutine(loadScene());
    }
    public void OnclickRoom(string roomame)
    {
        PhotonNetwork.NickName = userID.text;
        PlayerPrefs.SetString("User_id" ,userID.text);
        PhotonNetwork.JoinRoom(roomame);
    }

    IEnumerator loadScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        AsyncOperation ao = SceneManager.LoadSceneAsync("TankMainScene");
        yield return ao;
    }


    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.InRoom.ToString());
    }
    string UserId()
    {
        string userid = PlayerPrefs.GetString("Userid");
        if(string .IsNullOrEmpty(userid) )
        {
           userid = "User" + Random.Range(0, 999).ToString("000");
        }
        return userid;
    }
}
