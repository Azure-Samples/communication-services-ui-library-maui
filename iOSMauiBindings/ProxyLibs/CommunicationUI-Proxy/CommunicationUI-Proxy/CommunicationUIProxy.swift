//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  Licensed under the MIT License.
//

import Foundation
import SwiftUI
import AzureCommunicationUICalling
import AzureCommunicationCommon

@objcMembers
public class GroupCallObjectProxy: NSObject {
    public var groupId: String = ""
    public var displayName: String = ""

    public func setGroupCallProperties(_ groupId: String, displayName: String) {
        self.groupId = groupId
        self.displayName = displayName
    }
}

@objcMembers
public class TeamsMeetingObjectProxy: NSObject {
    public var teamsMeetingLink: String = ""
    public var displayName: String = ""
    
    public func setTeamsMeetingsProperties(_ teamsMeetingLink: String, displayName: String) {
        self.teamsMeetingLink = teamsMeetingLink
        self.displayName = displayName
    }
}

@objcMembers
public class CommunicationPersonaDataProxy: NSObject {
    public var avatarImage: UIImage? = nil
    public var renderDisplayName: String = ""

    public func setPersonaDataProperties(_ avatarImage: UIImage?, renderDisplayName: String) {
        self.avatarImage = avatarImage
        self.renderDisplayName = renderDisplayName
    }
}

@objcMembers
public class CommunicationLocalDataOptionProxy: NSObject {
    public var personaData: CommunicationPersonaDataProxy? = nil
    public var skipSetupScreen: Bool = false
    public var microphoneOn: Bool = false
    public var cameraOn: Bool = false

    public func setLocalDataOptionProperties(_ personaData: CommunicationPersonaDataProxy) {
        self.personaData = personaData
    }
}

@objcMembers
public class CommunicationThemeProxy: NSObject {
    public var primaryColor: UIColor = .clear
}

@objcMembers
public class CommunicationLocalizationProxy: NSObject {
    public var locale: Locale = Locale(identifier: "en")
    public var localizableFilename: String = "Localizable"
    public var isLeftToRight: Bool = true
}

@objcMembers
public class CommunicationErrorProxy: NSObject {
    public var code: String = ""
    public var error: NSError?
}

@objcMembers
public class CommunicationDismissedProxy: NSObject {
    public var errorCode: String?
    public var error: NSError?
}

@objcMembers
public class CommunicationCallStateProxy: NSObject {
    public var code: String = ""
}

@objcMembers
public class CommunicationScreenOrientationProxy: NSObject {
    public var callScreenOrientation: String = ""
    public var setupScreenOrientation: String = ""
}

@objcMembers
public class CommunicationUIProxy: NSObject {
    var callComposite: CallComposite? = nil
    
