using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    public CraftingManager.Melody m_Melody;
    
    private bool m_IsPlacable = true;

    public AudioSource m_Audio;

    // Use this for initialization
    void Init(CraftingManager.Melody melody)
    {
        m_Melody = melody;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Unloading
    void Unload()
    {
        // move to stump
        // attach to crafting manager??
        m_IsPlacable = true;
    }

    // Place
    void OnMouseDown()
    {
        m_Audio.Play();
    }

    void OnMouseDrag()
    {
        if (!m_IsPlacable)
            return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        this.transform.position = newPos;
    }

    void OnMouseUp()
    {
        CraftingManager.Instance.PlaceWisp(m_Melody, Input.mousePosition);
    }
}
