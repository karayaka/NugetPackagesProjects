namespace AuthService.Interfaces;

public interface IEncryptionService
{
    string DencryptionValue(string value);
    string EncryptionValue(string value);
}