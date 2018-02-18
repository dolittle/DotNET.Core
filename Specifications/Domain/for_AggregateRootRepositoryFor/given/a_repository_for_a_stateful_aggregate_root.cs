﻿using doLittle.Runtime.Applications;
using doLittle.Commands;
using doLittle.Domain;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor.given
{
    public class a_repository_for_a_stateful_aggregate_root : a_command_context
	{
		protected static AggregateRootRepositoryFor<SimpleStatefulAggregateRoot> repository;
        protected static Mock<IApplicationResourceIdentifier> application_resource_identifier;

        Establish context = () =>
		                    {
								repository = new AggregateRootRepositoryFor<SimpleStatefulAggregateRoot>(
                                    command_context_manager.Object, 
                                    event_store.Object, 
                                    event_source_versions.Object, 
                                    application_resources.Object,
                                    logger.Object);
		                        command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);

                                application_resource_identifier = new Mock<IApplicationResourceIdentifier>();
                                application_resources.Setup(a => a.Identify(typeof(SimpleStatefulAggregateRoot))).Returns(application_resource_identifier.Object);
                            };
	}
}