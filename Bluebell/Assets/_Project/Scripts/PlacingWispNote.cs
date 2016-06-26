using UnityEngine;
using System.Collections;

public class PlacingWispNote : MonoBehaviour
{
    public float m_TransitionTime = 1f;

    private CraftingManager.Melody m_Melody;

    private bool m_IsWiggling = false;

    private AudioSource m_Audio;

    private Vector2 m_DefaultPos;

    private bool m_GoingUp;

    // Use this for initialization
    public void Init(CraftingManager.Melody melody, Vector2 StumpPos)
    {
        m_Melody = melody;
        m_Audio = GetComponent<AudioSource>();
        m_DefaultPos = StumpPos;
        m_GoingUp = UnityEngine.Random.Range(0, 1) == 0 ? false : true;

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
        // attach to crafting manager
        CraftingManager.Instance.AddWisp(m_Melody);
        m_IsWiggling = true;
        StartCoroutine(Wiggle_cr());
    }

    // Wiggle
    IEnumerator Wiggle_cr()
    {
        while (m_IsWiggling)
        {
            if(m_GoingUp)
            {
                while(transform.position.x < m_DefaultPos.x + 5)
                {
                    Debug.Log("Going Up! " + m_GoingUp);
                    transform.position += Vector3.up * Time.deltaTime;
                    yield return null;
                }
                m_GoingUp = false;
            }
            else
            {
                while (transform.position.x > m_DefaultPos.x - 5)
                {
                    Debug.Log("Going Down! " + m_GoingUp);
                    transform.position -= Vector3.up * Time.deltaTime;
                    yield return null;
                }
                m_GoingUp = true;
            }
            
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
