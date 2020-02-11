// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events.Processing;
using Dolittle.Logging;

namespace EventSourcing
{
    [EventHandler("38eeb77f-90ca-405c-a733-3b0e6f0b0ef3")]
    public class MyEventProcessors : ICanHandleEvents
    {
        readonly ILogger _logger;

        public MyEventProcessors(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(MyEvent @event)
        {
            _logger.Information($"Processing event : '{@event}'");
        }
    }
}