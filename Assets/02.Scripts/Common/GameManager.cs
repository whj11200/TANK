using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
        set {
            if (instance == null)
                instance = value;
            else if (instance != value)
                Destroy(value);
            }
    }
    [SerializeField] private List<Transform> spawnList;
    [SerializeField] private GameObject apachePrefab;
    [SerializeField] public Text playerCountText;
    public bool isGameOver = false;
    private void Awake()
    {
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CreatTank();
        PhotonNetwork.IsMessageQueueRunning = true;
        apachePrefab = Resources.Load<GameObject>("Apache");

    }
    void CreatTank()
    {
        float pos = Random.RandomRange(10f, -19f);
        PhotonNetwork.Instantiate("Tank",new Vector3(pos,5f,pos),Quaternion.identity);
    }
    void Start()
    {
        var spawnPoint = GameObject.Find("SpawnPoints").gameObject;
        if(spawnPoint != null )
            spawnPoint.GetComponentsInChildren<Transform>(spawnList);

        spawnList.RemoveAt(0);
        if (spawnList.Count > 0)
            StartCoroutine(CreateApache());
    }
    IEnumerator CreateApache()
    {
        while (isGameOver == false)
        {


            int count = (int)GameObject.FindGameObjectsWithTag("APACHE").Length;
            if (count < 10)
            {
                yield return new WaitForSeconds(3.0f);
                int idx = Random.Range(0, spawnList.Count);
                Instantiate(apachePrefab, spawnList[idx].position, spawnList[idx].rotation);
            }
            else
            {
                yield return null;
            }


        }



    }
    [PunRPC]
    void GetConnetPlayerCounter()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Room currentRoom = PhotonNetwork.CurrentRoom;
            playerCountText.text = currentRoom.PlayerCount.ToString()+"/"+currentRoom.ToString();
            photonView.RPC("GetConnetPlayerCounter", RpcTarget.Others);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GetConnetPlayerCounter();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GetConnetPlayerCounter();
    }
    public void OnclickExit()
    {
       
    }

}
