using System;
using System.ComponentModel.DataAnnotations;

namespace PIClients.API.Helpers.CustomAttributes
{
  public class BirthDateValidation : ValidationAttribute
  {
    private readonly int _acceptedAge;

    public BirthDateValidation(int acceptedAge)
    {
      _acceptedAge = acceptedAge;
    }

    public override bool IsValid(object value)
    {
      bool retValue = true;
      if (CalculateAge((DateTime)value) < _acceptedAge)
        retValue = false;

      return retValue;
    }

    private int CalculateAge(DateTime date)
    {
      int retValue = 0;
      if (date != null)
      {
        retValue = DateTime.Now.Year - date.Year;
        if (DateTime.Now.DayOfYear < date.DayOfYear)
          retValue = retValue - 1;
      }
      return retValue;
    }
  }
}
