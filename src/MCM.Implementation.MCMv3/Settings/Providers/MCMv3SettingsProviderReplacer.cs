﻿extern alias v3;
extern alias v4;

using MCM.Implementation.MCMv3.Settings.Base;

using System.Collections.Generic;

using TaleWorlds.Core;

using v4::MCM.Abstractions.Settings.Providers;

using MCMv3BaseSettings = v3::MCM.Abstractions.Settings.Base.BaseSettings;
using MCMv3BaseSettingsProvider = v3::MCM.Abstractions.Settings.Providers.BaseSettingsProvider;
using MCMv3SettingsDefinition = v3::MCM.Abstractions.Settings.Models.SettingsDefinition;

namespace MCM.Implementation.MCMv3.Settings.Providers
{
    /// <summary>
    /// Replaces MCMv3's default <see cref="MCMv3BaseSettingsProvider"/> with MCMv4's one.
    /// </summary>
    internal class MCMv3SettingsProviderReplacer : MCMv3BaseSettingsProvider
    {
        private readonly BaseSettingsProvider _settingsProvider;

        public MCMv3SettingsProviderReplacer(BaseSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public override IEnumerable<MCMv3SettingsDefinition> CreateModSettingsDefinitions { get; } = default!;

        public override MCMv3BaseSettings? GetSettings(string id)
        {
            var baseSettings = _settingsProvider.GetSettings(id);
            if (baseSettings is MCMv3GlobalSettingsWrapper settingsWrapper && settingsWrapper.Object is MCMv3BaseSettings settings)
                return settings;
            return null;
        }

        public override void SaveSettings(MCMv3BaseSettings settings) { }
        public override void ResetSettings(MCMv3BaseSettings settings) { }
        public override void OverrideSettings(MCMv3BaseSettings settings) { }

        public override void OnGameStarted(Game game) { }
        public override void OnGameEnded(Game game) { }
    }
}