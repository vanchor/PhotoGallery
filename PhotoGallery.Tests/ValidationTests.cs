using PhotoGallery.Models.UserDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Tests
{
    public class ValidationTests
    {

        [Theory]
        [InlineData(null, null, null, false)]
        [InlineData(null, "test@gmail.com", null, false)]
        [InlineData(null, null, "qwery", false)]
        [InlineData(null, "test@gmail.com", "qwery", false)]
        [InlineData("TestName", null, null, false)]
        [InlineData("TestName", "test@gmail.com", null, false)]
        [InlineData("TestName", null, "qwery", false)]
        [InlineData("TestName", "test@gmail.com", "qwery", true)]
        [InlineData("TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest",
            "test@gmail.com",
            "qwer", false)]
        [InlineData("TestName", "test", "qwer", false)]
        public void TestModelValidation(string? Username, string? Email, string? Password, bool isValid)
        {
            var user = new UserModel()
            {
                Username = Username,
                Email = Email,
                Password = Password
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
    }
}
