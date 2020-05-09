﻿using MCM.Abstractions.Settings.Definitions;

namespace MCM.Implementation.ModLib.Attributes
{
    public sealed class ModLibPropertyGroupDefinitionWrapper : IPropertyGroupDefinition
    {
        public string GroupName { get; }
        public bool IsMainToggle { get; }
        public int GroupOrder { get; }

        public ModLibPropertyGroupDefinitionWrapper(object @object)
        {
            var type = @object.GetType();

            GroupName = type.GetProperty(nameof(GroupName))?.GetValue(@object) as string ?? "ERROR";
            IsMainToggle = type.GetProperty(nameof(IsMainToggle))?.GetValue(@object) as bool? ?? false;
            GroupOrder = -1;
        }
    }
}