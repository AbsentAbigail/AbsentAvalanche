﻿using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Sam() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "sam" + "_flavour";
    private const string Pronouns = "He/Him";
    private static readonly string Flavour = new Cards.Companion.Sam().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Sam.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Sam.Name + Leader<Cards.Companion.Bam>.Suffix), Name]
                ]
            );
    }
}