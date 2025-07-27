using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    public void SetBestScore(int score)
    {
        _bestScoreText.text = score.ToString();
    }
}
