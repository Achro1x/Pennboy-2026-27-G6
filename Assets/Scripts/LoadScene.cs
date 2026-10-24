using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void Load_Scene(int level)
    {
        SceneManager.LoadScene(level);
    }
}