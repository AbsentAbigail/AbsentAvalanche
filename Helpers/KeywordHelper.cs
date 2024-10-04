using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace AbsentAvalanche.Helpers
{
    internal class KeywordHelper
    {
        public static Color ORANGE = Color(255, 202, 87);
        public static Color WHITE = Color(255, 255, 255);
        public static Color GRAY = Color(166, 166, 166);

        public static KeywordDataBuilder Keyword(string name, string title, string description, Color? titleColour = null, Color? bodyColor = null, Color? noteColor = null)
        {
            return new KeywordDataBuilder(Absent.Instance)
                .Create(name)
                .WithTitle(title)
                .WithTitleColour(titleColour ?? ORANGE)
                .WithShowName(true)
                .WithDescription(description)
                .WithBodyColour(bodyColor ?? WHITE)
                .WithNoteColour(noteColor ?? GRAY);
        }

        public static Color Color(int r, int b, int g)
        {
            Color color = new(
                r / 255F,
                b / 255F,
                g / 255F
            );

            return color;
        }

        public static string Tag(string name)
        {
            return $"<keyword={Absent.Instance.GUID}.{name}>";
        }
    }
}