    public func startExperience(groupCall: GroupCallObjectProxy,
                                token: String,
                                localData: CommunicationLocalDataOptionProxy?,
                                theme: CommunicationThemeProxy?,
                                localization: CommunicationLocalizationProxy?,
                                orientationProxy: CommunicationScreenOrientationProxy?,
                                errorCallback: ((CommunicationErrorProxy) -> Void)?,
                                onRemoteParticipantJoinedCallback: (([String]) -> Void)?,
                                onCallStateChangedCallback: ((CommunicationCallStateProxy) -> Void)?,
                                onDismissedCallback: ((CommunicationDismissedProxy) -> Void)?) {
        let options: CallCompositeOptions
        var xamarinTheme: XamarinTheme?
        var localizationOptions: LocalizationOptions?
        var setupOrientation: OrientationOptions?
        var callOrientation: OrientationOptions?
        if let theme = theme {
            xamarinTheme = XamarinTheme(customPrimaryColor: theme.primaryColor)
        }
        
        if let localization = localization {
            localizationOptions = LocalizationOptions(locale: localization.locale,
                                                      localizableFilename: localization.localizableFilename,
                                                      layoutDirection: (localization.isLeftToRight) ? .leftToRight : .rightToLeft)
        }
        if let orientationSetupProxy = orientationProxy {
            setupOrientation = getOrientation(orientation: orientationSetupProxy.setupScreenOrientation)
        }
        if let orientationCallProxy = orientationProxy {
            callOrientation = getOrientation(orientation: orientationCallProxy.callScreenOrientation)
        }
        
        options = CallCompositeOptions(theme: xamarinTheme, localization: localizationOptions, setupScreenOrientation: setupOrientation, callingScreenOrientation: callOrientation)

        callComposite = CallComposite(withOptions: options)
        callComposite?.events.onError = { errorEvent in
                guard let callback = errorCallback else { return }
                let errorProxy = CommunicationErrorProxy()
                errorProxy.code = errorEvent.code
                errorProxy.error = errorEvent.error as NSError?

                callback(errorProxy)
        }


        callComposite?.events.onCallStateChanged = { callState in
            guard let callback = onCallStateChangedCallback else { return }
            let callStateProxy = CommunicationCallStateProxy()
            callStateProxy.code = callState.requestString
            callback(callStateProxy)
        }

        callComposite?.events.onDismissed = { dismissedEvent in
            guard let callback = onDismissedCallback else { return }
            let dismissedProxy = CommunicationDismissedProxy()
            dismissedProxy.errorCode = dismissedEvent.errorCode
            dismissedProxy.error = dismissedEvent.error as NSError?

            callback(dismissedProxy)
        }
        
        callComposite?.events.onRemoteParticipantJoined = { identifiers in
            guard let callback = onRemoteParticipantJoinedCallback else { return }
            let rawIds = identifiers.map { identifier -> String in
                switch identifier {
                case is CommunicationUserIdentifier:
                    return (identifier as? CommunicationUserIdentifier)?.identifier ?? ""
                case is UnknownIdentifier:
                    return (identifier as? UnknownIdentifier)?.identifier ?? ""
                case is PhoneNumberIdentifier:
                    return (identifier as? PhoneNumberIdentifier)?.phoneNumber ?? ""
                case is MicrosoftTeamsUserIdentifier:
                    return (identifier as? MicrosoftTeamsUserIdentifier)?.userId ?? ""
                default:
                    return ""
                }
            }
            callback(rawIds);
        }

        if let uuid = UUID(uuidString: groupCall.groupId),
           let tokenCredential = try? CommunicationTokenCredential(token: token) {
            let remoteOptions = RemoteOptions(for: .groupCall(groupId: uuid),
                                              credential: tokenCredential,
                                              displayName: groupCall.displayName)

            var localDataOptions: LocalOptions?
            if let localData = localData {
                localDataOptions = createLocalDataOptions(localData)
            }
            callComposite?.launch(remoteOptions: remoteOptions, localOptions: localDataOptions)
        }
    }

    public func startExperience(teamsMeeting: TeamsMeetingObjectProxy,
                                token: String,
                                localData: CommunicationLocalDataOptionProxy?,
                                theme: CommunicationThemeProxy?,
                                localization: CommunicationLocalizationProxy?,
                                orientationProxy: CommunicationScreenOrientationProxy?,
                                errorCallback: ((CommunicationErrorProxy) -> Void)?,
                                onRemoteParticipantJoinedCallback: (([String]) -> Void)?,
                                onCallStateChangedCallback: ((CommunicationCallStateProxy) -> Void)?,
                                onDismissedCallback: ((CommunicationDismissedProxy) -> Void)?) {
        let options: CallCompositeOptions
        var xamarinTheme: XamarinTheme?
        var localizationOptions: LocalizationOptions?
        var setupOrientation: OrientationOptions?
        var callOrientation: OrientationOptions?
        if let theme = theme {
            xamarinTheme = XamarinTheme(customPrimaryColor: theme.primaryColor)
        }
        
        if let localization = localization {
            localizationOptions = LocalizationOptions(locale: localization.locale,
                                                      localizableFilename: localization.localizableFilename,
                                                      layoutDirection: (localization.isLeftToRight) ? .leftToRight : .rightToLeft)
        }
        if let orientationSetupProxy = orientationProxy {
            setupOrientation = getOrientation(orientation: orientationSetupProxy.setupScreenOrientation)
        }
        if let orientationCallProxy = orientationProxy {
            callOrientation = getOrientation(orientation: orientationCallProxy.callScreenOrientation)
        }

        options = CallCompositeOptions(theme: xamarinTheme, localization: localizationOptions, setupScreenOrientation: setupOrientation, callingScreenOrientation: callOrientation)

        callComposite = CallComposite(withOptions: options)
        callComposite?.events.onError = { errorEvent in
                guard let callback = errorCallback else { return }
                let errorProxy = CommunicationErrorProxy()
                errorProxy.code = errorEvent.code
                errorProxy.error = errorEvent.error as NSError?

                callback(errorProxy)
        }


        callComposite?.events.onCallStateChanged = { callState in
            guard let callback = onCallStateChangedCallback else { return }
            let callStateProxy = CommunicationCallStateProxy()
            callStateProxy.code = callState.requestString
            callback(callStateProxy)
        }

        callComposite?.events.onDismissed = { dismissedEvent in
            guard let callback = onDismissedCallback else { return }
            let dismissedProxy = CommunicationDismissedProxy()
            dismissedProxy.errorCode = dismissedEvent.errorCode
            dismissedProxy.error = dismissedEvent.error as NSError?

            callback(dismissedProxy)
        }
        
        callComposite?.events.onRemoteParticipantJoined = { identifiers in
            guard let callback = onRemoteParticipantJoinedCallback else { return }
            let rawIds = identifiers.map { identifier -> String in
                switch identifier {
                case is CommunicationUserIdentifier:
                    return (identifier as? CommunicationUserIdentifier)?.identifier ?? ""
                case is UnknownIdentifier:
                    return (identifier as? UnknownIdentifier)?.identifier ?? ""
                case is PhoneNumberIdentifier:
                    return (identifier as? PhoneNumberIdentifier)?.phoneNumber ?? ""
                case is MicrosoftTeamsUserIdentifier:
                    return (identifier as? MicrosoftTeamsUserIdentifier)?.userId ?? ""
                default:
                    return ""
                }
            }
            callback(rawIds);
        }

        if let tokenCredential = try? CommunicationTokenCredential(token: token) {
            let remoteOptions = RemoteOptions(for: .teamsMeeting(teamsLink: teamsMeeting.teamsMeetingLink),
                                              credential: tokenCredential,
                                              displayName: teamsMeeting.displayName)
            var localDataOptions: LocalOptions?
            if let localData = localData {
                localDataOptions = createLocalDataOptions(localData)
            }

            callComposite?.launch(remoteOptions: remoteOptions, localOptions: localDataOptions)
        }
    }
    
