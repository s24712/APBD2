namespace ConsoleApp1
{
    internal class Program

    {
        static void Main(string[] args)
        {
            RefrigeratedContainer.MaxContainerCount = 10;
            Container standardContainer = new Container(1, 2, 3, 4, 120);
            LiquidContainer liquidContainer = new LiquidContainer(1, 2, 3, 4, 120, LiquidType.DANGER);
            GasContainer gasContainer = new GasContainer(1, 2, 3, 4, 120, 100);
            RefrigeratedContainer refrigeratedContainer =
                new RefrigeratedContainer(1, 2, 3, 4, 120, -18, ProductType.IceCream);

            try
            {
                liquidContainer.addLoad(60);
                gasContainer.addLoad(110);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<Container> containers = new List<Container> { liquidContainer, refrigeratedContainer };

            ContainerShip containerShip = new ContainerShip(100, 10, 1000);
            try
            {
                containerShip.LoadContainers(containers);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            containerShip.UnloadContainer(liquidContainer);
            Console.WriteLine(containerShip.GetShipInfo());

            ContainerShip anotherShip = new ContainerShip(200, 5, 2000);
            try
            {
                containerShip.ReplaceContainer(0, standardContainer);
                containerShip.TransferContainer(anotherShip, standardContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(containerShip.GetShipInfo());
            Console.WriteLine(anotherShip.GetShipInfo());
        }
    }
}