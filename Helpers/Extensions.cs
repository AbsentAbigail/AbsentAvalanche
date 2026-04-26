#region

using AbsentAvalanche.Patches;
using Deadpan.Enums.Engine.Components.Modding;

#endregion

namespace AbsentAvalanche.Helpers;

public static class Extensions
{
    public static CardDataBuilder DropsBling(this CardDataBuilder builder, int amount)
    {
        return builder.WithValue(amount * 36);
    }

    public static object GetCustomDataOrNull(this CardData cardData, string key)
    {
        return cardData.customData?.Get<object>(key, null);
    }

    public static KeywordDataBuilder AddToFlavours(this KeywordDataBuilder keywordDataBuilder, string cardName)
    {
        CardPatches.Flavours =
        [
            ..CardPatches.Flavours,
            [Absent.PrefixGuid(cardName), keywordDataBuilder._data.name],
            [Absent.PrefixGuid(cardName + "Leader"), keywordDataBuilder._data.name]
        ];
        return keywordDataBuilder;
    }
}