﻿using MCM.Abstractions.FluentBuilder.Models;
using MCM.Abstractions.Ref;
using MCM.Abstractions.Settings.Definitions;
using MCM.Abstractions.Settings.Definitions.Wrapper;

using System.Collections.Generic;

namespace MCM.Implementation.FluentBuilder.Models
{
    internal sealed class DefaultSettingsPropertyButtonBuilder :
        BaseDefaultSettingsPropertyBuilder<ISettingsPropertyButtonBuilder>,
        ISettingsPropertyButtonBuilder,
        IPropertyDefinitionButton
    {
        public string Content { get; }

        internal DefaultSettingsPropertyButtonBuilder(string id, string name, IRef @ref, string content)
            : base(id, name, @ref)
        {
            SettingsPropertyBuilder = this;
            Content = content;
        }

        /// <inheritdoc/>
        public override IEnumerable<IPropertyDefinitionBase> GetDefinitions() => new IPropertyDefinitionBase[]
        {
            new PropertyDefinitionButtonWrapper(this),
            new PropertyDefinitionWithIdWrapper(this),
        };
    }
}