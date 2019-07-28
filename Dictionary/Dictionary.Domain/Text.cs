using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary.Domain
{
    
    public class DefinitionsRelation : EntityBase
    {
        //TODO: Change primary key of this entity
        public Definition RelatedDefinition { get; private set; }

        public DefinitionsRelation(Definition definition)
        {
            RelatedDefinition = definition;
        }
        private DefinitionsRelation()
        {

        }
    }

    public abstract class TextBase : EntityBase
    {
        public Language Language { get; private set; }
        public string Text { get; private set; }

        protected TextBase(Language language, string value)
        {
            Language = language;
            Text = value;
        }

        protected TextBase()
        {
        }

        protected static Result EnsureTextIsValid(string text)
        {
            return Result.Create(!string.IsNullOrEmpty(text), "Text must be specified");
        }
    }

    public class Definition : TextBase
    {
        private Definition()
        {

        }
        private Definition(Language language, string text, List<Translation> translations) : base(language, text)
        {
            _related = new List<DefinitionsRelation>();
            _translations = new List<Translation>();
            _translations = translations;
        }

        public static Result<Definition> Create(Language language, string text, params Translation[] translations)
        {
            return Result.Combine(
                    EnsureTextIsValid(text),
                    Result.Create(translations.Any(), "At least one translation must be specified."))
                .OnSuccess(() => new Definition(language, text, translations.ToList()));
        }

        private List<Translation> _translations;
        public IReadOnlyList<Translation> Translations => _translations.ToList();

        private List<DefinitionsRelation> _related;
        public IReadOnlyList<DefinitionsRelation> Related => _related.ToList();


        public void AddTranslation(Translation translation)
        {
            _translations.Add(translation);
        }

        public void AddRelated(Definition related)
        {
            var relatedDefinition = new DefinitionsRelation(related);
            _related.Add(relatedDefinition);
            //TODO: Fix issue with two way relationships
            //related.AddRelated(this);
        }
    }

    public class Translation : TextBase
    {
        private Translation()
        {
        }

        private Translation(Language language, string text) : base(language, text)
        {
        }

        public static Result<Translation> Create(Language language, string text)
        {
            return EnsureTextIsValid(text)
                .OnSuccess(() => new Translation(language, text));
        }
    }
}