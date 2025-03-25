// See https://aka.ms/new-console-template for more information

public abstract class Container
{
    private static int counter = 0;

    public string SerialNumber { get; protected set; }

    public double LoadMass { get; set; }
    public double Height { get; set; }
    public double OwnWeight { get; set; }
    public double Depth { get; set; }
    public double MaxLoadCapacity { get; set; }
    public int ContainerNumber { get; private set; }
    protected Container(double loadMass, double height, double ownWeight, double depth, double maxLoadCapacity)
    {
        LoadMass = loadMass;
        Height = height;
        OwnWeight = ownWeight;
        Depth = depth;
        MaxLoadCapacity = maxLoadCapacity;
        
        ContainerNumber = ++counter;
    }
    
    protected static int GetCurrentContainerNumber() => counter;

    public abstract void EmptyContainer();
    public abstract void LoadContainer(double loadCapacity);
    
    public abstract void PrintInfo();
}