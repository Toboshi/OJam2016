using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
    Vector3 m_PrevPos = Vector3.zero;

    [HideInInspector]
    public bool m_LeftBuddy = false;
    [HideInInspector]
    public bool m_RightBuddy = false;

    void Start()
    {
        m_PrevPos = Camera.main.transform.position;
    }
    
    void Update()
    {
        //Movement
        Vector3 pos = transform.position;
        Vector3 diff = Camera.main.transform.position - m_PrevPos;

        pos.x += diff.x * 0.5f;
        pos.y = Camera.main.transform.position.y;

        transform.position = pos;
        m_PrevPos = Camera.main.transform.position;

        //Tiling
        if (Camera.main.transform.position.x < transform.position.x + Camera.main.orthographicSize && !m_LeftBuddy)
        {
            GameObject b = Instantiate(gameObject, transform.position + Vector3.left * 40, Quaternion.identity) as GameObject;
            b.GetComponent<Background>().m_RightBuddy = true;
            m_LeftBuddy = true;
        }
        else if (Camera.main.transform.position.x > transform.position.x - Camera.main.orthographicSize && !m_RightBuddy)
        {
            GameObject b = Instantiate(gameObject, transform.position + Vector3.right * 40, Quaternion.identity) as GameObject;
            b.GetComponent<Background>().m_LeftBuddy = true;
            m_RightBuddy = true;
        }
    }
}
