Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Threading
Imports InnerLibs
Imports Newtonsoft.Json
Imports RestSharp
Imports SharpAdbClient

Module GlobalVar

    Public Function CheckForScrcpyUpdate(NotifyIcon1 As NotifyIcon) As Boolean
        Try
            Dim URL = "https://api.github.com/repos/Genymobile/scrcpy/releases/latest"
            Dim client = New RestClient()
            Dim response = client.Execute(New RestRequest(URL, Method.GET))
            If response.IsSuccessful Then
                GenyGithubInfo = JsonConvert.DeserializeObject(Of GithubRelease)(response.Content)
                If ScrcpyVersion <> "NOT INSTALLED" AndAlso New Version(GenyGithubInfo.tag_name.Trim("v")) <= New Version(ScrcpyVersion) Then
                    NotifyIcon1.ShowBalloonTip(900, "Update", "You are up to date", ToolTipIcon.Info)
                    Return False
                End If
            Else
                NotifyIcon1.ShowBalloonTip(900, "Ops", "Can't connect to GitHub repo", ToolTipIcon.Warning)
                Return False
            End If
        Catch ex As Exception
            If NotifyIcon1 IsNot Nothing Then
                NotifyIcon1.ShowBalloonTip(900, "Error", ex.ToFullExceptionString, ToolTipIcon.Error)
            Else
                Alert(ex.ToFullExceptionString)
            End If
            Return False
        End Try
        Return True
    End Function




    Public Function CheckForLoaderUpdate(NotifyIcon1 As NotifyIcon) As Boolean
        Try
            Dim URL = "https://api.github.com/repos/zonaro/scrcpyloader/releases/latest"
            Dim client = New RestClient()
            Dim response = client.Execute(New RestRequest(URL, Method.GET))
            If response.IsSuccessful Then
                ZonaroGithubInfo = JsonConvert.DeserializeObject(Of GithubRelease)(response.Content)
                If New Version(ZonaroGithubInfo.tag_name) <= New Version(My.Forms.Main.ProductVersion) Then
                    NotifyIcon1.ShowBalloonTip(900, "Update", "You are up to date", ToolTipIcon.Info)
                    Return False
                End If
            Else
                NotifyIcon1.ShowBalloonTip(900, "Ops", "Can't connect to GitHub repo", ToolTipIcon.Warning)
                Return False
            End If
        Catch ex As Exception
            If NotifyIcon1 IsNot Nothing Then
                NotifyIcon1.ShowBalloonTip(900, "Error", ex.ToFullExceptionString, ToolTipIcon.Error)
            Else
                Alert(ex.ToFullExceptionString)
            End If
            Return False
        End Try
        Return True
    End Function

    ReadOnly Property Arguments As String()
        Get
            Dim a = Environment.GetCommandLineArgs()
            If a.Count > 1 Then
                Return a.Skip(1).ToArray()
            End If
            Return {}
        End Get
    End Property

    ReadOnly Property ScrcpyDirectory As DirectoryInfo
        Get
            Return ScrcpyDirectories.FirstOrDefault()
        End Get
    End Property

    ReadOnly Property ADBPath As String
        Get
            If ScrcpyDirectory IsNot Nothing Then Return ScrcpyDirectory.FullName & "\adb.exe"
            Return Nothing
        End Get
    End Property

    ReadOnly Property ScrcpyPath As String
        Get
            If ScrcpyDirectory IsNot Nothing Then Return ScrcpyDirectory.FullName & "\scrcpy.exe"
            Return Nothing
        End Get
    End Property

    ReadOnly Property ADBExists As Boolean
        Get
            Return ADBPath IsNot Nothing AndAlso File.Exists(ADBPath)
        End Get
    End Property

    ReadOnly Property ScrcpyExists As Boolean
        Get
            Return ScrcpyPath IsNot Nothing AndAlso File.Exists(ScrcpyPath)
        End Get
    End Property

    ReadOnly Property ScrcpyDirectories As IEnumerable(Of DirectoryInfo)
        Get
            Return CurDir().ToDirectoryInfo.GetDirectories("*scrcpy-*").OrderByDescending(Function(x) x.CreationTime)
        End Get
    End Property

    ReadOnly Property ScrcpyVersion As String
        Get
            If ScrcpyExists AndAlso ADBExists Then
                Return ScrcpyDirectory.FullName.Split("-").LastOrDefault()?.Trim.Trim("v")
            End If
            Return "NOT INSTALLED"
        End Get
    End Property

    Property GenyGithubInfo As GithubRelease
    Property ZonaroGithubInfo As GithubRelease
    Property ADB As AdbClient

    Property ADBServer As AdbServer

