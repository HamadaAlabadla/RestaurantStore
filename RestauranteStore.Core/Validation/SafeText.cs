using Microsoft.Security.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Web;


namespace RestaurantStore.Core.Validation
{
    public class SafeText : ValidationAttribute
    {
        public string? Text { get; set; }

        public string GetErrorMessage()
        {
            Text = HttpUtility.HtmlEncode(Text);

            return $"This Text '#{Text}#' Not Safe !!!";
        }

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            if (value != null && value is string stringValue)
            {
                Text = stringValue;
                var safeText = Sanitizer.GetSafeHtmlFragment(stringValue);
                var htmlEncoder = HtmlEncoder.Default;
                var jsEncoder = JavaScriptEncoder.Default;
                var urlEncoder = UrlEncoder.Default;

                safeText = htmlEncoder.Encode(safeText);
                safeText = jsEncoder.Encode(safeText);
                safeText = urlEncoder.Encode(safeText);
                safeText = safeText.Replace(urlEncoder.Encode(" "), " ");
                value = safeText;
                if (!CheckHtml(safeText)
                    || !CheckScript(safeText)
                    || !CheckSql(safeText)
                    || !safeText.Equals(stringValue))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            // 


            return ValidationResult.Success;
        }

        private bool CheckScript(string text)
        {
            if (text.Contains("<script", StringComparison.OrdinalIgnoreCase)
                || text.Contains("javascript:", StringComparison.OrdinalIgnoreCase)
                || text.Contains("onload=", StringComparison.OrdinalIgnoreCase))
            {
                // Potentially malicious JavaScript found <> 
                return false;
            }
            return true;
        }

        private bool CheckHtml(string text)
        {
            if (text.Contains("<", StringComparison.OrdinalIgnoreCase)
                || text.Contains(">", StringComparison.OrdinalIgnoreCase))
            {
                // Potentially malicious HTML found
                return false;
            }
            return true;
        }

        private bool CheckSql(string text)
        {
            string[] sqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "--", ";" };
            foreach (string keyword in sqlKeywords)
            {
                if (text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    // Potentially malicious SQL injection found
                    return false;
                }
            }
            return true;
        }
    }
}
