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
    }

    public void RemoveWisp()
    {
        m_Wisp = null;
    }

    public void Play()
    {
        if (m_Wisp != null)
            m_Wisp.GetAudio().Play();
        else
            m_Timer = 2;
    }

    public bool IsPlaying()
    {
        if(m_Wisp != null)
            return m_Wisp.GetAudio().isPlaying;
        else
        {
            m_Timer -= Time.deltaTime;
            return m_Timer > 0;
        }
    }

    public bool IsCorrect()
    {
        if (m_Wisp == null)
            return false;

        return m_CorrectMelody == m_CurrentMelody;
    }

}
