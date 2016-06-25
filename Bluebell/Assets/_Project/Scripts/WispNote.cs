using UnityEngine;
using System.Collections;

public class WispNote : MonoBehaviour
{
    private bool m_IsCollectable = true;
    public MelodyBit.Melody m_Melody;
    //public MelodyBit m_Bit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_IsCollectable && other.tag == "Player")
            StartCoroutine(Collect_cr());
    }

    IEnumerator Collect_cr()
    {
        m_IsCollectable = false;
        Debug.Log("Collected");
        yield return null;
    }
}
