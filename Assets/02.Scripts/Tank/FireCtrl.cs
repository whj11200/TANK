using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class FireCtrl : MonoBehaviourPun
{
    public GameObject bullet =null;
    public Transform firePos = null;
    public LeaserBeamT BeamT = null;
    public GameObject expEffect;
    private string playerTag = "Player";
    void Start()
    {
      bullet =  Resources.Load<GameObject>("Bullet");
      firePos = transform.GetChild(4).GetChild(1).GetChild(0).GetChild(0).transform;
       BeamT = GetComponentInChildren<LeaserBeamT>();
       expEffect = Resources.Load<GameObject>("Explosion");
    }
    [PunRPC]
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)&& photonView.IsMine)
        {

               Fire();
            photonView.RPC("Fire", RpcTarget.Others);
        }
    }
    [PunRPC]
    void Fire()
    {
        //Instantiate(bullet,firePos.position,firePos.rotation);
        RaycastHit hit;
        Ray ray = new Ray(firePos.position, firePos.forward);
        if(Physics.Raycast(ray,out hit,100f,1<<8|1<<10|1<<9 ))
        {
            BeamT.FireRay();
            ShowEffect(hit);
            if(hit.collider.tag == playerTag)
            {
                string tag = hit.collider.tag;

                hit.collider.transform.parent.SendMessage("OnDeamage", tag, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            BeamT.FireRay();
            Vector3 hitpos = ray.GetPoint(200f);
            Vector3 _normal = firePos.position - hitpos.normalized;
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
            GameObject eff = Instantiate(expEffect, hitpos, rot);
            Destroy(eff, 1.5f);
        }
    }
    void ShowEffect(RaycastHit hitTank)
    {
        Vector3 hitpos = hitTank.point;
        Vector3 _normal = firePos.position - hitpos.normalized;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
        GameObject eff = Instantiate(expEffect,hitpos,rot);
        Destroy(eff,1.5f );
    }
}
