public class SecondCylinder : CylinderBase
{
    protected override bool SetCanTurn()
    {
        return false;
    }

    protected override int SetCylinderTurnDirection()
    {
        return -1;
    }
}
