using Dictionary.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class CreateDefinitionHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDefinition()
        {
            var cmd = new AddDefinitionCommand()
            {
                DefinitionLanguageId = 2,
                TranslationLanguageId = 1,
                DefinitionText = "kitten",
            };
            cmd.Translations.Add("kociak");
            cmd.Translations.Add("kotek");
            var handler = new AddDefinitionHandler();
            var result = handler.Handle(cmd);
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void Get()
        {
            var ctx = new TranslationContext();
            var repo = new DefinitionRepository(ctx);
            var result = repo.Get(2).Value;
            var d2 = repo.Get(3).Value;
            //var result = ctx.Definitions
            //    .Include(x=>x.Language)
            //    .Include(x=>x.Translations).ThenInclude(x=>x.Language).First(x=>x.Language.Id == Language.English.Id);
        }

        [Test]
        public void AddRelated()
        {
            var cmd = new AddRelationCommand()
            {
                Definition1Id =2,
                Definition2Id =3,
            };
            var handler = new AddRelationHandler();
            var result = handler.Handle(cmd);
            result.IsSuccess.Should().BeTrue();
        }
    }
}
