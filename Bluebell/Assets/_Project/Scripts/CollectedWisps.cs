using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectedWisps : MonoBehaviour
{
    public static CollectedWisps Instance;

    private List<CraftingManager.Melody> m_CollectedMelodies = new List<CraftingManager.Melody>();

    public GameObject m_Wisp;
    public AudioClip[] m_Clips;

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

    public void Unload(Vector2 position)
    {
        foreach(CraftingManager.Melody m in m_CollectedMelodies)
        {
            GameObject g = GameObject.Instantiate(m_Wisp);
            g.AddComponent<AudioSource>().clip = m_Clips[(int)m];
            g.GetComponent<PlacingWispNote>().Init(m, position);
        }

        m_CollectedMelodies.Clear();
    }
}
