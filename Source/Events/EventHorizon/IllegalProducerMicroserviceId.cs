// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Applications;

namespace Dolittle.Events.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when the producer microservice id is an illegal value.
    /// </summary>
    public class IllegalProducerMicroserviceId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalProducerMicroserviceId"/> class.
        /// </summary>
        /// <param name="eventType">The <see cref="Type" /> of the <see cref="IExternalEvent" />.</param>
        /// <param name="microserviceId">The illegal <see cref="Microservice" />.</param>
        /// <param name="reason">The <see cref="IllegalProducerMicroserviceIdReason" />.</param>
        public IllegalProducerMicroserviceId(Type eventType, Microservice microserviceId, IllegalProducerMicroserviceIdReason reason)
            : base($"The microservice id '{microserviceId}' on the event '{eventType.FullName}' is an illegal producer microservice id. {reason}")
        {
        }
    }
}