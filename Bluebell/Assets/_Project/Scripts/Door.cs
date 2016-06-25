using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        CraftingManager.Instance.PlayMelody();
    }

    void Rumble()
    {
        // For when incorrect
    }

    void Open()
    {
        // For when correct
    }
}
