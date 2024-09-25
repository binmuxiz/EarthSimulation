using Global;
using TMPro;
using Data;
using UnityEngine;

namespace Game
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private TMP_Text envScoreText;
        [SerializeField] private TMP_Text societyScoreText;
        [SerializeField] private TMP_Text techScoreText;
        [SerializeField] private TMP_Text ecoScoreText;
        
        private void Start()
        {
            InitializeScore();
        }

        public void SetScore(Network.Score score)
        {
            Score.Environment += score.envScore;
            Score.Society += score.societyScore;
            Score.Technology += score.techScore;
            Score.Economy += score.economyScore;
        
            envScoreText.text = Score.Environment.ToString();
            societyScoreText.text = Score.Society.ToString();
            techScoreText.text = Score.Technology.ToString();
            ecoScoreText.text = Score.Economy.ToString();       
        }

    
        private void InitializeScore()
        {
            // score text 초기화
            envScoreText.text = Score.Environment.ToString();
            societyScoreText.text = Score.Society.ToString();
            techScoreText.text = Score.Technology.ToString();
            ecoScoreText.text = Score.Economy.ToString();        
        }
    }
}
