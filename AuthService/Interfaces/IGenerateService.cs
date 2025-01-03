namespace AuthService.Interfaces;

public interface IGenerateService
{
    /// <summary>
    /// Kod üreten method
    /// </summary>
    /// <returns></returns>
    string GenerateUniqueCode();
    /// <summary>
    /// kod üreten method
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    string GenerateUniqueCode(int length);
}