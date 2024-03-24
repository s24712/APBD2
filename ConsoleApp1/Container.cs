namespace ConsoleApp1;

public class Container
{
    public double weight;
    public double height;
    public double ownWeight;
    public double depth;
    public string serialNumber;
    public double maxLoad;
    public static int containerCount = 0;

    public Container(double weight, double height, double ownWeight, double depth, double maxLoad)
    {
        this.weight = 0;
        this.height = height;
        this.ownWeight = ownWeight;
        this.depth = depth;
        this.maxLoad = maxLoad;
        this.serialNumber = generateSerialNumber();
    }

    public virtual string generateSerialNumber()
    {
        containerCount++;
        return "KON-Unknown-Type-" + containerCount;
    }

    public virtual void loadOut()
    {
        Console.WriteLine("Container " + serialNumber + " is being unloaded.");
        this.weight = 0;
    }

    public virtual void addLoad(double weight)
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
        return
            $"Container: {serialNumber} - Weight: {weight} - Height: {height} - Own Weight: {ownWeight} " +
            $"- Depth: {depth} - Max Load: {maxLoad}";
    }
}