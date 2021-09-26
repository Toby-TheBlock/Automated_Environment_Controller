
namespace Data_Logging_and_Management_Application
{
    interface IDaqOutput
    {
        string OutputPort { get; set; }

        void SetOutputPortState();

        void StartNewOutputThread();

        void TerminateOutputThread();
    }
}
