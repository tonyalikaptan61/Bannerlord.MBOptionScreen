﻿using HarmonyLib;

using MCM.Abstractions.Settings.Definitions;
using MCM.Abstractions.Settings.Definitions.Wrapper;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MCM.Abstractions.Settings.SettingsProvider
{
    public class SettingsProviderWrapper : BaseSettingsProvider, IWrapper
    {
        private readonly object _object;
        private PropertyInfo? CreateModSettingsDefinitionsProperty { get; }
        private MethodInfo? GetSettingsMethod { get; }
        private MethodInfo? RegisterSettingsMethod { get; }
        private MethodInfo? SaveSettingsMethod { get; }
        private MethodInfo? ResetSettingsMethod { get; }
        private MethodInfo? OverrideSettingsMethod { get; }
        public bool IsCorrect { get; }

        public SettingsProviderWrapper(object @object)
        {
            _object = @object;
            var type = @object.GetType();

            CreateModSettingsDefinitionsProperty = AccessTools.Property(type, nameof(BaseSettingsProvider.CreateModSettingsDefinitions));
            GetSettingsMethod = AccessTools.Method(type, nameof(BaseSettingsProvider.GetSettings));
            RegisterSettingsMethod = AccessTools.Method(type, nameof(BaseSettingsProvider.RegisterSettings));
            SaveSettingsMethod = AccessTools.Method(type, nameof(BaseSettingsProvider.SaveSettings));
            ResetSettingsMethod = AccessTools.Method(type, nameof(BaseSettingsProvider.ResetSettings));
            OverrideSettingsMethod = AccessTools.Method(type, nameof(BaseSettingsProvider.OverrideSettings));

            IsCorrect = GetSettingsMethod != null;
        }

        public override IEnumerable<SettingsDefinition> CreateModSettingsDefinitions =>
            ((IEnumerable<object>) CreateModSettingsDefinitionsProperty?.GetValue(_object)).Select(s => new SettingsDefinitionWrapper(s));
        public override SettingsBase? GetSettings(string id) => GetSettingsMethod?.Invoke(_object, new object[] { id }) is { } settings
                ? settings is SettingsBase settingsBase ? settingsBase : new SettingsWrapper(settings)
                : default;
        public override void RegisterSettings(SettingsBase settings) =>
            RegisterSettingsMethod?.Invoke(_object, new object[] { settings is SettingsWrapper wrapper ? wrapper.Object : settings });
        public override void SaveSettings(SettingsBase settings) =>
            SaveSettingsMethod?.Invoke(_object, new object[] { settings is SettingsWrapper wrapper ? wrapper.Object : settings });
        public override SettingsBase? ResetSettings(string id) => ResetSettingsMethod?.Invoke(_object, new object[] { id }) is { } settings
                ? settings is SettingsBase settingsBase ? settingsBase : new SettingsWrapper(settings)
                : default;
        public override void OverrideSettings(SettingsBase settings) =>
            OverrideSettingsMethod?.Invoke(_object, new object[] { settings is SettingsWrapper wrapper ? wrapper.Object : settings });
    }
}