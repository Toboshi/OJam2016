using UnityEngine;
using System.Collections;

public class CollectingWispNote : MonoBehaviour
{


    public CraftingManager.Melody m_Melody;

    public float m_TransitionTime = 1.5f;

    private bool m_IsCollectable = true;

    public AudioSource m_Audio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End)) StartCoroutine(Collect_cr(Vector3.zero));
    }

    // Collecting
    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_IsCollectable && other.tag == "Player")
            StartCoroutine(Collect_cr(other.transform.position));
    }

    IEnumerator Collect_cr(Vector2 playerPos)
    {
        m_IsCollectable = false;

        // Play sound
        m_Audio.Play();

        yield return null;

        // attach to player script??
        CollectedWisps.Instance.AddWisp(m_Melody);

        // move to player
        float t = 0;
        Vector2 start = transform.position;
        while (t < m_TransitionTime)
        {
            transform.position = Vector3.Lerp(start, playerPos, t / m_TransitionTime);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = playerPos;

        yield return null;

        // Disappear
        //yield return new WaitForSeconds(m_Audio.clip.length);
        Destroy(gameObject);

    }
}
