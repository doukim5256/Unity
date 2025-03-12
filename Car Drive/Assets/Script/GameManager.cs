using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;

    public GameObject TITLE;
    private int totalObjects;  // 전체 오브젝트 개수
    private int collectedObjects = 0; // 수집된 오브젝트 개수

    void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
        endPanel.SetActive(false);

        // 특정 태그를 가진 오브젝트 개수 찾기
        totalObjects = GameObject.FindGameObjectsWithTag("PickUp").Length;
        Debug.Log("총 오브젝트 개수: " + totalObjects);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        TITLE.SetActive(false);
        Time.timeScale = 1;
    }

    public void CollectObject()
    {
        collectedObjects++;
        Debug.Log("현재 수집 개수: " + collectedObjects + "/" + totalObjects);

        if (collectedObjects >= totalObjects)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
