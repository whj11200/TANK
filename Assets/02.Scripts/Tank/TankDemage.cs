using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TankDemage : MonoBehaviourPun
{
    [SerializeField]private MeshRenderer[] m_Renderer;
    [SerializeField] private GameObject expeffect;
    private int maxhp = 100;
    private int hp = 0;
    public Canvas hubCanvas;
    public Image hpbar;
    private string playertag = "Player";
 
    void Start()
    {
        m_Renderer = GetComponentsInChildren<MeshRenderer>();
        expeffect = Resources.Load<GameObject>("Explosion");
        hp = maxhp;
        hpbar.color = Color.cyan;
    }

    [PunRPC]
    void OnDeamageRpc(string hit)
    {
        if(hp > 0 && hit == playertag)
        {
            hp -= 25;
            hpbar.fillAmount = (float)hp/(float)maxhp;
            if(hpbar.fillAmount <= 30) { hpbar.color = Color.red; }
            if(hpbar.fillAmount <= 70) {  hpbar.color = Color.yellow; }
            if(hp <= 0)
            {
                StartCoroutine(ExposionTank());
            }
        }
      

    }
    IEnumerator ExposionTank()
    {
        Object effect = GameObject.Instantiate(expeffect,transform.position,Quaternion.identity); 
        Destroy(effect);
        Settankdie(false);
        hubCanvas.enabled = false;
        yield return new WaitForSeconds(3.0f);
        hp = maxhp;
        hpbar.color = Color.green;
        hpbar.fillAmount = 1.0f;
        hubCanvas.enabled = true;
    }

    public void OnDeamage(string a)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("OnDeamageRpc", RpcTarget.All, a);
        }
    }
    void Settankdie(bool isdie)
    {
        foreach(var tank in m_Renderer)
        {
            tank.enabled = isdie;
        }
    }
}
