#region

using AbsentAvalanche.Patches;
using Deadpan.Enums.Engine.Components.Modding;

#endregion

namespace AbsentAvalanche.Builders.Flavours;

public static class AddToFlavour
{
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