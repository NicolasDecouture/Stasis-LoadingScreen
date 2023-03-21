using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Image loadingSlider;

    [Header("Time")]
    [SerializeField] private float coolDown = 10;
    private float _cooldownClock = -1;

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        _cooldownClock = coolDown;
    }

    public void Update()
    {
        if (_cooldownClock > 0)
        {
            _cooldownClock -= Time.deltaTime;

            loadingSlider.fillAmount = 1 - _cooldownClock / coolDown;
        }
        else if (_cooldownClock != -1)
        {
            SceneManager.LoadScene(1);
            _cooldownClock = -1;
        }
    }

}