End Module

Public Class Main

    WithEvents monitor As DeviceMonitor

    Private Sub StartMonitor()
        monitor = New DeviceMonitor(New AdbSocket(New IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)))
        AddHandler monitor.DeviceConnected, AddressOf OnDeviceConnected
        AddHandler monitor.DeviceDisconnected, AddressOf OnDeviceDisconnected
        Dim t = New Thread(Sub() monitor.Start())
        t.Start()
    End Sub

    Delegate Sub TabCallBack()

    Sub AddAba(e As DeviceDataEventArgs)
        Dim device = ADB.GetDevices().FirstOrDefault(Function(x) x.Serial = e.Device.Serial)
        NotifyIcon1.ShowBalloonTip(900, "New Device", $"The device {device.Name} has connected to this PC", ToolTipIcon.Info)
        TabControl1.TabPages.Add(CreateDevicePage(device))
    End Sub

    Sub RemoveAba(e As DeviceDataEventArgs)

        NotifyIcon1.ShowBalloonTip(900, "Device", $"The device has disconnected", ToolTipIcon.Warning)
        Dim l = New List(Of TabPage)
        For Each t In TabControl1.TabPages
            If t.Name = e.Device.Serial Then
                l.Add(t)
            End If
        Next
        For Each t In l
            TabControl1.TabPages.Remove(t)
            t.Dispose()
        Next
    End Sub

    Private Sub OnDeviceConnected(ByVal sender As Object, ByVal e As DeviceDataEventArgs)

        If (Me.TabControl1.InvokeRequired) Then
            Dim d = New TabCallBack(Sub() AddAba(e))
            Me.Invoke(d)
        Else
            AddAba(e)
        End If

    End Sub

    Private Sub OnDeviceDisconnected(ByVal sender As Object, ByVal e As DeviceDataEventArgs)
        If (Me.TabControl1.InvokeRequired) Then
            Dim d = New TabCallBack(Sub() RemoveAba(e))
            Me.Invoke(d)
        Else
            RemoveAba(e)
        End If
    End Sub

    <DllImport("user32.dll")> Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Shared Function MoveWindow(hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Integer) As Boolean
    End Function
    Sub CaptureWindow(Proc As Process)

        Proc.WaitForInputIdle()
        While Proc.MainWindowHandle = IntPtr.Zero
            Thread.Sleep(900)
            Proc.Refresh()
        End While

        Dim tab = New TabPage
        tab.Name = Proc.Id
        tab.Text = Proc.MainWindowTitle
        SetParent(Proc.MainWindowHandle, tab.Handle)
        MoveWindow(Proc.MainWindowHandle, 0, 0, tab.FindForm.Width, tab.FindForm.Height, False)
        TabControl1.TabPages.Add(tab)
        TabControl1.SelectedTab = tab

    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckUpdate()

        If ScrcpyExists = False Then
            NotifyIcon1.Visible = True
            NotifyIcon1.ShowBalloonTip(900, "Scrcpy not found", "Please, re-install Scrcpy from update panel", ToolTipIcon.Error)
            UpdateScrcpy.ShowDialog()
        End If

        If ADBExists = False Then
            NotifyIcon1.Visible = True
            NotifyIcon1.ShowBalloonTip(900, "ADB not found", "Please, re-install Scrcpy from update panel", ToolTipIcon.Error)
            UpdateScrcpy.ShowDialog()

        End If

        If (ScrcpyExists AndAlso ADBExists) Then
            ADB = New AdbClient()
            ADBServer = New AdbServer()
            ADBServer.StartServer(ADBPath, False)
            StartMonitor()
            NotifyIcon1.Visible = True
            ToolStripStatusLabel1.Text = "Scrcpy Loader v" & Me.ProductVersion & " / Scrcpy v" & ScrcpyVersion
        End If

    End Sub

    Sub RefreshDevices()
        Try
            DestroyProccess()
            Dim devices = ADB.GetDevices()
            If TabControl1.TabPages.Count > 0 Then
                TabControl1.TabPages.Clear()
            End If
            If devices.Count > 0 Then
                For Each device In devices
                    TabControl1.TabPages.Add(CreateDevicePage(device, True))
                Next
            Else
                NotifyIcon1.ShowBalloonTip(900, "No devices detected", "Remember to enable USB debug on your devices", ToolTipIcon.Error)
            End If
        Catch ex As Exception
            NotifyIcon1.ShowBalloonTip(900, "Error", ex.ToFullExceptionString, ToolTipIcon.Error)
        End Try
    End Sub

    Function CreateDevicePage(device As DeviceData, Optional StartWithArguments As Boolean = False) As TabPage
        Dim pagina = New TabPage(device.Model.Replace("_", " ") & " " & device.Product.Quote("("))
        pagina.Name = device.Serial
        Dim DEVC = New DeviceConsole() With {.Dock = DockStyle.Fill}
        DEVC.Device = device
        DEVC.device_id.Text = device.Serial
        DEVC.StartWithArguments = StartWithArguments

        pagina.Controls.Add(DEVC)

        Return pagina
    End Function


    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click, ToolStripMenuItem9.Click
        DestroyProccess()
    End Sub

    Sub DestroyProccess()
        Dim consoles = Me.GetAllControls(Of ConsoleControl.ConsoleControl)()
        For Each c In consoles
            If c.ProcessInterface IsNot Nothing AndAlso c.IsProcessRunning Then
                c.StopProcess()
            End If
        Next
        For Each p In Process.GetProcessesByName("scrcpy.exe")
            p.Kill()
        Next
    End Sub

    Public Sub ForceClose()
        Environment.Exit(1)
    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Select Case MsgBox("Close all Scrcpy proccess before exit?", MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    DestroyProccess()
                    Application.Exit()
                Case MsgBoxResult.No
                    Application.Exit()
                Case Else
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            If Me.Visible Then
                If Me.WindowState = FormWindowState.Minimized Then
                    Me.WindowState = FormWindowState.Normal
                Else
                    Me.Hide()
                End If
            Else
                If Me.WindowState = FormWindowState.Minimized Then
                    Me.WindowState = FormWindowState.Normal
                End If
                Me.Show()
                Me.Activate()
            End If
        End If

    End Sub

    Sub CheckUpdate()
        If CheckForLoaderUpdate(NotifyIcon1) Then
            UpdateLoader.ShowDialog()
        End If
        If CheckForScrcpyUpdate(NotifyIcon1) Then
            UpdateScrcpy.ShowDialog()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click, ToolStripMenuItem7.Click
        CheckUpdate()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        RefreshDevices()
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click
        Me.Hide()
    End Sub

    Private Sub StayOnTopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StayOnTopToolStripMenuItem.Click
        Me.TopMost = StayOnTopToolStripMenuItem.Checked
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        Process.Start(CurDir())
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click
        Dim p = TabControl1.SelectedTab
        If p IsNot Nothing Then
            Dim c As DeviceConsole = p.Controls(0)
            TabControl1.TabPages.Add(CreateDevicePage(c.Device))
        End If

    End Sub

    Private Sub ToolStripMenuItem11_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem11.Click
        Dim p = TabControl1.SelectedTab
        If p IsNot Nothing Then
            p.Dispose()
        End If
    End Sub
End Class