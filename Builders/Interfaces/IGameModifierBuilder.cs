#region

using Deadpan.Enums.Engine.Components.Modding;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Interfaces;

[PublicAPI]
public interface IGameModifierBuilder : IBuilder<GameModifierData, GameModifierDataBuilder>;