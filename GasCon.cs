namespace APBD;

public class GasCon : Container, IHazardNotifier
{
    public double Pressure { get; set; }
    public int ContainerNumber { get; private set; }

    
    public GasCon(double loadMass, double height, double ownWeight, double depth, double maxLoadCapacity, double pressure)
        : base(loadMass, height, ownWeight, depth, maxLoadCapacity)
    {
        Pressure = pressure;
        SerialNumber = $"KON-G-{GetCurrentContainerNumber()}";
        ContainerNumber = GetCurrentContainerNumber();
    }

    public override void EmptyContainer()
    {
        LoadMass = 0.05 * LoadMass;
    }

    public override void LoadContainer(double loadCapacity)
    {
        if (loadCapacity > MaxLoadCapacity)
        {
            Notify("Attempt to load above maximum capacity - rejected.");
            throw new Exception("OverfillException");
        }

        LoadMass = loadCapacity;
    }
    
    public void Notify(string msg)
    {
        Console.WriteLine($"GasCon Notify: {msg} , ConNumber: {ContainerNumber}");
    }
    
    public override void PrintInfo()
    {
        Console.WriteLine($"[GasCon] Serial: {SerialNumber}, Pressure: {Pressure} atm, Load: {LoadMass}kg, OwnWeight: {OwnWeight}kg");
    }
}