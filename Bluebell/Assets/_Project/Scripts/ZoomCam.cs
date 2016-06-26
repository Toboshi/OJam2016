using UnityEngine;
using System.Collections;

public class ZoomCam : MonoBehaviour
{
    [SerializeField]
    FollowCam m_FollowCam;

    Vector3 m_GoalPos = Vector3.zero;
    bool m_Active = false;
    
    void Start()
    {

    }
    
    void Update()
    {
        if (!m_Active) return;

        transform.position = m_GoalPos;
    }

    public void Activate(Vector3 aPos, float aZoom, float aTime, Easing.Type aZoomType = Easing.Type.Linear)
    {
        m_FollowCam.SetActive(false);
        m_Active = true;

        aPos.z = -10;
        Easing.StartTween(this, aZoomType, Camera.main.orthographicSize, aZoom, aTime, ZoomCallback);
        Easing.StartVectorTween(this, Easing.Type.QuadOut, transform.position, aPos, 2, PosCallback);
    }

    public void Deactivate()
    {
        m_Active = false;
        StopAllCoroutines();
        m_FollowCam.SetActive(true);
    }

    void ZoomCallback(float i)
    {
        Camera.main.orthographicSize = i;
    }

    void PosCallback(Vector3 i)
    {
        m_GoalPos = i;
    }
}
