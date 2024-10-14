using NUnit.Framework;
using CS460_HW1.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace CS460_HW1_Tests
{
    public class UnitTest1Test
    {
        [Test]
        public void TestValidTeamForm()
        {
            var teamForm = new TeamForm
            {
                TeamSize = 5,
                Names = "Alice\nBob\nCharlie\nDavid\nEve"
            };

            var validationResults = ValidateModel(teamForm);
            Assert.IsEmpty(validationResults);
        }

        [Test]
        public void TestInvalidTeamSizeTooSmall()
        {
            var teamForm = new TeamForm
            {
                TeamSize = 1,
                Names = "Alice\nBob\nCharlie\nDavid\nEve"
            };

            var validationResults = ValidateModel(teamForm);
            Assert.IsNotEmpty(validationResults);
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Team size must be between 2 and 10.")));
        }

        [Test]
        public void TestInvalidTeamSizeTooLarge()
        {
            var teamForm = new TeamForm
            {
                TeamSize = 11,
                Names = "Alice\nBob\nCharlie\nDavid\nEve"
            };

            var validationResults = ValidateModel(teamForm);
            Assert.IsNotEmpty(validationResults);
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Team size must be between 2 and 10.")));
        }

        [Test]
        public void TestInvalidNames()
        {
            var teamForm = new TeamForm
            {
                TeamSize = 5,
                Names = "Alice\nBob\nCharlie\nDavid\nEve123"
            };

            var validationResults = ValidateModel(teamForm);
            Assert.IsNotEmpty(validationResults);
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Names can only contain letters, spaces, ,.-_' and must be one per line.")));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}