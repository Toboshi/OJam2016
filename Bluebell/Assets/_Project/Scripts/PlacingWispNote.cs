using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    private CraftingManager.Melody m_Melody;

    private bool m_IsPlacable = true;

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

    // Unloading
    IEnumerator MoveToStump_cr(Vector2 StumpPos)
    {
        // move to stump
        transform.position = StumpPos;

        yield return null;
        // attach to crafting manager
        CraftingManager.Instance.AddWisp(m_Melody);
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
