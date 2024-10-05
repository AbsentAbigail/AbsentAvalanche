using Deadpan.Enums.Engine.Components.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsentAvalanche.StatusEffects
{
    internal class TriggerAgainstTargetWhenMissileAttacks() : AbstractStatus<StatusEffectApplyXWhenAlliesAttack>(
        Name, "Trigger against the target when a {0} is played",
        true,
        subscribe: status =>
        {
            
        })
    {
        public const string Name = "Trigger Against Target When Missile Attacks";
    }
}
