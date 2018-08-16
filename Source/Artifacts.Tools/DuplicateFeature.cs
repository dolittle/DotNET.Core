/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal class DuplicateFeature : Exception
    {
        internal DuplicateFeature(FeatureDefinition feature)
            : base($"Duplicate feature with id = {feature.Feature.Value} and name = {feature.Name} was found")
        {
        }
    }
}