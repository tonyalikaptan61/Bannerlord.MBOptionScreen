﻿using MCM.Abstractions;
using MCM.Abstractions.Settings.SettingsContainer;

namespace MCM.Implementation.ModLib.Settings.SettingsContainer
{
    public sealed class ModLibSettingsContainerWrapper : SettingsContainerWrapper, IModLibSettingsContainer, IWrapper
    {
        public ModLibSettingsContainerWrapper(object @object) : base(@object) { }
    }
}