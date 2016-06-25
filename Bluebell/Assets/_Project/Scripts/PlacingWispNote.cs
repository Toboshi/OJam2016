using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    private CraftingManager.Melody m_Melody;

    private bool m_IsWiggling = false;

    private AudioSource m_Audio;

    private Vector2 m_DefaultPos;

    // Use this for initialization
    public void Init(CraftingManager.Melody melody, Vector2 StumpPos)
    {
        m_Melody = melody;
        m_Audio = GetComponent<AudioSource>();
        m_DefaultPos = StumpPos;       

        StartCoroutine(MoveToStump_cr());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public CraftingManager.Melody GetMelody()
    {
        return m_Melody;
    }

    public AudioSource GetAudio()
    {
        return m_Audio;
    }

    // Unloading
    IEnumerator MoveToStump_cr()
    {
        // move to stump
        transform.position = m_DefaultPos;

        yield return null;
        // attach to crafting manager
        CraftingManager.Instance.AddWisp(m_Melody);
        m_IsWiggling = true;
    }

    // Wiggle
    IEnumerator Wiggle_cr()
    {
        if(m_IsWiggling)
        {
            yield return null;
        }
    }

    // Place
    void OnMouseDown()
    {
        m_IsWiggling = false;

        m_Audio.Play();
    }

    void OnMouseDrag()
    {
        if (m_IsWiggling)
            return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        this.transform.position = newPos;
    }

    void OnMouseUp()
    {
        CraftingManager.Instance.PlaceWisp(this, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
