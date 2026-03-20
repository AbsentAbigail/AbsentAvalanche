#region

using Deadpan.Enums.Engine.Components.Modding;

#endregion

namespace AbsentAvalanche.Helpers;

public static class CardExtensions
{
    public static CardDataBuilder DropsBling(this CardDataBuilder builder, int amount)
    {
        return builder.WithValue(amount * 36);
    }
}