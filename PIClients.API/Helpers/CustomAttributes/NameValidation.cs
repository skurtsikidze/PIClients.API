using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PIClients.API.Helpers.CustomAttributes
{
  public class NameValidation : ValidationAttribute
  {
    private string _validCharacters = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
    public override bool IsValid(object value)
    {
      bool retValue = true;
      bool geoIsValid = true;
      string name = value.ToString();
      
      foreach (char c in name)
      {
        if (!_validCharacters.Contains(c.ToString()))
        {
          geoIsValid = false;
          break;
        }
      }

      if (!Regex.IsMatch(name, @"^[a-zA-Z]+$") && !geoIsValid)
      {
        retValue = false;
      }

      return retValue;
    }
  }
}
