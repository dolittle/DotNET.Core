// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.for_BusinessValidator.given
{
    public class ChildValidator : BusinessValidator<Child>
    {
        public ChildValidator()
        {
            RuleFor(c => c.ChildConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(c => c.ChildSimpleStringProperty)
                .NotEmpty();
            RuleFor(c => c.ChildSimpleIntegerProperty)
                .LessThan(10);
            RuleFor(c => c.Grandchild)
                .SetValidator(new GrandchildValidator());
        }
    }
}