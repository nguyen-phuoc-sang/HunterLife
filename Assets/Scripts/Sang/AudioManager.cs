using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    // panel nền tối
    public GameObject nightPanel;
    // thời gian tồn tại panel
    private float transitionTime = 180f;
    // đã đến tối hay chưa
    private bool isNight = false;

    // thời gian
    [SerializeField] TextMeshProUGUI timeText;
    float elapsedTime;
    float maxTime = 1440f;

    public Transform rotatingImage;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayDayMusic();
        elapsedTime = 6 * 60;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > maxTime)
        {
            elapsedTime = 0f;
        }

        int m = Mathf.FloorToInt(elapsedTime / 60);
        int s = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", m, s);

        // Hiển thị panel khi đạt 17
        if (m == 17)
        {
            StartCoroutine(FadeInPanel());
        }
        // Ẩn panel khi đạt 4
        if (m == 4)
        {
            StartCoroutine(FadeOutPanel());
        }
        // chạy nhạc buổi tối khi 9h tối
        if (m == 19 && s == 0)
        {
            if (!isNight)
            {
                isNight = true;
                PlayNightMusic();
                rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rotatingImage.transform.Rotate(new Vector3(0, 0, 40f));
            }
        }
        //chạy nhạc buổi sáng khi 6h sáng
        if (m == 6 && s == 0)
        {
            if (isNight)
            {
                isNight = false;
                PlayDayMusic();
                rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rotatingImage.transform.Rotate(new Vector3(0, 0, 140f));
            }
        }
        if (m == 7 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 132.5f));
        }
        if (m == 8 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 125f));
        }
        if (m == 9 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 117.5f));
        }
        if (m == 10 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 110f));
        }
        if (m == 11 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 102.5f));
        }
        if (m == 12 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 95f));
        }
        if (m == 13 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 87.5f));
        }
        if (m == 14 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 80f));
        }
        if (m == 15 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 72.5f));
        }
        if (m == 16 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 65f));
        }
        if (m == 17 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 57.5f));
        }
        if (m == 18 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 50f));
        }
        if (m == 1 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 56.5f));
        }
        if (m == 2 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 73f));
        }
        if (m == 3 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 89.5f));
        }
        if (m == 4 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 106f));
        }
        if (m == 5 && s == 0)
        {
            rotatingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rotatingImage.transform.Rotate(new Vector3(0, 0, 122.5f));
        }
    }

    // panel hiện dần
    private IEnumerator FadeInPanel()
    {
        CanvasGroup canvasGroup = nightPanel.GetComponent<CanvasGroup>();
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // panel tắt dần
    private IEnumerator FadeOutPanel()
    {
        CanvasGroup canvasGroup = nightPanel.GetComponent<CanvasGroup>();
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // chạy nhạc buổi tối
    private void PlayNightMusic()
    {
        PlayMusic("Night");
    }

    // chạy nhạc buổi sáng
    private void PlayDayMusic()
    {
        PlayMusic("Farm");
    }



    public void StopMusic(string name)
    {
        // Tìm đối tượng Sound tương ứng với tên nhạc
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Không có sound với tên " + name);
        }
        else
        {
            // Nếu tìm thấy, kiểm tra xem có đang phát không
            if (musicSource.clip == s.clip && musicSource.isPlaying)
            {
                // Dừng phát nhạc nền
                musicSource.Stop();
            }
        }
    }



    // nhạc nền
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("không có sound");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    // Tiếng game
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("không có sound");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // bấm nút tắt nhạc nền
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    // bấm nút tắt âm thanh
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
