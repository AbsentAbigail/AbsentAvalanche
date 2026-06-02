using Deadpan.Enums.Engine.Components.Modding;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Interfaces;

[PublicAPI]
public interface IGameModifierBuilder : IBuilder<GameModifierData, GameModifierDataBuilder>;