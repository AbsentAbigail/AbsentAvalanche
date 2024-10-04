using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using System;

namespace AbsentAvalanche.Cards.Items
{
    public abstract class AbstractItem(
            string name, string title,
            int? attack = null, bool needsTarget = false,
            Pools pools = Pools.General,
            int shopPrice = 50,
            Action<CardData> subscribe = null
        ) : AbstractCard(name, title)
    {
        public virtual CardDataBuilder Builder()
        {
            subscribe ??= delegate { };
            return CardHelper.DefaultItemBuilder(name, title, attack, needsTarget, pools, shopPrice)
                .SubscribeToAfterAllBuildEvent(subscribe.Invoke);
        }
    }
}