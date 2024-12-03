using System.ComponentModel.DataAnnotations;

namespace Shop.Utils;

public class ValidEmailDomainAttribute: ValidationAttribute
{
    private readonly string ALLOWEDDOMAIN;

    public ValidEmailDomainAttribute(string allowed)
    {
        ALLOWEDDOMAIN = allowed;
    }


}
