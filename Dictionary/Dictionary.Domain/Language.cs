using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary.Domain
{
    public class Language : EntityBase, IReadOnlyEntity
    {
        public string Name { get; private set; }
        public static readonly Language Polish = new Language(1, "Polish");
        public static readonly Language English = new Language(2, "English");

        private static List<Language> All => new List<Language>() { Polish, English };
        private Language(int id, string name) : base(id)
        {
            Name = name;
        }

        internal static Result<Language> Get(int translationLanguageId)
        {
            var result = All.SingleOrDefault(x => x.Id == translationLanguageId);
            if (result == null)
                return Result.Fail<Language>($"There is no language with id: '{translationLanguageId}'.");

            return Result.Ok(result);
        }
    }
}