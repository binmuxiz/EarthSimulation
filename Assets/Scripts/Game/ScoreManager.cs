using System;
using Global;
using Network;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public TMP_Text[] scoreTexts;

    private Data.Score _score;

    private void Start()
    {
        InitializeScore();
    }

    public void SetScore(Score score)
    {
        _score.Environment += score.envScore;
        _score.Society += score.societyScore;
        _score.Technology += score.techScore;
        _score.Economy += score.economyScore;
        
        scoreTexts[0].text = _score.Environment.ToString();
        scoreTexts[1].text = _score.Society.ToString();
        scoreTexts[2].text = _score.Technology.ToString();
        scoreTexts[3].text = _score.Economy.ToString();       
    }

    
    private void InitializeScore()
    {
        _score = new Data.Score();
    
        // score text 초기화
        scoreTexts[0].text = _score.Environment.ToString();
        scoreTexts[1].text = _score.Society.ToString();
        scoreTexts[2].text = _score.Technology.ToString();
        scoreTexts[3].text = _score.Economy.ToString();        
    }
}
