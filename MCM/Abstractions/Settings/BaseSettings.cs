﻿using HarmonyLib;

using MCM.Abstractions.Settings.Models;
using MCM.Utils;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MCM.Abstractions.Settings
{
    public abstract class BaseSettings : INotifyPropertyChanged
    {
        public const string SaveTriggered = "SAVE_TRIGGERED";
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        public abstract string Id { get; }
        public abstract string DisplayName { get; }
        public virtual string FolderName { get; } = "";
        public virtual string SubFolder => "";
        public virtual string Format => "json";
        public virtual int UIVersion => 1;
        protected virtual char SubGroupDelimiter => '/';

        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual BaseSettings CreateNew()
        {
            var type = GetType();
            var constructor = AccessTools.Constructor(type, Type.EmptyTypes);
            return constructor != null
                ? (BaseSettings) constructor.Invoke(null)
                : (BaseSettings) FormatterServices.GetUninitializedObject(type);
        }
        protected virtual BaseSettings CopyAsNew()
        {
            var newSettings = CreateNew();
            SettingsUtils.OverrideSettings(newSettings, this);
            return newSettings;
        }
        public virtual IDictionary<string, Func<BaseSettings>> GetAvailablePresets() => new Dictionary<string, Func<BaseSettings>>()
        {
            {"Default", CreateNew}
        };

        public virtual List<SettingsPropertyGroupDefinition> GetSettingPropertyGroups() => GetUnsortedSettingPropertyGroups()
            .OrderByDescending(x => x.GroupName == SettingsPropertyGroupDefinition.DefaultGroupName)
            .ThenByDescending(x => x.Order)
            .ThenByDescending(x => x.DisplayGroupName.ToString(), new AlphanumComparatorFast())
            .ToList();
        protected virtual IEnumerable<SettingsPropertyGroupDefinition> GetUnsortedSettingPropertyGroups()
        {
            var groups = new List<SettingsPropertyGroupDefinition>();
            foreach (var settingProp in SettingsUtils.GetProperties(this))
            {
                //Find the group that the setting property should belong to. This is the default group if no group is specifically set with the SettingPropertyGroup attribute.
                var group = SettingsUtils.GetGroupFor(SubGroupDelimiter, settingProp, groups);
                group.Add(settingProp);
            }
            return groups;
        }
    }
}