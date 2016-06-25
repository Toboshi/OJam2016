using UnityEngine;
using System.Collections;

public class WispNote : MonoBehaviour
{
    public enum Melody
    {
        NULL,
        Melody1,
        Melody2,
        Melody3,
        Melody4,
        Melody5,
        Melody6,
        Melody7,
        Melody8
    }

    public Melody m_Melody;

    private bool m_IsCollectable = true;
    private bool m_IsPlacable = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Collecting
    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_IsCollectable && other.tag == "Player")
            StartCoroutine(Collect_cr());
    }

    IEnumerator Collect_cr()
    {
        m_IsCollectable = false;
        // play sound
        Debug.Log("Collected");
        yield return null;
        // move to player
        // disappear
        // attach to player script??
    }

    // Unloading
    void Unload()
    {
        // move to stump
        // attach to crafting manager??
        m_IsPlacable = true;
    }

    // Place
    void OnMouseDrag()
    {
        if (!m_IsPlacable)
            return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        this.transform.position = newPos;
    }
}
