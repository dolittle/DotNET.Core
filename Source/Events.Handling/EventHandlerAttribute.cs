// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Decorates a method to indicate that the method is an Event Processor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAttribute"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> identifier.</param>
        public EventHandlerAttribute(string id)
        {
            Id = Guid.Parse(id);
            ThrowIfIllegalId(Id);
        }

        /// <summary>
        /// Gets the unique id for this event processor.
        /// </summary>
        public EventHandlerId Id { get; }

        void ThrowIfIllegalId(EventHandlerId id)
        {
            var stream = new StreamId { Value = id };
            if (stream.IsNonWriteable) throw new IllegalEventHandlerId(id);
        }
    }
}