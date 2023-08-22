// Code written by Gabriel Mailhot, 09/08/2023.

#region

using System;
using System.IO;
using System.Reflection;

#endregion

namespace LogRaamConfiguration
{
   public class ConfigLoader : IConfigurationLoader
   {
      public string GetOptionFilePath()
      {
         var currentDirectory = Directory.GetCurrentDirectory();
         var parentDirectory = Directory.GetParent(currentDirectory)?.Parent;

         if (parentDirectory == null) return string.Empty;

         var filePath = Path.Combine(parentDirectory.FullName, "Modules", Assembly.GetCallingAssembly().GetName().Name, "OPTIONS.txt");

         return filePath;
      }

      public bool IsLineExistInStruct(in string[] content, string lineToFind)
      {
         return Array.Exists(content, option => option.Contains(lineToFind));
      }

      public string RetrieveAlphaValueFrom(in string[] content, string lineToFind)
      {
         var p = Array.Find(content, line => line.Contains(lineToFind));

         if (p == null) return string.Empty;

         var colonIndex = p.IndexOf(':');
         if (colonIndex == -1 || colonIndex >= p.Length - 2) colonIndex = p.IndexOf('=');

         if (colonIndex == -1 || colonIndex >= p.Length - 2) return string.Empty;

         var valueAfterColon = p.Substring(colonIndex + 2).Trim();

         return valueAfterColon;
      }


      public string[] RetrieveConfigDetails(string filePath)
      {
         return !File.Exists(filePath)
            ? new string[] { }
            : File.ReadAllLines(filePath);
      }

      public float RetrieveFloatValueFrom(in string[] content, string lineToFind)
      {
         var p = Array.Find(content, line => line.Contains(lineToFind));

         if (p == null) return 0;

         var valueStartIndex = -1;

         if ((valueStartIndex = p.IndexOf('=')) == -1 && (valueStartIndex = p.IndexOf(':')) == -1) return 0;

         valueStartIndex++;

         if (valueStartIndex >= p.Length) return 0;

         var valueSubstring = p.Substring(valueStartIndex);

         return float.TryParse(valueSubstring, out var result)
            ? result
            : 0;
      }

      public int RetrieveIntegerValueFrom(in string[] content, string lineToFind)
      {
         var p = Array.Find(content, line => line.Contains(lineToFind));

         if (p == null) return 0;

         var valueStartIndex = -1;

         if ((valueStartIndex = p.IndexOf('=')) == -1 && (valueStartIndex = p.IndexOf(':')) == -1) return 0;

         valueStartIndex++;

         if (valueStartIndex >= p.Length) return 0;

         var valueSubstring = p.Substring(valueStartIndex);

         return int.TryParse(valueSubstring, out var result)
            ? result
            : 0;
      }
   }
}