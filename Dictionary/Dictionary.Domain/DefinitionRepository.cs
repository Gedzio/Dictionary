using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictionary.Domain
{
    public class DefinitionRepository
    {
        private readonly TranslationContext _ctx;
        public DefinitionRepository(TranslationContext ctx)
        {
            _ctx = ctx;
        }

        public Maybe<Definition> Get(int id) => _ctx.Definitions
            .Include(x => x.Language)
            .Include(x => x.Translations).ThenInclude(x => x.Language)
            .Include(x => x.Related).ThenInclude(x => x.RelatedDefinition).ThenInclude(x=>x.Related)
            .Include(x => x.Related).ThenInclude(x => x.RelatedDefinition).ThenInclude(x => x.Translations)
            .FirstOrDefault(x=> x.Id == id);
    }
}
