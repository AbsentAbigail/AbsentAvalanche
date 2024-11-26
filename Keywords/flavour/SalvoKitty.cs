﻿using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class SalvoKitty() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "salvokitty" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.SalvoKitty().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.SalvoKitty.Name), Name]
                ]
            );
    }
}