namespace ConsoleApp1;

public class LiquidContainer : Container, IHazardNotifier
{
    private LiquidType _liquidType;

    public LiquidContainer(double weight, double height, double ownWeight, double depth, double maxLoad,
        LiquidType liquidType) :
        base(weight, height, ownWeight, depth, maxLoad)
    {
        this._liquidType = liquidType;
    }

    public override void addLoad(double weight)
    {
        double allowedLoad = (_liquidType == LiquidType.DANGER) ? maxLoad * 0.5 : maxLoad * 0.9;
        if (this.weight + weight > allowedLoad)
        {
            Danger("Overloaded with weight: " + weight + " serial number: " + serialNumber);
            throw new OverfillException("Container is overloaded");
        }

        this.weight += weight;
    }


    public void Danger(string message)
    {
        Console.WriteLine(message);
    }

    public override string ToString()
    {
        return
            $"Liquid Container: {base.serialNumber} - Weight: {base.weight} - Height: {base.height}" +
            $" - Own Weight: {base.ownWeight} - Depth: {base.depth} - Max Load: {base.maxLoad}";
    }

    public override string generateSerialNumber()
    {
        containerCount++;
        return "KON-L-" + containerCount;
    }
}