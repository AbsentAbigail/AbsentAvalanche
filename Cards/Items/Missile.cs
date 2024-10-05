using AbsentAvalanche.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsentAvalanche.Cards.Items
{
    internal class Missile() : AbstractItem(
        Name, "Missile",
        1, true,
        Pools.None,
        subscribe: card =>
        {
            card.traits = [Absent.TStack("Consume")];
        })
    {
        public const string Name = "Missile";
    }
}
