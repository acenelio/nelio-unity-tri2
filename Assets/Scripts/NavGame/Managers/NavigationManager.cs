using UnityEngine;
using UnityEngine.SceneManagement;
 
public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
 
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
