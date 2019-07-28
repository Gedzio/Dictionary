using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary.Domain
{
    public class AddRelationCommand
    {
        public int Definition1Id { get; set; }
        public int Definition2Id { get; set; }
    }

    public class AddRelationHandler
    {
        private readonly TranslationContext _ctx;

        public AddRelationHandler()
        {
            _ctx = new TranslationContext();
        }

        public Result Handle(AddRelationCommand cmd)
        {
            var repository = new DefinitionRepository(_ctx);
            var def1 = repository.Get(cmd.Definition1Id).ToResult($"Cannot find definition with id: '{cmd.Definition1Id}'");
            var def2 = repository.Get(cmd.Definition2Id).ToResult($"Cannot find definition with id: '{cmd.Definition2Id}'");

            return Result.Combine(def1, def2)
                .OnSuccess(() => def1.Value.AddRelated(def2.Value))
                .OnSuccess(() => _ctx.SaveChanges());
        }
    }
}