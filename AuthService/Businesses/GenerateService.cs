using AuthService.Interfaces;

namespace AuthService.Bussenes;

public class GenerateService:IGenerateService
{
    public string GenerateUniqueCode()
    {
        return DateTime.Now.ToString("yyMMddhhmmssfffffff");
    }

    public string GenerateUniqueCode(int length)
    { 
        var uniqueCode =  GenerateUniqueCode();
        return uniqueCode.Length > length ? uniqueCode.Substring(uniqueCode.Length - length) : uniqueCode;
    }
}