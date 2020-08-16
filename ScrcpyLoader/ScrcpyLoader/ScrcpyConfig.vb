Imports System.ComponentModel
Imports InnerLibs


Public Class ShortcutModList
    Inherits List(Of ShortcutMod)

    Public Overrides Function ToString() As String
        Return Me.Join(",")
    End Function

End Class

<DefaultProperty("Shortcut")>
Public Class ShortcutMod


    Public ReadOnly Property Shortcut
        Get
            Return Me.ToString()
        End Get
    End Property
    Public Property RightCtrl As Boolean = False
    Public Property LeftCtrl As Boolean = False
    Public Property RightAlt As Boolean = False
    Public Property LeftAlt As Boolean = False
    Public Property RightSuper As Boolean = False
    Public Property LeftSuper As Boolean = False

    Public Overrides Function ToString() As String
        Dim lista As New List(Of String)

        If LeftCtrl Then
            lista.Add("lctrl")
        End If

        If RightCtrl Then
            lista.Add("rctrl")
        End If

        If LeftSuper Then
            lista.Add("lsuper")
        End If

        If RightSuper Then
            lista.Add("rsuper")
        End If

        If LeftAlt Then
            lista.Add("lalt")
        End If

        If RightAlt Then
            lista.Add("ralt")
        End If

        Return lista.Join("+")

    End Function

End Class

Public Class ScrcpyConfig

    <Browsable(False)>
    Property DefaultKeyboard As String

    <Description("If true, enable NULL Keybaord as default Input Method (if installed) This is not a Scrcpy option. This behavior is defined by Scrcpy Loader"), Category("Behavior"), Browsable(False)>
    Property EnableNullKeyboard As Boolean

    <Description("Capture the output window of Scrcpy and host inside a tab. This is not a Scrcpy option. This behavior is defined by Scrcpy Loader"), Category("Behavior"), Browsable(False)>
    Property HostWindow As Boolean

    <Description("Auto Start Scrcpy as soon as device is connected. This is not a Scrcpy option. This behavior is defined by Scrcpy Loader"), Category("Behavior"), Browsable(False)>
    Property AutoStart As Boolean

    <Description("By default, the window title is the device model. It can be changed"), Category("Definition")>
    Property Title As String

    <Description("Horizontal position"), Category("Position")>
    Property WindowX As Integer?

    <Description("Vertical position"), Category("Position")>
    Property WindowY As Integer?

    <Description("Initial Width (preserve aspect ratio)"), Category("Size")>
    Property Width As Integer?

    <Description("Initial Height (preserve aspect ratio)"), Category("Size")>
    Property Height As Integer?

    <Description("Sometimes, it is useful to mirror an Android device at a lower definition to increase performance. To limit both the width and height to some value, preserving its aspect ratio"), Category("Definition")>
    Property ReduceSize As Integer?

    <Description("The default bit-rate is 8 Mbps. To change the video bitrate"), Category("Definition")>
    Property BitRate As String

    <Description("Rotates only the window content. This affects only the display, not the recording."), Category("Orientation")>
    Property ScreenRotation As ScreenOrientation

    <Description("Rotates the device content. This affects recording."), Category("Orientation")>
    Property LockVideoOrientation As ScreenOrientation

    <Description("Limit the FPS"), Category("Definition")>
    Property LimitFramerate As Integer?

    <Description("Crop part of screen"), Category("Size"), TypeConverter(GetType(ExpandableObjectConverter))>
    Property Crop As CropInfo = New CropInfo

    <Description("Hide the mirror window. Useful if you want to record a file without showing anything on computer"), Category("Definition")>
    Property NoDisplay As Boolean

    <Description("Video File Name (.mkv)"), Category("Recording"), EditorAttribute(GetType(CustomFileBrowser), GetType(System.Drawing.Design.UITypeEditor))>
    Property RecordFileName As String

    <Description("Change to TRUE if you want to record a video"), Category("Recording")>
    Property RecordFile As Boolean

    <Description("Force Text Input. Default is KeyEvents"), Category("Definition")>
    Property PreferText As Boolean

    <Description("Force window stay always on top of other windows."), Category("Position")>
    Property AlwaysOnTop As Boolean

    <Description("Force the device to stay awake during mirroring (Dont Lock itself)."), Category("Behavior")>
    Property StayAwake As Boolean

    <Description("Disable the computer input (Keyboard and Mouse) to Scrcpy window."), Category("Behavior")>
    Property NoControl As Boolean

    <Description("If several displays are available, it is possible to select the display to mirror"), Category("Definition")>
    Property Display As Integer?

    <Description("Turn off the device screen while keep mirroring"), Category("Behavior")>
    Property TurnScreenOff As Boolean

    <Description("Render expired frames. May cause delay"), Category("Behavior")>
    Property RenderExpiredFrames As Boolean

    <Description("Show physical touches. (Fingers on screen)"), Category("Behavior")>
    Property ShowTouches As Boolean

    <Description("The Path to push files on drag-n-drop (non-APK) "), Category("Behavior")>
    Property PushFilePath As String

    <Description("Hide the borders of window"), Category("Definition")>
    Property Borderless As Boolean

    <Description("Make window fullscreen on start"), Category("Definition")>
    Property Fullscreen As Boolean

    <Description("Define the Scrcpy MOD key(s)"), Category("Definition"), TypeConverter(GetType(ExpandableObjectConverter)), Editor(GetType(ShortcutModEditor), GetType(System.Drawing.Design.UITypeEditor))>
    Property ShortcutMOD As New SHortcutModList

    <Description("Disable computer Screen Saver"), Category("Behavior")>
    Property DisableScreenSaver As Boolean

    Function Arguments() As List(Of String)
        Dim args = New List(Of String)

        If Title.IsNotBlank() Then
            args.Add($"--window-title {Title.Quote}")
        End If

        If WindowX.HasValue Then
            args.Add("--window-x  " & WindowX)
        End If

        If WindowY.HasValue AndAlso WindowY > -1 Then
            args.Add("--window-y  " & WindowY)
        End If

        If Width.HasValue AndAlso Width > 0 Then
            args.Add("--window-width  " & Width)
        End If

        If Height.HasValue AndAlso Height > 0 Then
            args.Add("--window-height  " & Height)
        End If

        If Borderless Then
            args.Add("--window-borderless")
        End If

        If AlwaysOnTop Then
            If Not HostWindow Then
                args.Add("--always-on-top")
            End If
        End If

        If Fullscreen Then
            If Not HostWindow Then
                args.Add("--fullscreen")
            End If
        End If

        If HostWindow Then
            If Not Borderless Then
                args.Add("--window-borderless")
            End If
        End If

        If ReduceSize.HasValue AndAlso ReduceSize > 0 Then
            args.Add("--max-size " & ReduceSize)
        End If

        If BitRate.IsNotBlank() Then
            args.Add("--bit-rate " & BitRate)
        End If

        If LimitFramerate.HasValue AndAlso LimitFramerate > 0 Then
            args.Add("--max-fps " & LimitFramerate)
        End If

        If Crop IsNot Nothing AndAlso Crop.ToString.IsNotBlank() Then
            args.Add("--crop " & Crop.ToString())
        End If

        If LockVideoOrientation > 0 Then
            args.Add("--lock-video-orientation " & LockVideoOrientation)
        End If

        If ScreenRotation > 0 Then
            args.Add("--rotation " & ScreenRotation)
        End If

        If NoDisplay Then
            args.Add("--no-display")
        End If

        If RecordFile AndAlso RecordFileName.IsNotBlank() Then
            args.Add("--record " & RecordFileName.Quote)
        End If

        If PreferText Then
            args.Add("--prefer-text")
        End If

        If NoControl Then
            args.Add("--no-control")
        End If

        If Display.HasValue AndAlso Display > 0 Then
            args.Add("--display " & Display)
        End If

        If StayAwake Then
            args.Add("--stay-awake")
        End If

        If TurnScreenOff Then
            args.Add("--turn-screen-off")
        End If

        If RenderExpiredFrames Then
            args.Add("--render-expired-frames")
        End If

        If ShowTouches Then
            args.Add("--show-touches")
        End If

        If PushFilePath.IsNotBlank Then
            args.Add("--push-target " & PushFilePath.Quote)
        End If

        If ShortcutMOD IsNot Nothing AndAlso ShortcutMOD.Count > 0 Then
            args.Add("--shortcut-mod=" & ShortcutMOD.Join(","))
        End If

        If DisableScreenSaver Then
            args.Add("--disable-screensaver")
        End If

        Return args
    End Function

    Public Overrides Function ToString() As String

        Return Arguments.Join(" ")

    End Function

    <Description("Crop and Offset information"), Category("Size")>
    Public Class CropInfo

        <Description("Width of crop"), Category("Size")>
        Property Width As Integer

        <Description("Height of crop"), Category("Size")>
        Property Height As Integer

        <Description("Horizontal Offset"), Category("Size")>
        Property OffsetX As Integer

        <Description("Vertical Offset"), Category("Size")>
        Property OffsetY As Integer

        Public Overrides Function ToString() As String
            If Width > 0 AndAlso Height > 0 AndAlso OffsetX > 0 AndAlso OffsetY > 0 Then
                Return {Width, Height, OffsetX, OffsetY}.Join(":")
            End If
            Return ""
        End Function

    End Class

