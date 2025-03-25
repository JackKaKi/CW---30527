using System.Runtime.InteropServices.JavaScript;

namespace APBD;

public class RefrigeratedCon : Container
{
    public Product StoredProduct { get; private set; }
    public double Temperature { get; private set; }

    public RefrigeratedCon(double loadMass, double height, double ownWeight, double depth, double maxLoadCapacity, Product product, double temperature)
        : base(loadMass, height, ownWeight, depth, maxLoadCapacity)
    {
        if (temperature < product.Temperature)
        {
            throw new ArgumentException($"Temperature too low ({temperature}°C) for product :  {product.Name} ({product.Temperature}°C).");
        }

        StoredProduct = product;
        Temperature = temperature;
        SerialNumber = $"KON-C-{GetCurrentContainerNumber()}";
    }
    
    public override void LoadContainer(double loadCapacity)
    {
        if (loadCapacity > MaxLoadCapacity)
        {
            throw new Exception("OverfillException: Load exceeds maximum capacity.");
        }

        LoadMass = loadCapacity;
    }
    public void LoadProduct(Product product, double loadCapacity)
    {
        if (product.Name != StoredProduct.Name)
            throw new InvalidOperationException("Inny typ produktu");

        if (Temperature < product.Temperature)
            throw new InvalidOperationException("Zbyt niska temperatura");

        if (LoadMass + loadCapacity > MaxLoadCapacity)
            throw new Exception("OverfillException");

        LoadMass += loadCapacity;
    }

    public override void EmptyContainer()
    {
        LoadMass = 0.0;
    }
    public override void PrintInfo()
    {
        Console.WriteLine($"[RefrigeratedCon] Serial: {SerialNumber}, Product: {StoredProduct.Name}, Temp: {Temperature}°C, Load: {LoadMass}kg");
    }
}