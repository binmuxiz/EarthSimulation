using System.Collections.Generic;
using UnityEngine;

public class GetData
{
   public int round;
   public string text;
   public List<Choice> choices;
}

public class Choice
{ 
   public string text;
   public Score score;
}

public class Score
{
   public int envScore;
   public int societyScore;
   public int techScore;
   public int economyScore;
}