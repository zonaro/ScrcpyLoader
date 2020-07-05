Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports InnerLibs
Imports Newtonsoft.Json
Imports SharpAdbClient

Public Class DeviceConsole

    Property Configuration As New ScrcpyConfig
    Public Property Device As DeviceData

    Sub LogText(Text As String, Optional Color As Color? = Nothing)
        Color = If(Color, System.Drawing.Color.White)
        Me.ConsoleControl1.WriteOutput(Environment.NewLine & Text, Color)
    End Sub

    Sub Breakline()
        LogText(Environment.NewLine)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles start_bt.Click
        If start_bt.Text = "START" Then
            StartProccess()
        Else
            Me.ConsoleControl1.ProcessInterface.StopProcess()
            ChangeToDefaultKeyboard()
            EnableStart()
        End If
    End Sub

    Sub StartProccess(Optional lineargs As String = "")
        Try

            If ADBProc IsNot Nothing AndAlso ADBProc.HasExited = False Then
                ADBProc.Kill()
                ADBProc.Dispose()
            End If

            ChangeToNullKeyboard()

            While Me.ConsoleControl1.IsProcessRunning
                Me.ConsoleControl1.StopProcess()
            End While

            If Me.Configuration.HostWindow Then
                Me.Configuration.Width = window_cap.Width
                Me.Configuration.Height = window_cap.Height
                With DirectCast(Me.FindForm(), Main)
                    .StayOnTopToolStripMenuItem.Checked = Me.Configuration.AlwaysOnTop
                    .TopMost = Me.Configuration.AlwaysOnTop
                End With
            End If

            If TabControl1.SelectedTab Is TabPage1 Then
                TabControl1.SelectedTab = TabPage2
            End If

            SaveConfig()

            lineargs = lineargs.IfBlank("-s " + Me.device_id.Text & " " & Configuration.ToString())

            LogText(lineargs, Color.Yellow)
            Breakline()
            Try
                Me.ConsoleControl1.StartProcess(ScrcpyDirectory.FullName & "\scrcpy.exe", lineargs)
            Catch ex As Exception
                LogText(ex.ToFullExceptionString, Color.Magenta)
            End Try
            If Me.Configuration.HostWindow AndAlso Me.ConsoleControl1.IsProcessRunning Then
                CaptureWindow(Me.ConsoleControl1.ProcessInterface.Process)
            End If
        Catch ex As Exception
            LogText(ex.ToFullExceptionString, Color.Red)
        End Try
        If Me.ConsoleControl1.IsProcessRunning = False Then EnableStart() Else DisableStart()
    End Sub

    Sub EnableStart()
        Try
            ToolStripMenuItem2.Enabled = True
            start_bt.Text = "START"
            SHowHideTab1(True)
            ChangeToDefaultKeyboard()
        Catch ex As Exception
        End Try
    End Sub

    Sub DisableStart()
        Try
            ToolStripMenuItem2.Enabled = False
            SHowHideTab1(False)
            start_bt.Text = "STOP"
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadConfig()
        Try
            Dim folder = (CurDir() & "\configs\").ToDirectoryInfo().FullName & "\"
            Dim file = folder & device_id.Text & ".scrcpylconfig"
            If IO.File.Exists(file) Then
                Me.Configuration = JsonConvert.DeserializeObject(Of ScrcpyConfig)(IO.File.ReadAllText(file, Encoding.Default))
                Me.Configs.SelectedObject = Me.Configuration
                Me.autostart_menu.Checked = Me.Configuration.AutoStart
                Me.capture_menu.Checked = Me.Configuration.HostWindow
                Me.NullKeyboard_menu.Checked = Me.Configuration.EnableNullKeyboard AndAlso Me.NullKeyboard_menu.Enabled
            Else
                Me.Configuration = New ScrcpyConfig
                SaveConfig()
                LoadConfig()
            End If
        Catch ex As Exception
            LogText(ex.ToFullExceptionString, Color.Red)
        End Try
    End Sub

    Sub SaveConfig()
        Try
            Dim folder = (CurDir() & "\configs\").ToDirectoryInfo().FullName & "\"
            Dim file = folder & device_id.Text & ".scrcpylconfig"
            JsonConvert.SerializeObject(Me.Configuration).WriteToFile(file)
        Catch ex As Exception
            LogText(ex.ToFullExceptionString, Color.Red)
        End Try
    End Sub

    Property StartWithArguments As Boolean = False

    Delegate Sub CallBackDel()

    Private Sub ExitProcess(sender As Object, e As EventArgs)

        If (Me.TabControl1.InvokeRequired) Then
            Dim d = New CallBackDel(Sub() EnableStart())
            Me.Invoke(d)
        Else
            EnableStart()
        End If

    End Sub

    Private Sub DeviceConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Me.ConsoleControl1.ProcessInterface.OnProcessExit, AddressOf ExitProcess
        LoadConfig()
        SHowHideTab3()
        If StartWithArguments AndAlso Arguments.Count > 0 Then
            Me.StartProccess("-s " & Device.Serial & " " & Arguments.Join(" "))
            Me.Hide()
            StartWithArguments = False
        Else
            If Me.Configuration.AutoStart Then
                Me.StartProccess()
            End If
        End If

        NullKeyboard_menu.Enabled = False
        GetApps()

        While ADBProc.HasExited = False
            Thread.Sleep(900)
        End While

        GetDefaultKeyboard()
        While ADBProc.HasExited = False
            Thread.Sleep(900)
        End While
        ChangeToDefaultKeyboard()
    End Sub

    Private Sub HELPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HELPToolStripMenuItem.Click
        Try
            Me.StartProccess("--help")
        Catch ex As Exception
            LogText(ex.ToFullExceptionString, Color.Red)
        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Me.Configuration = New ScrcpyConfig
        SaveConfig()
        LoadConfig()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem9.Click, ToolStripMenuItem8.Click, ToolStripMenuItem7.Click, ToolStripMenuItem5.Click, ToolStripMenuItem11.Click, ToolStripMenuItem10.Click, OtherPiPSizeToolStripMenuItem.Click
        If Not sender Is OtherPiPSizeToolStripMenuItem Then
            If ToolStripTextBox1.Text.IsBlank Then
                ToolStripTextBox1.Text = "20%"
            End If
            ToolStripTextBox1.Text = sender.Text.trim("%") & "%"
        End If

        If Me.Configuration Is Nothing Then
            Me.Configuration = New ScrcpyConfig
        End If

        Dim s = Screen.FromControl(Me).WorkingArea
        Dim PercentSize = ToolStripTextBox1.Text.IfBlank("20").AppendIf("%", Function(x As String) x.EndsWith("%") = False)

        Me.Configuration.Width = PercentSize.CalculateValueFromPercent(s.Width)
        Me.Configuration.Height = PercentSize.CalculateValueFromPercent(s.Height)

        Me.Configuration.WindowY = s.Bottom - Me.Configuration.Height - 30
        Me.Configuration.WindowX = s.Right - Me.Configuration.Width - 30

        Me.Configuration.Fullscreen = False
        Me.Configuration.HostWindow = False
        Me.Configuration.AlwaysOnTop = True
        capture_menu.Checked = False

        SaveConfig()
        LoadConfig()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Configuration.HostWindow = False
        capture_menu.Checked = False
        MouseSelector.ShowDialog(Me)
        Configuration.Width = MouseSelector.WindowSize.Width
        Configuration.Height = MouseSelector.WindowSize.Height
        Configuration.WindowX = Math.Min(MouseSelector.StartBounds.X, MouseSelector.EndBounds.X)
        Configuration.WindowY = Math.Min(MouseSelector.StartBounds.Y, MouseSelector.EndBounds.Y)
        SaveConfig()
        LoadConfig()
        MouseSelector.Dispose()
    End Sub

    <DllImport("user32.dll")>
    Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Shared Function MoveWindow(hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Integer) As Boolean
    End Function

    <DllImport("USER32.DLL")>
    Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    <DllImport("USER32.DLL")>
    Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <DllImport("User32.dll")>
    Public Shared Function SetForegroundWindow(point As IntPtr) As Integer

    End Function

    Public Shared GWL_STYLE As Integer = -16
    Public Shared WS_CHILD As Integer = &H40000000
    Public Shared WS_BORDER As Integer = &H800000
    Public Shared WS_DLGFRAME As Integer = &H400000
    Public Shared WS_CAPTION As Integer = WS_BORDER Or WS_DLGFRAME

    <DllImport("USER32.DLL")>
    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True)>
    Private Shared Function FindWindowByCaption(ByVal ZeroOnly As IntPtr, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetMenu(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetMenuItemCount(ByVal hMenu As IntPtr) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function DrawMenuBar(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function RemoveMenu(ByVal hMenu As IntPtr, ByVal uPosition As UInteger, ByVal uFlags As UInteger) As Boolean
    End Function

    Public Shared MF_BYPOSITION As UInteger = &H400
    Public Shared MF_REMOVE As UInteger = &H1000

    Sub CaptureWindow(Proc As Process)

        'Proc.WaitForInputIdle()
        While Proc.MainWindowHandle = IntPtr.Zero
            Thread.Sleep(900)
            Proc.Refresh()
        End While

        SetParent(Proc.MainWindowHandle, window_cap.Handle)

        MoveWindow(Proc.MainWindowHandle, 0, 0, window_cap.FindForm.Width, window_cap.FindForm.Height, False)

        TabControl1.SelectedTab = TabPage3
        TabPage3.Text = Proc.MainWindowTitle.IfBlank("SCRCPY WINDOW")
    End Sub

    Private Sub ToolStripMenuItem13_Click(sender As Object, e As EventArgs) Handles autostart_menu.Click
        Me.Configuration.AutoStart = autostart_menu.Checked
        SaveConfig()
        LoadConfig()
    End Sub

    Private Sub ToolStripMenuItem14_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem14.Click
        Dim lineargs = Prompt("Type the arguments below.", "-s " + Me.device_id.Text & " ")
        If lineargs.IsNotBlank Then
            StartProccess(lineargs)
        End If
    End Sub

    Private Sub ToolStripMenuItem15_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem15.Click
        ConsoleControl1.ClearOutput()
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles capture_menu.CheckedChanged
        SHowHideTab3()
        Me.Configuration.HostWindow = capture_menu.Checked
        SaveConfig()
        LoadConfig()
    End Sub

    Sub SHowHideTab3()
        If capture_menu.Checked Then
            If TabControl1.TabPages.IndexOf(TabPage3) = -1 Then
                TabControl1.TabPages.Add(TabPage3)
            End If
        Else
            If TabControl1.TabPages.IndexOf(TabPage3) > -1 Then
                TabControl1.TabPages.Remove(TabPage3)
            End If
        End If
    End Sub

    Sub SHowHideTab1(show As Boolean)
        If show Then
            If TabControl1.TabPages.IndexOf(TabPage1) <= -1 Then
                TabControl1.TabPages.Insert(0, TabPage1)
            End If
            TabControl1.SelectedTab = TabPage1
        Else
            If TabControl1.TabPages.IndexOf(TabPage1) > -1 Then
                TabControl1.TabPages.Remove(TabPage1)
            End If
        End If
    End Sub

    Private Sub DeviceConsole_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.Configuration.HostWindow AndAlso Me.ConsoleControl1.IsProcessRunning Then
            StartProccess()
        End If
    End Sub

    Sub IniciarApp(package As String, Optional LineArgs As String = "")
        If ADBProc IsNot Nothing AndAlso ADBProc.HasExited = False Then
            ADBProc.Kill()
            ADBProc.Dispose()
        End If
        Dim d As New ProcessStartInfo(ScrcpyDirectory.FullName & "\adb.exe", "-s " & device_id.Text & $" shell monkey -p {package} -c android.intent.category.LAUNCHER 1")
        d.UseShellExecute = False
        d.RedirectStandardOutput = True
        d.RedirectStandardError = True
        d.RedirectStandardInput = True
        d.CreateNoWindow = True
        Process.Start(d)
        StartProccess(LineArgs)
    End Sub

    Property ADBProc As Process

    Sub ChangeToNullKeyboard()
        If NullKeyboard_menu.Enabled AndAlso Configuration.EnableNullKeyboard Then
            GetDefaultKeyboard()

            While ADBProc.HasExited = False
                Thread.Sleep(900)
            End While


            ADBProc = New Process()
            ADBProc.StartInfo = New ProcessStartInfo(ADBPath, "-s " & device_id.Text & "  shell ime set com.wparam.nullkeyboard/.NullKeyboard")
            ADBProc.StartInfo.UseShellExecute = False
            ADBProc.StartInfo.RedirectStandardOutput = True
            ADBProc.StartInfo.RedirectStandardError = True
            ADBProc.StartInfo.RedirectStandardInput = True
            ADBProc.StartInfo.CreateNoWindow = True
            ADBProc.Start()
            Thread.Sleep(900)
        End If
    End Sub

    Sub ChangeToDefaultKeyboard()
        If Configuration.DefaultKeyboard.IsNotBlank Then
            If ADBProc IsNot Nothing AndAlso ADBProc.HasExited = False Then
                ADBProc.Kill()
                ADBProc.Dispose()
            End If
            ADBProc = New Process()
            ADBProc.StartInfo = New ProcessStartInfo(ADBPath, "-s " & device_id.Text & "  shell ime set  " & Configuration.DefaultKeyboard)

            ADBProc.StartInfo.UseShellExecute = False
            ADBProc.StartInfo.RedirectStandardOutput = True
            ADBProc.StartInfo.RedirectStandardError = True
            ADBProc.StartInfo.RedirectStandardInput = True
            ADBProc.StartInfo.CreateNoWindow = True

            ADBProc.Start()
            Thread.Sleep(900)
        End If

    End Sub

    Sub GetDefaultKeyboard()
        If ADBProc IsNot Nothing AndAlso ADBProc.HasExited = False Then
            ADBProc.Kill()
            ADBProc.Dispose()
        End If
        ADBProc = New Process()
        ADBProc.StartInfo = New ProcessStartInfo(ADBPath, "-s " & device_id.Text & "  shell settings get secure default_input_method")
        ADBProc.StartInfo.UseShellExecute = False
        ADBProc.StartInfo.RedirectStandardOutput = True
        ADBProc.StartInfo.RedirectStandardError = True
        ADBProc.StartInfo.RedirectStandardInput = True
        ADBProc.StartInfo.CreateNoWindow = True
        AddHandler ADBProc.OutputDataReceived, Sub(sender, args)
                                                   If args.Data.IsNotBlank Then
                                                       Me.Configuration.DefaultKeyboard = args.Data
                                                   End If

                                               End Sub
        ADBProc.Start()
        Thread.Sleep(900)
        ADBProc.BeginOutputReadLine()

    End Sub

    Sub GetApps()
        If ADBProc IsNot Nothing AndAlso ADBProc.HasExited = False Then
            ADBProc.Kill()
            ADBProc.Dispose()
        End If
        ADBProc = New Process()
        ADBProc.StartInfo = New ProcessStartInfo(ADBPath, "-s " & device_id.Text & "  shell pm list packages -3 -f")
        ADBProc.StartInfo.UseShellExecute = False
        ADBProc.StartInfo.RedirectStandardOutput = True
        ADBProc.StartInfo.RedirectStandardError = True
        ADBProc.StartInfo.RedirectStandardInput = True
        ADBProc.StartInfo.CreateNoWindow = True

        AddHandler ADBProc.OutputDataReceived, Sub(sender, args)
                                                   If args.Data.IsNotBlank Then

                                                       Dim infos = args.Data.Split("base.apk=")
                                                       Dim app_package = infos.Last()
                                                       Dim app_path = infos.First() & "base.apk"
                                                       Dim appdomain = app_package.Split(".").Where(Function(x) x <> "com")
                                                       Dim cbd = New CallBackDel(Sub()
                                                                                     Dim appdev = appdomain.First().ToTitle()
                                                                                     Dim appname = appdomain.Last().ToTitle()

                                                                                     If app_package = "com.sentio.desktop" Then
                                                                                         WithSentioDesktopToolStripMenuItem.Enabled = True
                                                                                     End If
                                                                                     If app_package = "com.wparam.nullkeyboard" Then
                                                                                         NullKeyboard_menu.Enabled = True
                                                                                         NullKeyboard_menu.Checked = Me.Configuration.EnableNullKeyboard

                                                                                     End If

                                                                                     Dim DevMenu As ToolStripMenuItem = Nothing
                                                                                     For Each m As ToolStripMenuItem In AppMenu.DropDownItems
                                                                                         If m.Text = appdev Then
                                                                                             DevMenu = m
                                                                                             Exit For
                                                                                         End If
                                                                                     Next

                                                                                     If DevMenu Is Nothing Then
                                                                                         DevMenu = New ToolStripMenuItem(appdev)
                                                                                         AppMenu.DropDownItems.Add(DevMenu)
                                                                                     End If

                                                                                     Dim newAppMenu As New ToolStripMenuItem(appname)
                                                                                     DevMenu.DropDownItems.Add(newAppMenu)
                                                                                     AddHandler newAppMenu.Click, Sub() IniciarApp(app_package)

                                                                                 End Sub)
                                                       Me.Invoke(cbd)
                                                   End If

                                               End Sub

        ADBProc.Start()
        Thread.Sleep(1000)
        ADBProc.BeginOutputReadLine()

    End Sub

    Private Sub WithSentioDesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WithSentioDesktopToolStripMenuItem.Click
        IniciarApp("com.sentio.desktop", $"-s {device_id.Text} --fullscreen")
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles NullKeyboard_menu.Click
        Configuration.EnableNullKeyboard = NullKeyboard_menu.Checked
        SaveConfig()
        LoadConfig()
    End Sub

    Private Sub device_id_Click(sender As Object, e As EventArgs) Handles device_id.Click
        Clipboard.SetText(device_id.Text)
        Alert("Device serial copied to clipboard.")
    End Sub

    Private Sub ToolStripMenuItem1_Click_2(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Configuration.DefaultKeyboard = "com.google.android.inputmethod.latin/com.android.inputmethod.latin.LatinIME"
        ChangeToDefaultKeyboard()
        SaveConfig()
        LoadConfig()
    End Sub
End Class