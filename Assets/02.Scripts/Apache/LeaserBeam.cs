using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaserBeam : MonoBehaviour
{
    [SerializeField]LineRenderer lineRenderer;
    [SerializeField] Transform tr;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = false;//������ǥ�� �ϱ� ���� 
        tr = GetComponent<Transform>();
        
    }
    public void FireRay()
    {
        Ray ray  = new Ray(tr.position+(Vector3.up *0.02f) , tr.forward);
                            // �ణ �ø� , ������ �������� 
                                     //���� Ȥ�� ���� ��ǥ�� ������ǥ�� ��ȯ
        lineRenderer.SetPosition(0,tr.InverseTransformPoint(ray.origin));
                      //���η������� ù��° ���� �߻���ġ�� ��´�. 0 �� ù��° ��
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,200f))
        {   // �¾Ҵٸ�   ���� ������ �������� ��´�. 1
            lineRenderer.SetPosition(1,tr.InverseTransformPoint(hit.point));
        }
        else
        {    //���� �ʾҴٸ�  ������ ������ ray.GetPoint(200f) 200������ �������� ��´�.
            lineRenderer.SetPosition(1,tr.InverseTransformPoint(ray.GetPoint(200f)));
        }

        StartCoroutine(ShowLaserBeam() ); //���η������� ������ �Ⱥ����� �ϱ����� 
    }
    IEnumerator ShowLaserBeam()
    {
        lineRenderer.enabled =true;
        yield return new WaitForSeconds(Random.Range(0.1f,0.3f));
        lineRenderer.enabled = false;

    }
}
