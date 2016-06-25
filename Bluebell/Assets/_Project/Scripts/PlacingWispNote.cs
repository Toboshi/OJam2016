using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    private CraftingManager.Melody m_Melody;

    private bool m_IsWiggling = false;

    private AudioSource m_Audio;

    // Use this for initialization
    public void Init(CraftingManager.Melody melody, Vector2 StumpPos)
    {
        m_Melody = melody;
        m_Audio = GetComponent<AudioSource>();        

        StartCoroutine(MoveToStump_cr(StumpPos));
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
    IEnumerator MoveToStump_cr(Vector2 StumpPos)
    {
        // move to stump
        transform.position = StumpPos;

        yield return null;
        // attach to crafting manager
        CraftingManager.Instance.AddWisp(m_Melody);
        m_IsWiggling = true;
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
