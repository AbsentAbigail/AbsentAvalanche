using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbsentAvalanche
{
    public class Absent : WildfrostMod
    {
        public static Absent Instance;

        public Absent(string directory) : base(directory)
        {
            Instance = this;
        }

        public override string GUID => "absentabigail.wildfrost.absentavalanche";

        public override string[] Depends => [];

        public override string Title => "AbsentAvalanche";

        public override string Description => "Avalanche of random concepts";

        private List<object> assets;
        private bool loaded = false;

        public override void Load()
        {
            if (!loaded) CreateModAssets();
            base.Load();
        }

        public override void Unload()
        {
            UnloadFromClasses();
            base.Unload();
        }

        public void CreateModAssets()
        {
            assets = [
                /**
                 * Status Effects
                 */
                new OnCardPlayedGainOverload().Builder(),
                new WhenDestroyedSummonUnboundFlame().Builder(),
                new InstantSummonUnboundFlame().Builder(),
                new SummonUnboundFlame().Builder(),
                new OnCardPlayedApplyOverloadToAlliesInRow().Builder(),

                /**
                 * Keywords
                 */

                /**
                 * Traits
                 */

                /**
                 * Cards (Companions)
                 */
                new FrozenFlame().Builder(),
                new UnboundFlame().Builder(),

                /**
                 * Cards (Clunker)
                 */

                /**
                 * Cards (Items)
                 */

                /**
                 * Card Upgrades
                 */
            ];

            loaded = true;
        }

        public void UnloadFromClasses()
        {
            List<ClassData> tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
            foreach (ClassData tribe in tribes)
            {
                if (tribe == null || tribe.rewardPools == null)
                    continue;

                foreach (RewardPool pool in tribe.rewardPools)
                {
                    if (pool == null)
                        continue;

                    pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == this);
                }
            }
        }

        public override List<T> AddAssets<T, Y>()
        {
            if (assets.OfType<T>().Any())
                LogHelper.Warn($"[{Title}] adding {typeof(Y).Name}s: {assets.OfType<T>().Select(a => a._data.name).Join()}");
            return assets.OfType<T>().ToList();
        }

        public static T TryGet<T>(string name) where T : DataFile
        {
            T data;
            if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
                data = Instance.Get<StatusEffectData>(name) as T;
            else
                data = Instance.Get<T>(name);

            if (data == null)
                throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, Instance)}]");

            return data;
        }

        public static T TryGetOrNull<T>(string name) where T : DataFile
        {
            T data;
            if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
                data = Instance.Get<StatusEffectData>(name) as T;
            else
                data = Instance.Get<T>(name);

            return data;
        }

        public static CardData.StatusEffectStacks SStack(string name, int amount = 1) => new(Instance.Get<StatusEffectData>(name), amount);

        public static CardData.TraitStacks TStack(string name, int amount = 1) => new(Instance.Get<TraitData>(name), amount);

        public static StatusEffectDataBuilder StatusCopy(string oldName, string newName)
        {
            StatusEffectData data = TryGet<StatusEffectData>(oldName).InstantiateKeepName();
            data.name = Instance.GUID + "." + newName;
            StatusEffectDataBuilder builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
            builder.Mod = Instance;
            return builder;
        }
    }
}