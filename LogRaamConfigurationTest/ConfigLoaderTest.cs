// Code written by Gabriel Mailhot, 09/08/2023.

#region

using FluentAssertions;
using LogRaamConfiguration;
using NUnit.Framework;

#endregion

namespace LogRaamConfigurationTest
{
   [TestFixture]
   public class ConfigLoaderTest
   {
      [Test]
      public void GetOptionFilePath_AssemblyNameShouldBeUsefulToBuildFilePath()
      {
         //Given
         var sut = new ConfigLoader();
         var expectedResult = "LogRaamConfiguration";
         //When
         var result = sut.GetOptionFilePath();
         //Then
         result.Should().Contain(expectedResult);
      }


      [Test]
      public void GivenIHaveALineWithAValueOf10_WhenIWantToRetrieveTheValue_ThenICanRetrieveItByPassingTheMaskingLine()
      {
         //Given
         var options = new[] {
            "Line A = 5",
            "Line B : 10",
            "Line C = 15"
         };
         var lineToFind = "Line B : ";
         var sut = new ConfigLoader();
         var expected = 10;
         //When
         var result = sut.RetrieveValueFrom(options, lineToFind);
         //Then
         result.Should().Be(expected);
      }
   }
}