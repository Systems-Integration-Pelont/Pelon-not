namespace apiUsuariosCuenta.services;

public class EncryptService
{
  public string Encrypt(string text)
  {
    string encryptedText = string.Empty;
    byte[] cipherText = System.Text.Encoding.Unicode.GetBytes(text);
    encryptedText = Convert.ToBase64String(cipherText);
    return encryptedText;
  }
}