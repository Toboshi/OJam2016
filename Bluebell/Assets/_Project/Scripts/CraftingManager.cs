﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    public enum Melody
    {
        Top1,
        Top2,
        Top3,
        Top4,
        Bottom1,
        Bottom2,
        Bottom3,
        Bottom4,
        NULL
    }

    public Bluebell[] m_TopBluebells;
    public Bluebell[] m_BottomBluebells;

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

    // Placing Wisps
    public bool PlacedWisp(PlacingWispNote wisp, Vector2 position)
    {
        // Find the melody's old position & remove it
        //RemoveWisp(wisp);

        // Find which bluebell I'm over
        Bluebell bell = FindBluebell(position);

        // Place melody there or, no where & return to position
        if (bell == null || bell.m_CurrentMelody != Melody.NULL)
        {
            //AddWisp(melody);
            wisp.Reset();
            return false;
        }
        else
        {
            bell.AddWisp(wisp);
            return true;
        }
    }

    public void RemoveWisp(PlacingWispNote wisp)
    {
        for (int i = 0; i < m_TopBluebells.Length; i++)
        {
            if (wisp.GetMelody() == m_TopBluebells[i].m_CurrentMelody)
            {
                m_TopBluebells[i].RemoveWisp();
                return;
            }
        }

        for (int i = 0; i < m_BottomBluebells.Length; i++)
        {
            if (wisp.GetMelody() == m_BottomBluebells[i].m_CurrentMelody)
            {
                m_BottomBluebells[i].RemoveWisp();
                return;
            }
        }
    }

    Bluebell FindBluebell(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Bluebell>() != null)
                return colliders[i].GetComponent<Bluebell>();
        }

        return null;
    }

    // Playing Melody
    public void PlayMelody()
    {
        StartCoroutine(PlayTopMelody_cr());
        StartCoroutine(PlayBottomMelody_cr());
    }

    IEnumerator PlayTopMelody_cr()
    {
        for (int i = 0; i < m_TopBluebells.Length; i++)
        {
            m_TopBluebells[i].Play();

            while (m_TopBluebells[i].IsPlaying())
            {
                yield return null;
            }
        }

        CheckSolution();
    }

    IEnumerator PlayBottomMelody_cr()
    {
        for (int i = 0; i < m_BottomBluebells.Length; i++)
        {
            m_BottomBluebells[i].Play();

            while (m_BottomBluebells[i].IsPlaying())
            {
                yield return null;
            }
        }
    }

    bool CheckSolution()
    {        
        for (int i = 0; i < m_TopBluebells.Length; i++)
        {
            if (!m_TopBluebells[i].IsCorrect())
                return false;
        }

        for (int i = 0; i < m_BottomBluebells.Length; i++)
        {
            if (!m_BottomBluebells[i].IsCorrect())
                return false;
        }

        Debug.Log("Finish!");
        UIManager.Instance.EndGame();
        return true;
    }
}
