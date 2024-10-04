using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords
{
    internal abstract class AbstractKeyword
    {
        public static AbstractKeyword i;
        public string Tag;
        private readonly string name;
        private readonly string title;
        private readonly string description;

        public AbstractKeyword(string name, string title, string description)
        {
            i = this;
            Tag = KeywordHelper.Tag(name);
            this.name = name;
            this.title = title;
            this.description = description;
        }

        public virtual KeywordDataBuilder Builder()
        {
            return KeywordHelper.Keyword(name, title, description);
        }
    }
}