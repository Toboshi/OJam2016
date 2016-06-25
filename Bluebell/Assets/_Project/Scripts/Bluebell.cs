using UnityEngine;
using System.Collections;

public class Bluebell : MonoBehaviour
{
    public CraftingManager.Melody m_CorrectMelody;

    public CraftingManager.Melody m_CurrentMelody;

    private PlacingWispNote m_Wisp = null;

    private float m_Timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddWisp(PlacingWispNote wisp)
    {
        m_Wisp = wisp;
        m_CurrentMelody = m_Wisp.GetMelody();
        m_Wisp.gameObject.transform.position = transform.position;
    }

    public void RemoveWisp()
    {
        m_Wisp = null;
        m_CurrentMelody = CraftingManager.Melody.NULL;
    }

    public void Play()
    {
        if (m_Wisp != null)
        {
            m_Wisp.GetAudio().Play();
            m_Timer = 1.5f;
            if (m_CurrentMelody == CraftingManager.Melody.Top1 || m_CurrentMelody == CraftingManager.Melody.Bottom3)
                m_Timer = 2.25f;
        }
        else
            m_Timer = 2;
    }

    public bool IsPlaying()
    {
        m_Timer -= Time.deltaTime;
        return m_Timer > 0;
    }

    public bool IsCorrect()
    {
        if (m_Wisp == null)
            return false;

        return m_CorrectMelody == m_CurrentMelody;
    }

}
