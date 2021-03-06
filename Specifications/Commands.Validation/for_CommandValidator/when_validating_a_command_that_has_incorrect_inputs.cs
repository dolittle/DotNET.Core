﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Dynamic;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Validation.for_CommandValidator
{
    public class when_validating_a_command_that_has_incorrect_inputs : given.a_command_validation_service
    {
        static IEnumerable<ValidationResult> input_validation_errors;
        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;
        static Mock<ICommandInputValidator> command_input_validator;
        static Mock<ICommandBusinessValidator> command_business_validator;

        Establish context = () =>
        {
            input_validation_errors = new List<ValidationResult>()
            {
            new ValidationResult("first failed input", new[] { "AProperty" }),
            new ValidationResult("second failed input", new[] { "AnotherProperty" })
            };

            var artifact = Artifact.New();
            command = new CommandRequest(CorrelationId.Empty, artifact.Id, artifact.Generation, new ExpandoObject());
            command_instance = Mock.Of<ICommand>();
            command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

            command_input_validator = new Mock<ICommandInputValidator>();
            command_business_validator = new Mock<ICommandBusinessValidator>();

            command_input_validator.Setup(iv => iv.ValidateFor(command_instance)).Returns(input_validation_errors);

            command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_instance)).Returns(command_input_validator.Object);
        };

        Because of = () => result = command_validator.Validate(command);

        It should_have_failed_validations = () => result.ValidationResults.ShouldNotBeEmpty();
        It should_have_all_the_failed_input_validations = () => result.ValidationResults.ShouldContainOnly(input_validation_errors);
        It should_not_have_validated_the_command_business_rules = () => command_business_validator.VerifyAll();
    }
}