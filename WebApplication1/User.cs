using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public string KullaniciAdi { get; set; }
    public string Sifre { get; set; }
}