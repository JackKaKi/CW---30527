namespace APBD;

public class Product
{
    public string Name { get; }
    public double Temperature { get; }

    public Product(string name, double temperature)
    {
        Name = name;
        Temperature = temperature;
    }
}