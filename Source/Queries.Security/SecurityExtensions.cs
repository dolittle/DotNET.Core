﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.ReadModels;
using Dolittle.Security;

namespace Dolittle.Queries.Security
{
    /// <summary>
    /// Extensions for building a security descriptor specific for dealing with <see cref="IReadModel">read models</see>.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Add an <see cref="Fetching">action</see> to describe the fetching of <see cref="IReadModel">read models</see>.
        /// </summary>
        /// <param name="descriptorBuilder"><see cref="ISecurityDescriptorBuilder"/> to extend.</param>
        /// <returns><see cref="Fetching"/> builder.</returns>
        public static Fetching Fetching(this ISecurityDescriptorBuilder descriptorBuilder)
        {
            var action = new Fetching();
            descriptorBuilder.Descriptor.AddAction(action);
            return action;
        }

        /// <summary>
        /// Add a <see cref="FetchingSecurityTarget"/> to the <see cref="Fetching">action</see>.
        /// </summary>
        /// <param name="action"><see cref="Fetching">Action</see> to add to.</param>
        /// <returns><see cref="FetchingSecurityTarget"/>.</returns>
        public static FetchingSecurityTarget ReadModels(this Fetching action)
        {
            var target = new FetchingSecurityTarget();
            action.AddTarget(target);
            return target;
        }

        /// <summary>
        /// Add a <see cref="NamespaceSecurable"/> to the <see cref="FetchingSecurityTarget"/> for a given namespace.
        /// </summary>
        /// <param name="target"><see cref="FetchingSecurityTarget"/> to add to.</param>
        /// <param name="namespace">Namespace to secure.</param>
        /// <param name="continueWith">Callback for continuing the fluent interface.</param>
        /// <returns><see cref="NamespaceSecurable"/> for the specific namespace.</returns>
        public static FetchingSecurityTarget InNamespace(this FetchingSecurityTarget target, string @namespace, Action<NamespaceSecurable> continueWith)
        {
            var securable = new NamespaceSecurable(@namespace);
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }
    }
}