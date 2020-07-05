<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DeviceConsole
    Inherits System.Windows.Forms.UserControl

    'O UserControl substitui o descarte para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.device_id = New System.Windows.Forms.ToolStripMenuItem()
        Me.start_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.WithSentioDesktopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureInPictureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OtherPiPSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.capture_menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.autostart_menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.HELPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Configs = New System.Windows.Forms.PropertyGrid()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ConsoleControl1 = New ConsoleControl.ConsoleControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.window_cap = New System.Windows.Forms.Panel()
        Me.NullKeyboard_menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.device_id, Me.start_bt, Me.ToolStripMenuItem2})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(648, 24)
        Me.MenuStrip2.TabIndex = 3
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'device_id
        '
        Me.device_id.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.device_id.Name = "device_id"
        Me.device_id.Size = New System.Drawing.Size(63, 20)
        Me.device_id.Text = "deviceid"
        '
        'start_bt
        '
        Me.start_bt.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AppMenu, Me.WithSentioDesktopToolStripMenuItem})
        Me.start_bt.Name = "start_bt"
        Me.start_bt.Size = New System.Drawing.Size(50, 20)
        Me.start_bt.Text = "START"
        '
        'AppMenu
        '
        Me.AppMenu.Name = "AppMenu"
        Me.AppMenu.Size = New System.Drawing.Size(181, 22)
        Me.AppMenu.Text = "With App"
        '
        'WithSentioDesktopToolStripMenuItem
        '
        Me.WithSentioDesktopToolStripMenuItem.Enabled = False
        Me.WithSentioDesktopToolStripMenuItem.Name = "WithSentioDesktopToolStripMenuItem"
        Me.WithSentioDesktopToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.WithSentioDesktopToolStripMenuItem.Text = "With Sentio Desktop"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem4, Me.ToolStripSeparator2, Me.ToolStripMenuItem3, Me.PictureInPictureToolStripMenuItem, Me.ToolStripSeparator1, Me.capture_menu, Me.autostart_menu, Me.ToolStripSeparator3, Me.HELPToolStripMenuItem, Me.ToolStripMenuItem14, Me.ToolStripMenuItem15, Me.ToolStripSeparator4, Me.NullKeyboard_menu, Me.ToolStripMenuItem1})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(68, 20)
        Me.ToolStripMenuItem2.Text = "OPTIONS"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(295, 22)
        Me.ToolStripMenuItem4.Text = "Set Default Scrcpy Confguration (No args)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(292, 6)
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(295, 22)
        Me.ToolStripMenuItem3.Text = "Define Window Size and Position"
        '
        'PictureInPictureToolStripMenuItem
        '
        Me.PictureInPictureToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OtherPiPSizeToolStripMenuItem, Me.ToolStripMenuItem5, Me.ToolStripMenuItem7, Me.ToolStripMenuItem8, Me.ToolStripMenuItem10, Me.ToolStripMenuItem9, Me.ToolStripMenuItem11})
        Me.PictureInPictureToolStripMenuItem.Name = "PictureInPictureToolStripMenuItem"
        Me.PictureInPictureToolStripMenuItem.Size = New System.Drawing.Size(295, 22)
        Me.PictureInPictureToolStripMenuItem.Text = "Load Picture In Picture Presets"
        '
        'OtherPiPSizeToolStripMenuItem
        '
        Me.OtherPiPSizeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1})
        Me.OtherPiPSizeToolStripMenuItem.Name = "OtherPiPSizeToolStripMenuItem"
        Me.OtherPiPSizeToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.OtherPiPSizeToolStripMenuItem.Text = "Other PiP Size"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem5.Text = "5%"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem7.Text = "10%"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem8.Text = "25%"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem10.Text = "75%"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem9.Text = "50%"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItem11.Text = "100%"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(292, 6)
        '
        'capture_menu
        '
        Me.capture_menu.CheckOnClick = True
        Me.capture_menu.Name = "capture_menu"
        Me.capture_menu.Size = New System.Drawing.Size(295, 22)
        Me.capture_menu.Text = "Capture Scrcpy Window"
        '
        'autostart_menu
        '
        Me.autostart_menu.CheckOnClick = True
        Me.autostart_menu.Name = "autostart_menu"
        Me.autostart_menu.Size = New System.Drawing.Size(295, 22)
        Me.autostart_menu.Text = "AutoStart on connect"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(292, 6)
        '
        'HELPToolStripMenuItem
        '
        Me.HELPToolStripMenuItem.Name = "HELPToolStripMenuItem"
        Me.HELPToolStripMenuItem.Size = New System.Drawing.Size(295, 22)
        Me.HELPToolStripMenuItem.Text = "Print Scrcpy Help on Console"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(295, 22)
        Me.ToolStripMenuItem14.Text = "Start Custom Scrcpy Process"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(295, 22)
        Me.ToolStripMenuItem15.Text = "Clear Console"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(648, 576)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Configs)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(640, 550)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "START OPTIONS"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Configs
        '
        Me.Configs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Configs.Location = New System.Drawing.Point(3, 3)
        Me.Configs.Name = "Configs"
        Me.Configs.Size = New System.Drawing.Size(634, 544)
        Me.Configs.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ConsoleControl1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(640, 550)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "CONSOLE"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ConsoleControl1
        '
        Me.ConsoleControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConsoleControl1.IsInputEnabled = True
        Me.ConsoleControl1.Location = New System.Drawing.Point(3, 3)
        Me.ConsoleControl1.Name = "ConsoleControl1"
        Me.ConsoleControl1.SendKeyboardCommandsToProcess = False
        Me.ConsoleControl1.ShowDiagnostics = False
        Me.ConsoleControl1.Size = New System.Drawing.Size(634, 544)
        Me.ConsoleControl1.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.window_cap)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(640, 550)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "SCRCPY WINDOW"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'window_cap
        '
        Me.window_cap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.window_cap.Location = New System.Drawing.Point(3, 3)
        Me.window_cap.Name = "window_cap"
        Me.window_cap.Size = New System.Drawing.Size(634, 544)
        Me.window_cap.TabIndex = 0
        '
        'NullKeyboard_menu
        '
        Me.NullKeyboard_menu.CheckOnClick = True
        Me.NullKeyboard_menu.Enabled = False
        Me.NullKeyboard_menu.Name = "NullKeyboard_menu"
        Me.NullKeyboard_menu.Size = New System.Drawing.Size(295, 22)
        Me.NullKeyboard_menu.Text = "Auto Enable NULL Keyboard if Installed"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(292, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(295, 22)
        Me.ToolStripMenuItem1.Text = "Force Google Board as Default IME"
        '
        'DeviceConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Name = "DeviceConsole"
        Me.Size = New System.Drawing.Size(648, 600)
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents start_bt As ToolStripMenuItem
    Friend WithEvents device_id As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Configs As PropertyGrid
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ConsoleControl1 As ConsoleControl.ConsoleControl
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents PictureInPictureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As ToolStripMenuItem
    Friend WithEvents OtherPiPSizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents autostart_menu As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents HELPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents capture_menu As ToolStripMenuItem
    Friend WithEvents window_cap As Panel
    Friend WithEvents AppMenu As ToolStripMenuItem
    Friend WithEvents WithSentioDesktopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NullKeyboard_menu As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
