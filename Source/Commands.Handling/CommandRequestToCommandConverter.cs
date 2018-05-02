﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Runtime.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandRequestToCommandConverter"/>
    /// </summary>
    public class CommandRequestToCommandConverter : ICommandRequestToCommandConverter
    {
        IApplicationArtifactResolver _applicationArtifactResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationArtifactResolver"></param>
        public CommandRequestToCommandConverter(IApplicationArtifactResolver applicationArtifactResolver)
        {
            _applicationArtifactResolver = applicationArtifactResolver;
        }

        /// <inheritdoc/>
        public ICommand Convert(CommandRequest request)
        {
            // todo: Cache it per transaction / command context 

            var type = _applicationArtifactResolver.Resolve(request.Type);

            // todo: Verify that it is a an ICommand
            var instance = Activator.CreateInstance(type) as ICommand;

            // todo: Verify that the command shape matches 100% - do not allow anything else
            var properties = type.GetProperties().ToDictionary(p => p.Name.ToLowerInvariant(), p => p);

            CopyPropertiesFromRequestToCommand(request, instance, properties);

            return instance;
        }

        void CopyPropertiesFromRequestToCommand(CommandRequest request, ICommand instance, Dictionary<string, PropertyInfo> properties)
        {
            request.Content.Keys.ForEach(propertyName =>
            {
                var lowerCasedPropertyName = propertyName.ToLowerInvariant();
                if (properties.ContainsKey(lowerCasedPropertyName))
                {
                    var property = properties[lowerCasedPropertyName];
                    object value = request.Content[propertyName];

                    value = HandleValue(property.PropertyType, value);
                    property.SetValue(instance, value);
                }
            });
        }

        object HandleValue(Type targetType, object value)
        {
            if (value is JArray ||  value is JObject)
            {
                value = JsonConvert.DeserializeObject(value.ToString(), targetType);
            }
            else if (targetType.IsConcept())
            {
                value = ConceptFactory.CreateConceptInstance(targetType, value);
            }
            else if (targetType == typeof(DateTimeOffset) && value.GetType() == typeof(DateTime))
            {
                value = new DateTimeOffset((DateTime) value);
            }
            else if (targetType.IsEnum)
            {
                value = Enum.Parse(targetType, value.ToString());
            }
            else if (targetType == typeof(Guid))
            {
                value = Guid.Parse(value.ToString());
            }
            else
            {
                if (!targetType.IsAssignableFrom(value.GetType()))
                    value = System.Convert.ChangeType(value, targetType);
            }

            return value;
        }
    }
}