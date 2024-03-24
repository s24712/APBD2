using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class ContainerShip
    {
        public double ContainerSpeed { get; private set; }
        public int MaxContainers { get; private set; }
        public double MaxWeight { get; private set; }
        private List<Container> Containers { get; set; }

        public ContainerShip(double containerSpeed, int maxContainers, double maxWeight)
        {
            ContainerSpeed = containerSpeed;
            MaxContainers = maxContainers;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainers)
            {
                throw new InvalidOperationException("Max containers limit reached.");
            }

            if (Containers.Sum(c => c.weight) + container.weight > MaxWeight)
            {
                throw new InvalidOperationException("Max weight limit exceeded.");
            }

            Containers.Add(container);
        }

        public void UnloadContainer(Container container)
        {
            if (!Containers.Contains(container))
            {
                throw new InvalidOperationException("Container not found on this ship.");
            }

            Containers.Remove(container);
        }

        public void LoadContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                LoadContainer(container);
            }
        }

        public void ReplaceContainer(int index, Container newContainer)
        {
            if (index < 0 || index >= Containers.Count)
            {
                throw new IndexOutOfRangeException("Invalid container index.");
            }

            // Check if replacing the container exceeds the weight limit
            var currentWeight = Containers.Sum(c => c.weight) - Containers[index].weight + newContainer.weight;
            if (currentWeight > MaxWeight)
            {
                throw new InvalidOperationException("Replacing container exceeds max weight limit.");
            }

            Containers[index] = newContainer;
        }

        public Container TransferContainer(ContainerShip targetShip, Container container)
        {
            this.UnloadContainer(container);
            try
            {
                targetShip.LoadContainer(container);
                return container;
            }
            catch (Exception e)
            {
                this.LoadContainer(container);
                throw;
            }
        }

        public string GetContainerInfo(Container container)
        {
            if (!Containers.Contains(container))
            {
                return "Container not found on this ship.";
            }
            return container.ToString();
        }

        public string GetShipInfo()
        {
            return $"Ship Max Containers: {MaxContainers}, Max Weight: {MaxWeight}, Current Containers: {Containers.Count}, " +
                   $"Current Total Weight: {Containers.Sum(c => c.weight)}";
        }
    }
}
