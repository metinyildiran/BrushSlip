public class FirstCylinder : CylinderBase
{
    protected override bool SetCanTurn()
    {
        return true;
    }

    protected override int SetCylinderTurnDirection()
    {
        return 1;
    }
}
