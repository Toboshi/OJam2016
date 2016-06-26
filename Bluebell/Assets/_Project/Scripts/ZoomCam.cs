using UnityEngine;
using System.Collections;

public class ZoomCam : MonoBehaviour
{
    [SerializeField]
    FollowCam m_FollowCam;

    [SerializeField]
    AudioSource m_FadeSound;
    float m_FadeVolume = 1;
    IEnumerator m_VomuleCoroutine = null;

    Vector3 m_GoalPos = Vector3.zero;
    bool m_Active = false;
    
    void Start()
    {
        if (m_FadeSound) m_FadeVolume = m_FadeSound.volume;
    }
    
    void Update()
    {
        if (!m_Active) return;

        transform.position = m_GoalPos;
    }

    public void Activate(Vector3 aPos, float aZoom, float aTime, Easing.Type aZoomType = Easing.Type.Linear, float aVolume = 0)
    {
        m_FollowCam.SetActive(false);
        m_Active = true;

        aPos.z = -10;
        Easing.StartTween(this, aZoomType, Camera.main.orthographicSize, aZoom, aTime, ZoomCallback);
        Easing.StartVectorTween(this, Easing.Type.QuadOut, transform.position, aPos, 2, PosCallback);

        if (m_FadeSound)
        {
            if (m_VomuleCoroutine != null) StopCoroutine(m_VomuleCoroutine);
            m_VomuleCoroutine = volume_cr(aVolume);
            StartCoroutine(m_VomuleCoroutine);
        }
    }

    public void Deactivate()
    {
        m_Active = false;
        StopAllCoroutines();
        m_FollowCam.SetActive(true);

        if (m_FadeSound)
        {
            m_VomuleCoroutine = volume_cr(m_FadeVolume);
            StartCoroutine(m_VomuleCoroutine);
        }
    }

    void ZoomCallback(float i)
    {
        Camera.main.orthographicSize = i;
    }

    void PosCallback(Vector3 i)
    {
        m_GoalPos = i;
    }

    IEnumerator volume_cr(float end)
    {
        float start = m_FadeSound.volume;

        float t = 0;
        float duration = Mathf.Abs(start - end) * 2;

        while (t < duration)
        {
            float val = t / duration;
            m_FadeSound.volume = Mathf.Lerp(start, end, val);

            t += Time.deltaTime;
            yield return null;
        }

        m_FadeSound.volume = end;
        m_VomuleCoroutine = null;
    }
}
