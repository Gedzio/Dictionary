using Dictionary.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDefinition()
        {
            var translation1 = Translation.Create(Language.Polish, "Kot").Value;
            var translation2 = Translation.Create(Language.Polish, "Kotka").Value;
            var translation3 = Translation.Create(Language.Polish, "Kociątko").Value;

            var definitionResult = Definition.Create(Language.English, "Cat", translation1, translation2,translation3);

            definitionResult.IsSuccess.Should().Be(true);
            definitionResult.Value.Translations.Count().Should().Be(3);
        }

        [Test]
        public void AddRelated()
        {
            var translation1 = Translation.Create(Language.Polish, "Kot").Value;
            var definition = Definition.Create(Language.English, "Cat", translation1).Value;

            var translation2 = Translation.Create(Language.Polish, "Kotek").Value;
            var def2 = Definition.Create(Language.English, "Kitten", translation2).Value;

            definition.AddRelated(def2);

            definition.Related.Count().Should().Be(1);
            //def2.Related.Count().Should().Be(1);
        }
    }
}