﻿using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Boozle() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "boozle" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Clunkers.Boozle().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Clunkers.Boozle.Name), Name]
                ]
            );
    }
}