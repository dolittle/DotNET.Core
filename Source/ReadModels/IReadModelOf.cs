﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;

namespace Dolittle.ReadModels
{
    /// <summary>
    /// Defines a way of retrieving a single <see cref="IReadModel"/>.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IReadModel"/> it retrieves.</typeparam>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered and invoked when a read model of
    /// type <typeparamref name="T"/> is retrieved.
    /// In most cases it is possible to use the provided <see cref="ReadModelOf{T}"/> by implementing a <see cref="IReadModelRepositoryFor{T}"/>.
    /// </remarks>
    public interface IReadModelOf<T>
        where T : IReadModel
    {
        /// <summary>
        /// Filter by properties.
        /// </summary>
        /// <param name="propertyExpressions">Property filter expressions to use.</param>
        /// <returns>An instance or default/null of the <see cref="IReadModel"/>, throws an exception if there is not a unique match.</returns>
        T InstanceMatching(params Expression<Func<T, bool>>[] propertyExpressions);
    }
}
