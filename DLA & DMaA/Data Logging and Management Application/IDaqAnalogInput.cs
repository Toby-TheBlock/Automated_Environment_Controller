
namespace Data_Logging_and_Management_Application
{
    public interface IDaqAnalogInput : IDaqChannel
    {
        bool AnalogReadingInProgress { get; set; }

        float GetAnalogValue(string pChanName, string gChanName, double minVolt, double maxVolt);
    }
}
