// Code written by Gabriel Mailhot, 17/08/2023.

namespace LogRaamConfiguration
{
   public interface IOption
   {
      int GetAlphaValueFor(in string[] content, string valueTag);
      float GetFloatValueFor(in string[] content, string valueTag);
      int GetIntegerValueFor(in string[] content, string valueTag);
      bool IsOptionActivated(in string[] content, string activeTag);
      bool RetrieveAnswerFor(in string[] content, string tag);
   }
}