using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ray�� ��Ƽ� ���콺 ������ ���� 
public class TurretCtrl : MonoBehaviour
{
    private Transform tr;
    private float rotSpeed = 5f;
    RaycastHit hit;
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
                   //ī�޶󿡼� ���콺 ������ �������� ������ �߻� 
        Debug.DrawRay(ray.origin, ray.direction *100f,Color.green);
       
        if(Physics.Raycast(ray, out hit,100f,1<<8))
        {  //���̰� �ͷ��ο� �¾Ҵٸ� 

            Vector3 relative = tr.InverseTransformPoint(hit.point);
            //�¾Ҵ� ���� ��ġ�� ��ũ�� �´� ������ǥ�� �ٲ�
                                                              // Mathf.Deg2Rad;�Ϲݰ����� ���� ������ �ٲ� 
            float angle = Mathf.Atan2(relative.x,relative.z) * Mathf.Rad2Deg; //���𰢵��� �Ϲݰ����� �ٲ�
                          //��ź��Ʈ �Լ��� Atan2��  ������ ������ ���
            tr.Rotate(0f, angle * Time.deltaTime *rotSpeed, 0f);
        }
    }
}
