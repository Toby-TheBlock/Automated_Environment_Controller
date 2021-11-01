
namespace Data_Logging_and_Management_Application
{
    public interface IDaqChannel
    {
        string ChannelIdentifier { get; set; }

        void StartNewChannelThread();

        void TerminateChannelThread();
    }
}
