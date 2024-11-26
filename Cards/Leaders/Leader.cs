using System;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Leaders;

public class Leader<T>(
    int healthModMin = 0,
    int healthModMax = 0,
    int damageModMin = 0,
    int damageModMax = 0,
    int counterModMin = 0,
    int counterModMax = 0,
    Action<CardData> subscribe = null
) where T : AbstractCompanion, new()
{
    public const string Suffix = "Leader";
    private readonly Action<CardData> _subscribe = subscribe ?? delegate { };
    
    public CardDataBuilder Builder()
    {
        var companion = new T();
        return companion.Builder()
            .Build().Edit<CardData, CardDataBuilder>()
            .WithCardType("Leader")
            .FreeModify(card =>
            {
                card.name += Suffix;
                card.createScripts =
                [
                    LeaderHelper.GiveUpgrade(),
                    LeaderHelper.AddRandomHealth(healthModMin, healthModMax),
                    LeaderHelper.AddRandomDamage(damageModMin, damageModMax),
                    LeaderHelper.AddRandomCounter(counterModMin, counterModMax)
                ];
            })
            .SubscribeToAfterAllBuildEvent(companion.Subscribe.Invoke)
            .SubscribeToAfterAllBuildEvent(_subscribe.Invoke);
    }
}