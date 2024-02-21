using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] rainParticleSystems;
    [SerializeField] private ParticleSystem rainSplashPS;
    [SerializeField] private float minRainDuration = 60f;
    [SerializeField] private float maxRainDuration = 120f;
    private bool isRainEnabled = false;
    private bool isRaining = false;
    private static bool shouldRain;
    private static float randomRainDuration;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRainIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRainEnabled && !IsAnyRainParticleSystemPlaying())
        {
            UpdateRainIntensity();
        }


    }

    IEnumerator StartRandomRain()
    {
        isRaining = true;

        while (isRainEnabled)
        {
            StartRain();

            // Sử dụng UnityEngine.Random.Range để tạo thời gian mưa ngẫu nhiên
            randomRainDuration = UnityEngine.Random.Range(minRainDuration, maxRainDuration);
            yield return new WaitForSeconds(randomRainDuration);

            // Tắt mưa sau thời gian mưa
            StopRain();
        }
        isRaining = false;
    }

    void StartRain()
    {
        foreach (var rainParticleSystem in rainParticleSystems)
        {
            rainParticleSystem.Play();
            rainSplashPS.Play();          
        }
        Debug.Log("It's raining!");
    }

    void StopRain()
    {
        foreach (var rainParticleSystem in rainParticleSystems)
        {
            rainParticleSystem.Stop();
            rainSplashPS.Stop();
        }
        Debug.Log("Rain stopped.");
        isRainEnabled = false;
    }

    private void EnableRainRandomly()
    {
        // Xác định ngẫu nhiên liệu nên bật mưa hay không
        shouldRain = Random.Range(0f, 1f) > 0.5f;

        // Nếu nên bật mưa, thì kích hoạt hệ thống hạt mưa và đặt cờ là true
        if (shouldRain)
        {
            StartCoroutine(StartRandomRain());
            isRainEnabled = true;
        }
        else
        {
            // Nếu không nên bật mưa, đặt cờ là false
            isRainEnabled = false;
        }
    }

    // Hàm này sẽ được gọi khi bạn muốn cập nhật giá trị mưa
    void UpdateRainIntensity()
    {
        // Lấy tất cả các particle system trong scene
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();

        // Duyệt qua từng particle system
        foreach (ParticleSystem ps in particleSystems)
        {
            // Kiểm tra xem particle system có phải là particle system rain không
            if (ps.CompareTag("RainParticleSystem"))
            {
                EnableRainRandomly();
            }
        }
    }

    bool IsAnyRainParticleSystemPlaying()
    {
        foreach (var rainParticleSystem in rainParticleSystems)
        {
            if (rainParticleSystem.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

}
