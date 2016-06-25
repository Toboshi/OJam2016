using UnityEngine;
using System.Collections;

public class CollectingWispNote : MonoBehaviour
{
    

    public CraftingManager.Melody m_Melody;

    private bool m_IsCollectable = true;

    public AudioSource m_Audio;

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
        m_Audio.Play();

        yield return null;

        // move to player
        // disappear
        // attach to player script??
    }
}
