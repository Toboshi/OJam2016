using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectedWisps : MonoBehaviour
{
    public static CollectedWisps Instance;

    private List<CraftingManager.Melody> m_CollectedMelodies = new List<CraftingManager.Melody>();

    public GameObject m_Wisp;
    public AudioClip[] m_Clips;

    public GameObject[] m_WispStumpPosition;
    private int m_CollectedWisp = 0;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddWisp(CraftingManager.Melody melody)
    {
        m_CollectedMelodies.Add(melody);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stump")
        {
            Unload();

            FindObjectOfType<ZoomCam>().Activate(other.transform.position + new Vector3(3, 3, -10), 8, 2, Easing.Type.QuadOut, 0.15f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stump")
        {
            FindObjectOfType<ZoomCam>().Deactivate();
        }
    }

    public void Unload()
    {
        foreach(CraftingManager.Melody m in m_CollectedMelodies)
        {
            GameObject g = GameObject.Instantiate(m_Wisp);
            g.AddComponent<AudioSource>().clip = m_Clips[(int)m];
            g.GetComponent<PlacingWispNote>().Init(m, this.transform.position, m_WispStumpPosition[m_CollectedWisp++].transform.position);
        }

        m_CollectedMelodies.Clear();
    }
}
