﻿extern alias v4;

using v4::MCM.Abstractions.Settings.Containers.Global;

namespace MCM.Implementation.MCMv3.Settings.Containers
{
    /// <summary>
    /// So it can be overriden by an external library
    /// </summary>
    public interface IMCMv3GlobalSettingsContainer : IGlobalSettingsContainer { }
}