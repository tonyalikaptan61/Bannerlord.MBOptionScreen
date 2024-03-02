﻿namespace MCM.Abstractions.PerSave
{
#if !BANNERLORDMCM_PUBLIC
    internal
#else
    public
# endif
    interface IPerSaveSettingsContainer :
        ISettingsContainer,
        ISettingsContainerHasSettingsDefinitions,
        ISettingsContainerCanOverride,
        ISettingsContainerCanReset,
        ISettingsContainerPresets,
        ISettingsContainerHasUnavailable,
        ISettingsContainerHasSettingsPack,
        ISettingsContainerCanInvalidateCache
    {
        void LoadSettings();
    }
}