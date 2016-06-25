using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public float m_Speed = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * m_Speed;
    }
}
