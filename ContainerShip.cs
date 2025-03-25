using System.Collections;

namespace APBD;

public class ContainerShip
{
   public List<Container> Containers { get; set;} = new List<Container>();
   public double MaxSpeed { get; set; }
   public int MaxContainer { get; set; }
   public int CurrentNumContainer { get; set; } = 0;
   public double CurrentWeightContainer { get; set; } = 0;
   public double MaxContainerWeight { get; set; }

   public ContainerShip(double maxSpeed, int maxContainer, double maxContainerWeight)
   {
      MaxSpeed = maxSpeed;
      MaxContainer = maxContainer;
      MaxContainerWeight = maxContainerWeight;
   }

   public void AddContainer(Container container)
   {
      if (CurrentNumContainer < MaxContainer && CurrentWeightContainer < MaxContainerWeight)
      {
         Containers.Add(container);
         CurrentNumContainer++;
         CurrentWeightContainer += container.OwnWeight + container.LoadMass;
      }
      else
      {
         throw new Exception("ContainerShip: Too many containers or Max weight is exceeded.");
      }
   }

   public void AddListContainer(List<Container> containers)
   {
      int numContainers = containers.Count;
      double totalWeight = containers.Sum(c => c.OwnWeight + c.LoadMass);
      
      if (CurrentNumContainer + numContainers > MaxContainer)
      {
         throw new Exception("ContainerShip: Przekroczono maksymalną liczbę kontenerów.");
      }

      if (CurrentWeightContainer + totalWeight > MaxContainerWeight)
      {
         throw new Exception("ContainerShip: Przekroczono maksymalną łączną wagę kontenerów.");
      }

      Containers.AddRange(containers);
      CurrentNumContainer += numContainers;
      CurrentWeightContainer += totalWeight;
   }
   
   public void RemoveContainer(Container container)
   {
      if (Containers.Remove(container))
      {
         CurrentWeightContainer -= container.OwnWeight + container.LoadMass;
         CurrentNumContainer--;
      }
      else
      {
         throw new Exception("The specified container is not on board the ship..");
      }
   }
   public void UnloadContainer(Container container)
   {
      if (!Containers.Contains(container))
      {
         throw new Exception("This container is not on the ship.");
      }

      container.EmptyContainer();
   }
   
   public void ReplaceContainer(string serialNumber, Container newContainer)
   {
      var oldContainer = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);

      if (oldContainer == null)
         throw new Exception($"Container not found : {serialNumber}.");

      double newTotalWeight = CurrentWeightContainer
                              - (oldContainer.OwnWeight + oldContainer.LoadMass)
                              + (newContainer.OwnWeight + newContainer.LoadMass);

      if (newTotalWeight > MaxContainerWeight)
         throw new Exception("\nThe change would result in exceeding the maximum weight of the containers.");
      
      int index = Containers.IndexOf(oldContainer);
      Containers[index] = newContainer;

      CurrentWeightContainer = newTotalWeight;
   }
   
   public static void TransferContainer(ContainerShip from, ContainerShip to, string serialNumber)
   {
      var container = from.Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
      if (container == null)
         throw new Exception($"Container{serialNumber} is not on the source ship.");
      
      try
      {
         to.AddContainer(container);
         from.RemoveContainer(container);
      }
      catch (Exception ex)
      {
         throw new Exception($"Failed to move container: {ex.Message}");
      }
   }
   
   public void PrintShipInfo()
   {
      Console.WriteLine("==== ContainerShip ====");
      Console.WriteLine($"Max speed: {MaxSpeed} knots");
      Console.WriteLine($"Max containers: {MaxContainer}");
      Console.WriteLine($"Current containers: {Containers.Count}");
      Console.WriteLine($"Max weight: {MaxContainerWeight} kg");
      Console.WriteLine($"Current weight: {CurrentWeightContainer} kg");
      Console.WriteLine("----- Containers -----");

      foreach (var container in Containers)
      {
         container.PrintInfo();
      }
   }
}