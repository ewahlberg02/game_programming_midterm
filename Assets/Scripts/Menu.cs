using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void PlayLevel() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