    public func setRemote(participantDataOption: CommunicationPersonaDataProxy,
                          rawId: String,
                          onCompletionCallback:((Bool, NSError?) -> Void)?) throws {
        guard let composite = callComposite else {
            throw NSError(
                domain: "CommunicationUIProxy.setRemote",
                code: 0,
                userInfo: ["message": "CommunicationUIProxy startExperience has not been called yet."]
            )
        }
        
        let participant = ParticipantViewData(avatar: participantDataOption.avatarImage,
                                              displayName: participantDataOption.renderDisplayName)
        let unknownIdentifier = UnknownIdentifier(rawId)
        composite.set(remoteParticipantViewData: participant,
                      for: unknownIdentifier) { result in
            guard let onCompletionCallback = onCompletionCallback else { return }
            switch result {
            case .success:
                onCompletionCallback(true, nil)
            case .failure(let participantError):
                let error = NSError(domain: "CommunicationUIProxy.setRemote",
                                    code: 0,
                                    userInfo: ["message": participantError.rawValue])
                onCompletionCallback(false, error)
            }
        }
    }
    
    public func getAvailableLanguages() -> [String] {
        return SupportedLocale.values.map{ $0.identifier }
    }

    public func dismiss() {
        callComposite?.dismiss()
    }

    public func getCallStateCode() -> String {
        return callComposite?.callState.requestString ?? ""
    }
}

extension CommunicationUIProxy {
    private func createLocalDataOptions(_ localDataOptionsProxy: CommunicationLocalDataOptionProxy) ->
    LocalOptions {
        let avatar = localDataOptionsProxy.personaData?.avatarImage
        let renderDisplayName = localDataOptionsProxy.personaData?.renderDisplayName
        let persona: ParticipantViewData = ParticipantViewData(avatar: avatar, displayName: renderDisplayName)

        return LocalOptions(participantViewData: persona,
                            cameraOn: localDataOptionsProxy.cameraOn,
                            microphoneOn: localDataOptionsProxy.microphoneOn,
                            skipSetupScreen: localDataOptionsProxy.skipSetupScreen)
    }

    private func getOrientation(orientation: String) -> OrientationOptions {
        switch orientation {
            case "portrait": 
                return OrientationOptions.portrait
            case "landscape":
                return OrientationOptions.landscape
            case "landscapeRight":
                return OrientationOptions.landscapeRight
            case "landscapeLeft":
                return OrientationOptions.landscapeLeft
            case "allButUpsideDown":
                return OrientationOptions.allButUpsideDown
            default:
                return OrientationOptions.allButUpsideDown
        }
        return OrientationOptions.allButUpsideDown
    }
}

struct XamarinTheme: ThemeOptions {
    let customPrimaryColor: UIColor
    var primaryColor: UIColor {
        return customPrimaryColor
    }
}
