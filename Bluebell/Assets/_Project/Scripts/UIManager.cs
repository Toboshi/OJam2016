using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public float m_TransitionTime = 2;

    public GameObject m_PauseCanvas;

    public GameObject m_EndCanvas;
    public Image m_EndBackground;
    public GameObject m_EndCredits;

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

    [ContextMenu("The End")]
    public void EndGame()
    {
        m_EndCanvas.SetActive(true);

        StartCoroutine(TheEnd_cr());        
    }

    IEnumerator TheEnd_cr()
    {
        Color c = Color.white;
        c.a = 0;

        while(c.a <= 1)
        {
            c.a += Time.deltaTime / m_TransitionTime;
            m_EndBackground.color = c;
            yield return null;
        }

        c.a = 1;
        m_EndBackground.color = c;

        yield return null;

        m_EndCredits.SetActive(true);
    }
}
