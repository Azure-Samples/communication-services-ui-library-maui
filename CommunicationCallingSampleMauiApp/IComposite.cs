using System;
namespace CommunicationCallingSampleMauiApp
{
    public interface IComposite
    {
        void joinCall(string name, string acsToken, string callID, bool isTeamsCall, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection);
        List<string> languages();
    }
}

