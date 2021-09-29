
namespace Data_Logging_and_Management_Application
{
    interface IDaqChannel
    {
        string ChannelIdentifier { get; set; }

        void StartNewChannelThread();

        void TerminateChannelThread();
    }
}
