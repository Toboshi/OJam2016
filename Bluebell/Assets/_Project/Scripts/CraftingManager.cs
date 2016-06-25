using UnityEngine;
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

    private List<Melody> m_AvailableMelodies = new List<Melody>();

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

    public void AddWisp(Melody melody)
    {
        m_AvailableMelodies.Add(melody);
    }

    public void PlaceWisp(Melody melody, Vector2 position)
    {
        // Find the melody's old position & remove it

        // Find which bluebell I'm over

        // Place melody there or, no where & return to position
    }
}
