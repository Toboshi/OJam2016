using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public float m_TransitionTime = 2;

    public GameObject m_SplashScreenCanvas;
    public Image m_SplashScreen;
    public GameObject m_Title;

    public GameObject m_PauseCanvas;
    public Slider m_GameSound;
    public Slider m_AmbientSound;

    public GameObject m_EndCanvas;
    public Image m_EndBackground;
    public GameObject m_EndCredits;

    private bool m_GamePaused = true;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        StartCoroutine(StartGame_cr());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public bool IsGamePaused()
    {
        return m_GamePaused;
    }

    public void Pause()
    {
        m_GamePaused = !m_GamePaused;
        m_PauseCanvas.SetActive(m_GamePaused);
    }

    public void EndGame()
    {
        m_EndCanvas.SetActive(true);

        StartCoroutine(TheEnd_cr());        
    }

    IEnumerator StartGame_cr()
    {
        Color c = Color.white;
        c.a = 1;

        while (c.a > 0)
        {
            c.a -= Time.deltaTime / m_TransitionTime;
            m_SplashScreen.color = c;
            yield return null;
        }

        c.a = 0;
        m_EndBackground.color = c;

        yield return null;

        m_Title.SetActive(false);
        m_GamePaused = false;
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
