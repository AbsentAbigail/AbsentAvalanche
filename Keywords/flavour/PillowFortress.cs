﻿using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class PillowFortress() : AbstractKeyword(Name, "", " |" + Flavour)
{
    public const string Name = "pillowfortress" + "_flavour";
    private static readonly string Flavour = new Cards.Clunkers.PillowFortress().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Clunkers.PillowFortress.Name), Name]
                ]
            );
    }
}