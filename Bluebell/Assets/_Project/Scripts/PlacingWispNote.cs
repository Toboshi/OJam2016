using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    public float m_TransitionTime = 1f;

    public SpriteRenderer m_Renderer;

    private CraftingManager.Melody m_Melody;

    private bool m_IsWiggling = false;

    private AudioSource m_Audio;

    private Vector2 m_DefaultPos;

    private float m_BobVariant;

    // Use this for initialization
    public void Init(CraftingManager.Melody melody, Vector2 currentPos, Vector2 stumpPos)
    {
        m_Melody = melody;
        m_Audio = GetComponent<AudioSource>();
        transform.position = currentPos;
        m_DefaultPos = stumpPos;
        m_BobVariant = UnityEngine.Random.Range(-1.0f, 1.0f);

        Reset();
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
        float t = 0;
        Vector2 start = transform.position;
        while (t < m_TransitionTime)
        {
            transform.position = Vector3.Lerp(start, m_DefaultPos, t / m_TransitionTime);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = m_DefaultPos;

        yield return null;

        m_IsWiggling = true;
        StartCoroutine(Wiggle_cr());
    }

    // Wiggle
    IEnumerator Wiggle_cr()
    {
        while (m_IsWiggling)
        {
            transform.position = m_DefaultPos + Vector2.up * Mathf.Sin(Time.time * m_BobVariant);
            yield return null;
        }
    }

    public void Reset()
    {
        StartCoroutine(MoveToStump_cr());
    }

    // Place
    void OnMouseDown()
    {
        m_IsWiggling = false;

        m_Audio.Play();

        CraftingManager.Instance.RemoveWisp(this);
        m_Renderer.enabled = true;
        
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
        if (CraftingManager.Instance.PlacedWisp(this, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            m_Renderer.enabled = false;
    }
}
