using System;
namespace CommunicationCallingSampleMauiApp
{
    public interface IComposite
    {
        void joinCall(string name, string acsToken, string callID, CallType callType, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection, OrientationProps? orientationProps, CallControlProps? callControlProps);
        List<string> languages();
        List<string> orientations();
    }
}

