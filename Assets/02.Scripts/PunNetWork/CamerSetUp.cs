using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamerSetUp : MonoBehaviourPun
{
    

    private void Start()
    {
    
        CinemachineVirtualCamera virtualCamera =FindAnyObjectByType(typeof(CinemachineVirtualCamera))as CinemachineVirtualCamera;
        if (photonView.IsMine)
        {
            virtualCamera.LookAt = transform;
            virtualCamera.Follow = transform;
        }
    }
    private void Update()
    {
        
        
    }
}
