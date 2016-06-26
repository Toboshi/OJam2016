using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public float m_TransitionTime = 2;

    public GameObject m_SplashScreenCanvas;
    public Image m_SplashScreen;
    public Text m_TitleText;
    public GameObject m_Title;

    public GameObject m_PauseCanvas;
    public Slider m_Volume;

    public GameObject m_EndCanvas;
    public Image m_EndBackground;
    public GameObject m_EndCredits;

    public Transform m_EndDoor;

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

    public void VolumeSlider()
    {
        AudioListener.volume = m_Volume.value;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        m_EndCanvas.SetActive(true);

        StartCoroutine(TheEnd_cr());        
    }

    IEnumerator StartGame_cr()
    {
        Color white = Color.white;
        Color title = m_TitleText.color;
        float a = 1;

        while (a > 0)
        {
            a -= Time.deltaTime / m_TransitionTime;
            white.a = a;
            title.a = a;
            m_SplashScreen.color = white;
            m_TitleText.color = title;
            yield return null;
        }

        a = 0;
        white.a = a;
        title.a = a;
        m_SplashScreen.color = white;
        m_TitleText.color = title;

        yield return null;

        m_Title.SetActive(false);
        m_GamePaused = false;
    }

    IEnumerator TheEnd_cr()
    {
        GetComponent<AudioSource>().Play();
        FindObjectOfType<ZoomCam>().Activate(m_EndDoor.position, 3, 10.5f);

        yield return new WaitForSeconds(1);

        StartCoroutine(moveDoor_cr());

        Color c = Color.white;
        c.a = 0;

        while(c.a <= 1)
        {
            c.a += Time.deltaTime / 9.5f;
            m_EndBackground.color = c;
            yield return null;
        }

        c.a = 1;
        m_EndBackground.color = c;

        yield return null;

        m_EndCredits.SetActive(true);
    }

    IEnumerator moveDoor_cr()
    {
        Vector3 start = m_EndDoor.position;
        Vector3 end = start + Vector3.down * 8;

        float t = 0;
        float duration = 9.5f;

        while (t < duration)
        {
            float val = t / duration;
            m_EndDoor.position = Easing.Linear(start, end, val);
            
            t += Time.deltaTime;
            yield return null;
        }

        m_EndDoor.position = end;
    }
}
