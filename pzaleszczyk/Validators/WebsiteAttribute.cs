using pzaleszczyk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pzaleszczyk.Validators
{
    public class WebsiteAttribute : ValidationAttribute
    {
        //Error message
        public string GetErrorMessage() =>
            $"Set correct website address.";

        protected override ValidationResult IsValid(object value,
                   ValidationContext validationContext)
        {
            string website = value.ToString();
            var regex = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
            var match = Regex.Match(website, regex);
            if (!match.Success)
                return new ValidationResult("Podaj adres w poprawnym formacie! http://name.domain/... ");
            else
                return ValidationResult.Success;
        }
    }
}
//https://docs.microsoft.com/pl-pl/aspnet/core/mvc/models/validation?view=aspnetcore-3.1