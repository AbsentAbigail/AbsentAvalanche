using System;

namespace AbsentAvalanche.Helpers
{
    internal class TargetConstraintHelper
    {
        public static T General<T>(string name = null, Action<T> modification = null, bool not = false) where T : TargetConstraint
        {
            T targetConstraint = ScriptableHelper.CreateScriptable<T>(name ?? typeof(T).Name, modification);
            targetConstraint.not = not;
            return targetConstraint;
        }

        public static TargetConstraintHasStatus HasStatus(string status, Action<TargetConstraintHasStatus> modification = null, bool not = false)
        {
            var targetConstraint = General(not ? "Does Not Have" : "Has" + $" Status {status}", modification, not);
            targetConstraint.status = Absent.TryGet<StatusEffectData>(status);
            return targetConstraint;
        }

        public static TargetConstraintMaxCounterMoreThan MaxCounterMoreThan(int moreThan, Action<TargetConstraintMaxCounterMoreThan> modification = null, bool not = false)
        {
            var targetConstraint = General(not ? "Does Not Have" : "Has" + $" Max Counter More Than {moreThan}", modification, not);
            targetConstraint.moreThan = moreThan;
            return targetConstraint;
        }

        public static TargetConstraintHasAttackEffect HasAttackEffect(string status, Action<TargetConstraintHasAttackEffect> modification = null, bool not = false)
        {
            var targetConstraint = General(not ? "Does Not Have" : "Has" + $" Attack Effect {status}", modification, not);
            targetConstraint.effect = Absent.TryGet<StatusEffectData>(status);
            return targetConstraint;
        }

        public static TargetConstraintHasEffectBasedOn HasEffectBasedOn(string status, Action<TargetConstraintHasEffectBasedOn> modification = null, bool not = false)
        {
            var targetConstraint = General(not ? "Does Not Have" : "Has" + $" Effect Based On {status}", modification, not);
            var type = Absent.TryGet<StatusEffectData>(status).type;
            targetConstraint.basedOnStatusType = type;
            return targetConstraint;
        }

        public static TargetConstraintOr Or(string name, bool not = false, params TargetConstraint[] constraints)
        {
            var targetConstraint = General<TargetConstraintOr>(name: name, not: not);
            targetConstraint.constraints = constraints;
            return targetConstraint;
        }

        public static TargetConstraintAnd And(string name, bool not = false, params TargetConstraint[] constraints)
        {
            var targetConstraint = General<TargetConstraintAnd>(name: name, not: not);
            targetConstraint.constraints = constraints;
            return targetConstraint;
        }
    }
}