using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    float m_LeadDistance = 4;

    public bool m_Active = true;

    Vector3 m_PreviousPos = Vector3.zero;
    Vector3 m_GoalPos = Vector3.zero;

    IEnumerator m_PanCoroutine = null;

    void Start()
    {
        transform.localPosition = Vector3.forward * -10;
        m_PreviousPos = transform.parent.position;
        m_GoalPos = transform.parent.position;
    }
    
    void Update()
    {
        if (!m_Active) return;

        if (transform.parent.position.x < m_PreviousPos.x && m_GoalPos.x >= 0)
        {
            if (m_PanCoroutine != null) StopCoroutine(m_PanCoroutine);
            m_PanCoroutine = pan_cr(Vector2.left);
            StartCoroutine(m_PanCoroutine);
        }
        else if (transform.parent.position.x > m_PreviousPos.x && m_GoalPos.x <= 0)
        {
            if (m_PanCoroutine != null) StopCoroutine(m_PanCoroutine);
            m_PanCoroutine = pan_cr(Vector2.right);
            StartCoroutine(m_PanCoroutine);
        }

        m_PreviousPos = transform.parent.position;
    }

    IEnumerator pan_cr(Vector2 aDir)
    {
        Vector3 start = transform.localPosition;
        m_GoalPos = aDir.normalized * m_LeadDistance;
        m_GoalPos += Vector3.forward * -10;

        float t = 0;
        float duration = Vector3.Distance(start, m_GoalPos) * 0.2f;

        while (t < duration)
        {
            float val = t / duration;
            transform.localPosition = Easing.EaseOutQuad(start, m_GoalPos, val);

            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = m_GoalPos;
        m_PanCoroutine = null;
    }
}