End Class

Public Class GithubRelease
    Public Property url As String
    Public Property assets_url As String
    Public Property upload_url As String
    Public Property html_url As String
    Public Property id As Integer
    Public Property node_id As String
    Public Property tag_name As String
    Public Property target_commitish As String
    Public Property name As String
    Public Property draft As Boolean
    Public Property author As Author
    Public Property prerelease As Boolean
    Public Property created_at As Date
    Public Property published_at As Date
    Public Property assets As Asset()
    Public Property tarball_url As String
    Public Property zipball_url As String
    Public Property body As String
End Class

Public Class Author
    Public Property login As String
    Public Property id As Integer
    Public Property node_id As String
    Public Property avatar_url As String
    Public Property gravatar_id As String
    Public Property url As String
    Public Property html_url As String
    Public Property followers_url As String
    Public Property following_url As String
    Public Property gists_url As String
    Public Property starred_url As String
    Public Property subscriptions_url As String
    Public Property organizations_url As String
    Public Property repos_url As String
    Public Property events_url As String
    Public Property received_events_url As String
    Public Property type As String
    Public Property site_admin As Boolean
End Class

Public Class Asset
    Public Property url As String
    Public Property id As Integer
    Public Property node_id As String
    Public Property name As String
    Public Property label As Object
    Public Property uploader As Uploader
    Public Property content_type As String
    Public Property state As String
    Public Property size As Integer
    Public Property download_count As Integer
    Public Property created_at As Date
    Public Property updated_at As Date
    Public Property browser_download_url As String
End Class

Public Class Uploader
    Public Property login As String
    Public Property id As Integer
    Public Property node_id As String
    Public Property avatar_url As String
    Public Property gravatar_id As String
    Public Property url As String
    Public Property html_url As String
    Public Property followers_url As String
    Public Property following_url As String
    Public Property gists_url As String
    Public Property starred_url As String
    Public Property subscriptions_url As String
    Public Property organizations_url As String
    Public Property repos_url As String
    Public Property events_url As String
    Public Property received_events_url As String
    Public Property type As String
    Public Property site_admin As Boolean
End Class