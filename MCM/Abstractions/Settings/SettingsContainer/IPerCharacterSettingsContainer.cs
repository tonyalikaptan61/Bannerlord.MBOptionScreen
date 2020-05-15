﻿using TaleWorlds.Core;

namespace MCM.Abstractions.Settings.SettingsContainer
{
    public interface IPerCharacterSettingsContainer : ISettingsContainer, IDependencyBase
    {
        void OnGameStarted(Game game);
        void OnGameEnded(Game game);
    }
}