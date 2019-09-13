namespace LightSwitch.Core.Commands
{
    public interface ILightSwitchCommand
    {
        byte[] ToBuffer();
    }
}