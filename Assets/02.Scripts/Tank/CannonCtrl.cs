using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    private Transform tr;
    public float rotSpeed = 5000f;
    public float upperAngle = -30f; //���� ����
    public float downAngle = 0f; //���� ���� 
    public float currentRotate = 0f; //���� ȸ�� ���� 
    void Start()
    {
        tr = transform;
    }
    void Update()
    {   //���� ����
        float Wheel = -Input.GetAxisRaw("Mouse ScrollWheel");
        float angle =  Time.deltaTime * rotSpeed * Wheel;
        if(Wheel <= -0.01f) //������ �ø��� 
        {
            currentRotate += angle;
            if(currentRotate > upperAngle)
            {
                tr.Rotate(angle, 0f, 0f);
            }
            else
            {
                currentRotate = upperAngle;
            }
        }
        else //������ ������
        {
            currentRotate += angle;
            if (currentRotate < downAngle)
            {
                tr.Rotate(angle, 0f, 0f);
            }
            else
            {
                currentRotate = downAngle;
            }
        }
       
    }
}
