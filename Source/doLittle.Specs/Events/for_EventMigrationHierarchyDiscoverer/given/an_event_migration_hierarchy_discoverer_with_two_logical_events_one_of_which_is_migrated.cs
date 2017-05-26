﻿using Bifrost.Events;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using System;
using Bifrost.Specs.Events.Fakes;

namespace Bifrost.Specs.Events.for_EventMigrationLevelDiscoverer.given
{
    public class an_event_migration_hierarchy_discoverer_with_two_logical_events_one_of_which_is_migrated
    {
        protected static EventMigrationHierarchyDiscoverer event_migration_level_discoverer;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Type[] event_types;

        Establish context = () =>
                                {
                                    event_types = new []
                                                      {
                                                         typeof(AnotherSimpleEvent),
                                                         typeof(SimpleEvent),
                                                         typeof(Fakes.v2.SimpleEvent),
                                                         typeof(Fakes.v3.SimpleEvent)
                                                      };

                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    type_discoverer_mock.Setup(d => d.FindMultiple<IEvent>()).Returns(event_types);
                                    event_migration_level_discoverer = new EventMigrationHierarchyDiscoverer(type_discoverer_mock.Object);
                                };


    }
}