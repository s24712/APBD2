namespace ConsoleApp1;

public class GasContainer : Container, IHazardNotifier
{
    public double pressureLevel;

    public GasContainer(double weight, double height, double ownWeight, double depth, double maxLoad,
        double pressureLevel) : base(weight,
        height, ownWeight, depth, maxLoad)
    {
        this.pressureLevel = pressureLevel;
    }

    public override string generateSerialNumber()
    {
        containerCount++;
        return "KON-G-" + containerCount;
    }

    public void Danger(string message)
    {
        Console.WriteLine(message);
    }

    public override void loadOut()
    {
        Console.WriteLine($"Container {serialNumber} is being unloaded.");
        double newWeight = weight * 0.05;
        weight = newWeight;
    }

    public override void addLoad(double weight)
    {
        if (this.weight + weight > maxLoad)
        {
            Danger("Overloaded with weight: " + weight + "serial number: " + serialNumber);
            throw new OverfillException("Container is overloaded");
        }

        this.weight += weight;
    }

    public override string ToString()
    {
        return
            $"Container: {serialNumber} - Weight: {weight} - Height: {height} - Own Weight: {ownWeight} " +
            $"- Depth: {depth} - Max Load: {maxLoad}";
    }
}