using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerIDName : MonoBehaviourPun
{
    public Text playid;
    void Start()
    {
        playid.text = photonView.Owner.NickName;
    }

}
