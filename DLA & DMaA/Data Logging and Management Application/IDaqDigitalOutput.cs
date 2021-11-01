
namespace Data_Logging_and_Management_Application
{
    public interface IDaqDigitalOutput : IDaqChannel
    {

        void SetOutputPortState();
    }
}
