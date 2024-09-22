using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

public class GetData
{
   public int round;
   public string story;
   public List<Choice> choices;

   public override string ToString()
   {
      StringBuilder sb = new StringBuilder();

      sb.Append("[GetData.ToString()]");
      sb.Append($"\nround {round}");
      sb.Append($"\nstory : {story}");
      sb.Append("\n----선택지----");

      for (int i = 0; i < choices.Count; i++)
      {
         var choice = choices[i];
         sb.Append($"\n선택지 {choice.text}");
         sb.Append($"\nenvScore : {choice.score.envScore}");
         sb.Append($"\nsocietyScore : {choice.score.societyScore}");
         sb.Append($"\ntechScore : {choice.score.techScore}");
         sb.Append($"\neconomyScore : {choice.score.economyScore}");
         sb.Append("\n--------------");
      }
      return sb.ToString();
   }
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