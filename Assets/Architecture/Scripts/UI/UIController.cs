using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button RedLight, GreenLight;


        void Start() => RedLight.onClick.Invoke();


        public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void CloseApplication() => Application.Quit();
    }
}