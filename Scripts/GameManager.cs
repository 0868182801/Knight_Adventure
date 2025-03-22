using UnityEngine;
using TMPro;    // Thư viện để use canvas points
using UnityEngine.SceneManagement;  // màn chơi
public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;     // Panel game over
    [SerializeField] private GameObject gameWinUi;      // Panel win game
    private bool isGameOver = false;   
    private bool isGameWin = false;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();  // Khi game bắt đầu cập nhật lại điểm
        gameOverUi.SetActive(false); // Ẩn panel gameover
        gameWinUi.SetActive(false);
    }
    
    public void AddScore(int points)
    {
        if(!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
    }
    private void UpdateScore()  // Cập nhật lại giá trị point mỗi khi layer ăn điểm
    {
        scoreText.text = score.ToString();  // score là kiểu nguyên mà scoreText kiểu chuỗi lên phải ép kiểu để gán
    }
    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;             // K thể ấn phím
        gameOverUi.SetActive(true);     // Hiển thị panel gameover 
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;             //Nhấn phím bthg
        SceneManager.LoadScene("Game");
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;             
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
     public bool IsGameWin()
    {
        return isGameWin;
    }
}
