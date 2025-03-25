namespace APBD;

public class LiquidCon : Container, IHazardNotifier
{
    public bool Dangerous { get; set; }
    public int ContainerNumber { get; private set; }

    public LiquidCon(double loadMass, double height, double ownWeight, double depth, double maxLoadCapacity, bool dangerous)
        : base(loadMass, height, ownWeight, depth, maxLoadCapacity)
    {
        Dangerous = dangerous;
        SerialNumber = $"KON-L-{GetCurrentContainerNumber()}";
        ContainerNumber = GetCurrentContainerNumber();
    }

    public void Notify(string msg)
    {
        Console.WriteLine($"LiquidCon Notify: {msg} , ConNumber: {ContainerNumber}");
    }

    public override void LoadContainer(double newLoadMass)
    {
        double limit = Dangerous ? 0.5 * MaxLoadCapacity : 0.9 * MaxLoadCapacity;

        if (newLoadMass <= limit)
        {
            LoadMass = newLoadMass;
        }
        else
        {
            Notify("Attempted loading beyond permitted limit!");
            throw new Exception("OverfillException: Permitted load capacity exceeded.");
        }
    }

    public override void EmptyContainer()
    {
        LoadMass = 0.0;
    }
    
    public override void PrintInfo()
    {
        string dangerLabel = Dangerous ? "YES" : "NO";
        Console.WriteLine($"[LiquidCon] Serial: {SerialNumber}, Dangerous: {dangerLabel}, Load: {LoadMass}kg, OwnWeight: {OwnWeight}kg");
    }
}