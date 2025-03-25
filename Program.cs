namespace APBD;

class Program
{
    static void Main(string[] args)
    {
        // === Produkty ===
        var bananas = new Product("Bananas", 13.3);
        var butter = new Product("Butter", 20.5);

        // === Kontenery ===
        var cooler = new RefrigeratedCon(0, 200, 300, 100, 1000, bananas, 14);
        cooler.LoadProduct(bananas, 500);

        var gas = new GasCon(0, 300, 350, 100, 1000, 10);
        gas.LoadContainer(800);

        var liquid = new LiquidCon(0, 250, 250, 90, 1000, true);
        liquid.LoadContainer(400);

        // === Statek A ===
        var shipA = new ContainerShip(25, 5, 5000);
        shipA.AddContainer(cooler);
        shipA.AddContainer(gas);
        shipA.AddContainer(liquid);
        
        shipA.PrintShipInfo();
        
        shipA.UnloadContainer(liquid);
        
        var newGas = new GasCon(0, 300, 300, 100, 1000, 12);
        newGas.LoadContainer(600);
        shipA.ReplaceContainer(gas.SerialNumber, newGas);
        
        var shipB = new ContainerShip(30, 3, 3000);
        ContainerShip.TransferContainer(shipA, shipB, cooler.SerialNumber);
        
        newGas.PrintInfo();
        shipB.PrintShipInfo();
    }
}