﻿using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Catcus() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "catcus" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.Catcus().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Catcus.Name), Name]
                ]
            );
    }
}