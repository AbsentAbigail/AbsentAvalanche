#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

#endregion

namespace AbsentAvalanche.Builders.Icons;

[UsedImplicitly]
public class Abduct : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "abduct",
                Absent.Instance.ImagePath("Icons/abduct.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.counter)
            .WithTextboxSprite()
            .WithKeywords(Keywords.Abduct.Name);
    }
}