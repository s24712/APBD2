namespace ConsoleApp1
{
    public class RefrigeratedContainer : Container
    {
        private static readonly Dictionary<ProductType, (double Min, double Max)> ValidTemperatureRanges =
            new Dictionary<ProductType, (double Min, double Max)>
            {
                { ProductType.Bananas, (13.3, 14.9) },
                { ProductType.Chocolate, (18, 20) },
                { ProductType.Fish, (-2, 2) },
                { ProductType.Meat, (-1.5, -1.5) },
                { ProductType.IceCream, (-18, -15) },
                { ProductType.FrozenPizza, (-30, -18) },
                { ProductType.Cheese, (5, 7.2) },
                { ProductType.Sausages, (5, 7) },
                { ProductType.Butter, (19, 21) },
                { ProductType.Eggs, (19, 21) }
            };

        public double Temperature { get; private set; }
        public ProductType ProductType { get; private set; }

        public static int MaxContainerCount { get; set; }
        public static double MaxTotalWeight { get; set; }

        public RefrigeratedContainer(double weight, double height, double ownWeight, double depth, double maxLoad,
            double temperature, ProductType productType) : base(weight, height, ownWeight, depth, maxLoad)
        {
            if (!IsValidTemperatureForProduct(temperature, productType)) // Corrected to use the instance's productType
            {
                throw new ArgumentException($"Invalid temperature {temperature} for product {productType}");
            }

            this.Temperature = temperature;
            this.ProductType = productType;
        }

        public static bool IsValidTemperatureForProduct(double temperature, ProductType productType)
        {
            if (ValidTemperatureRanges.ContainsKey(productType))
            {
                (double Min, double Max) range = ValidTemperatureRanges[productType];
                return temperature >= range.Min && temperature <= range.Max;
            }

            throw new ArgumentException($"Unknown product type: {productType}");
        }

        public override string generateSerialNumber() 
        {
            if (containerCount >= MaxContainerCount)
            {
                throw new InvalidOperationException("Cannot create more containers than the maximum allowed.");
            }

            containerCount++;
            return "KON-R-" + containerCount;
        }

        public override void addLoad(double weight) 
        {
            if (this.weight + weight > maxLoad)
            {
                Console.WriteLine("Container " + serialNumber + " is overloaded.");
                throw new OverfillException("Container is overloaded");
            }

            this.weight += weight;
        }

        public override string ToString()
        {
            return $"Container: {serialNumber} - Weight: {weight} - Height: {height} - Own Weight: {ownWeight} " +
                   $"- Depth: {depth} - Max Load: {maxLoad} - Temperature: {Temperature} - Product Type: {ProductType}";
        }
    }
}