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
public class CommunicationUIProxy: NSObject {
    var callComposite: CallComposite? = nil
    
    public func startExperience(groupCall: GroupCallObjectProxy,
                                token: String,
                                localData: CommunicationLocalDataOptionProxy?,
                                theme: CommunicationThemeProxy?,
                                localization: CommunicationLocalizationProxy?,
                                errorCallback: ((CommunicationErrorProxy) -> Void)?,
                                onRemoteParticipantJoinedCallback: (([String]) -> Void)?) {
        let options: CallCompositeOptions
        var xamarinTheme: XamarinTheme?
        var localizationOptions: LocalizationOptions? 
        if let theme = theme {
            xamarinTheme = XamarinTheme(customPrimaryColor: theme.primaryColor)
        }
        
        if let localization = localization {
            localizationOptions = LocalizationOptions(locale: localization.locale,
                                                      localizableFilename: localization.localizableFilename,
                                                      layoutDirection: (localization.isLeftToRight) ? .leftToRight : .rightToLeft)
        }
        
        options = CallCompositeOptions(theme: xamarinTheme, localization: localizationOptions)

        callComposite = CallComposite(withOptions: options)
        callComposite?.events.onError = { errorEvent in
                guard let callback = errorCallback else { return }
                let errorProxy = CommunicationErrorProxy()
                errorProxy.code = errorEvent.code
                errorProxy.error = errorEvent.error as NSError?

                callback(errorProxy)
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
                                errorCallback: ((CommunicationErrorProxy) -> Void)?,
                                onRemoteParticipantJoinedCallback: (([String]) -> Void)?) {
        let options: CallCompositeOptions
        var xamarinTheme: XamarinTheme?
        var localizationOptions: LocalizationOptions?
        if let theme = theme {
            xamarinTheme = XamarinTheme(customPrimaryColor: theme.primaryColor)
        }
        
        if let localization = localization {
            localizationOptions = LocalizationOptions(locale: localization.locale,
                                                      localizableFilename: localization.localizableFilename,
                                                      layoutDirection: (localization.isLeftToRight) ? .leftToRight : .rightToLeft)
        }

        options = CallCompositeOptions(theme: xamarinTheme, localization: localizationOptions)

        callComposite = CallComposite(withOptions: options)
        callComposite?.events.onError = { errorEvent in
                guard let callback = errorCallback else { return }
                let errorProxy = CommunicationErrorProxy()
                errorProxy.code = errorEvent.code
                errorProxy.error = errorEvent.error as NSError?

                callback(errorProxy)
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
}

extension CommunicationUIProxy {
    private func createLocalDataOptions(_ localDataOptionsProxy: CommunicationLocalDataOptionProxy) ->
    LocalOptions {
        let avatar = localDataOptionsProxy.personaData?.avatarImage
        let renderDisplayName = localDataOptionsProxy.personaData?.renderDisplayName
        let persona: ParticipantViewData = ParticipantViewData(avatar: avatar, displayName: renderDisplayName)

        return LocalOptions(participantViewData: persona)
    }
}

struct XamarinTheme: ThemeOptions {
    let customPrimaryColor: UIColor
    var primaryColor: UIColor {
        return customPrimaryColor
    }
}
