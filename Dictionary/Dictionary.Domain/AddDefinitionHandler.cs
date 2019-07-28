using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary.Domain
{
    public class AddDefinitionCommand
    {
        public int DefinitionLanguageId { get; set; }
        public int TranslationLanguageId { get; set; }
        public string DefinitionText { get; set; }
        public List<string> Translations { get; set; }
        public AddDefinitionCommand()
        {
            Translations = new List<string>();
        }
    }

    public class AddDefinitionHandler
    {
        private readonly TranslationContext _ctx;

        public AddDefinitionHandler()
        {
            ////
            //var connectionString = "Data Source=(local);Initial Catalog=BOSS_SCHOOLS_SHARED;Connection Timeout=3600;Integrated Security=False;User ID=BcSchoolsUser;password=P@ssw0rd;MultipleActiveResultSets=True;Timeout=200;";
            //var dbContextOptions = new DbContextOptionsBuilder()
            //    //.UseLazyLoadingProxies()
            //    .UseSqlServer(connectionString)
            //    .Options;

            _ctx = new TranslationContext();
        }

        public Result Handle(AddDefinitionCommand cmd)
        {
            var translationLanguageResult = Language.Get(cmd.TranslationLanguageId);
            var definitionLanguageResult = Language.Get(cmd.DefinitionLanguageId);

            return Result.Combine(translationLanguageResult, definitionLanguageResult)
                .OnSuccess(() =>
                {
                    var translationsResults = cmd.Translations.Select(x => Translation.Create(translationLanguageResult.Value, x));
                    return Result.Combine(translationsResults.ToArray())
                        .OnSuccess(() => Definition.Create(definitionLanguageResult.Value, cmd.DefinitionText, translationsResults.Select(x => x.Value).ToArray()));
                })
                .OnSuccess(definition => _ctx.Definitions.Add(definition))
                .OnSuccess(definition => _ctx.SaveChanges());
        }
    }
}