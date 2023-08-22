// Code written by Gabriel Mailhot, 17/08/2023.

namespace LogRaamConfiguration
{
   public class OptionBase : IOption
   {
      private readonly IAlphaValueConverter _converter;

      public OptionBase(IAlphaValueConverter converter)
      {
         _converter = converter;
      }

      public OptionBase() { }

      public int GetAlphaValueFor(in string[] content, string valueTag)
      {
         var s = new ConfigLoader().RetrieveAlphaValueFrom(content, valueTag).ToLower();

         if (_converter == null) return 0;
         var result = _converter.Convert(s);

         return result;
      }

      public float GetFloatValueFor(in string[] content, string valueTag)
      {
         return new ConfigLoader().RetrieveFloatValueFrom(content, valueTag);
      }

      public int GetIntegerValueFor(in string[] content, string valueTag)
      {
         return new ConfigLoader().RetrieveIntegerValueFrom(content, valueTag);
      }


      /*
      public int GetStaggerAlphaValueFor(in string[] content, string valueTag)
      {
         var s = new ConfigLoader().RetrieveAlphaValueFrom(content, valueTag).ToLower();
         var result = _converter.Convert(s); //Runtime.StaggerStrength.Convert(s);

         return result;
      }
      */


      public bool IsOptionActivated(in string[] content, string activeTag)
      {
         return RetrieveAnswerFor(content, activeTag);
      }

      public bool RetrieveAnswerFor(in string[] content, string tag)
      {
         return new ConfigLoader().IsLineExistInStruct(content, tag);
      }
   }
}