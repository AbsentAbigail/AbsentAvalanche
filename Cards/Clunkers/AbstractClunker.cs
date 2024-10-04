using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using System;

namespace AbsentAvalanche.Cards.Clunkers
{
    public abstract class AbstractClunker(
            string name, string title,
            int scrap = 1, int? attack = null, int counter = 0,
            Pools pools = Pools.General,
            Action<CardData> subscribe = null) : AbstractCard(name, title)
    {
        public virtual CardDataBuilder Builder()
        {
            subscribe ??= delegate { };
            return CardHelper.DefaultClunkerBuilder(name, title, scrap, attack, counter, pools)
                .SubscribeToAfterAllBuildEvent(subscribe.Invoke);
        }
    }
}