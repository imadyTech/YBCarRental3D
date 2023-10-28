using imady.NebuEvent;
using YBCarRental3D;

public class YBCarSelectedMessage : NebuMessageBase
{
    public YB_Car messageBody { get; set; }

    public YBCarSelectedMessage() { }

    public YBCarSelectedMessage(YB_Car messageBody)
    {
        this.messageBody = messageBody;
    }
}
public class YBCarDeSelectedMessage : NebuMessageBase
{
    public YB_Car messageBody { get; set; }

    public YBCarDeSelectedMessage() { }

    public YBCarDeSelectedMessage(YB_Car messageBody)
    {
        this.messageBody = messageBody;
    }
}
