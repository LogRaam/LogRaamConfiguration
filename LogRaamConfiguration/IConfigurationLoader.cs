// Code written by Gabriel Mailhot, 09/08/2023.

namespace LogRaamConfiguration
{
   public interface IConfigurationLoader
   {
      string GetOptionFilePath();
      bool IsLineExistInStruct(in string[] options, string lineToFind);
      string RetrieveAlphaValueFrom(in string[] options, string lineToFind);
      string[] RetrieveConfigDetails(string filePath);
      int RetrieveIntegerValueFrom(in string[] options, string lineToFind);
   }
}