﻿using MCM.Abstractions.Settings.Properties;

namespace MCM.Abstractions.Settings.Base.PerCampaign
{
    public sealed class EmptyPerCampaignSettings : PerCampaignSettings<EmptyPerCampaignSettings>
    {
        /// <inheritdoc/>
        public override string Id => "empty_percamp_v1";
        /// <inheritdoc/>
        public override string DisplayName => "Empty PerCampaign Settings";
        /// <inheritdoc/>
        public override string Format => "memory";

        /// <inheritdoc/>
        protected override ISettingsPropertyDiscoverer? Discoverer => null;
    }
}