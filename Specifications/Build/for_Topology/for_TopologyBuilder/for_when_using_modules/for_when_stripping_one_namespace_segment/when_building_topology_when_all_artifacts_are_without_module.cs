// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_stripping_one_namespace_segment
{
    public class when_building_topology_when_all_artifacts_are_without_module : given.a_configuration_without_topology_with_valid_artifacts_without_module
    {
        static Exception exception_result;

        Because of = () => exception_result = Catch.Exception(() => topology_builder.Build());

        It should_throw_an_exception = () => exception_result.ShouldNotBeNull();
        It should_throw_InvalidTopology = () => exception_result.ShouldBeOfExactType(typeof(InvalidArtifact));
    }
}