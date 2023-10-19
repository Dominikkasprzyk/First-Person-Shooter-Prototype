using System.Collections.Generic;

public enum Fabric
{
    Wood,
    Fire,
    Plant,
    Water,
}

public class FabricData
{
    public static Dictionary<Fabric, List<Fabric>> damages;

    static FabricData()
    {
        damages =  new Dictionary<Fabric, List<Fabric>>();

        damages.Add(Fabric.Wood, new List<Fabric>());
        damages.Add(Fabric.Fire, new List<Fabric>());
        damages.Add(Fabric.Water, new List<Fabric>());
        damages.Add(Fabric.Plant, new List<Fabric>());
        

        damages[Fabric.Wood].Add(Fabric.Fire);
        damages[Fabric.Wood].Add(Fabric.Plant);
        damages[Fabric.Wood].Add(Fabric.Water);

        damages[Fabric.Fire].Add(Fabric.Water);

        damages[Fabric.Water].Add(Fabric.Plant);

        damages[Fabric.Plant].Add(Fabric.Fire);
    }
}
