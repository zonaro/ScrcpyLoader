Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Design
Imports InnerLibs

Friend Class CustomFileBrowser
    Inherits FileNameEditor

    Protected Overrides Sub InitializeDialog(openFileDialog As OpenFileDialog)
        MyBase.InitializeDialog(openFileDialog)
        openFileDialog.DefaultExt = ".mkv"
        openFileDialog.Multiselect = False
        openFileDialog.CheckFileExists = False
        openFileDialog.Title = "Save Video"
        openFileDialog.Filter = "Video File (*.mkv)|*.mkv"
    End Sub

End Class

Public Class ShortcutModEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)
        MyBase.FormCaption = "Shortcut MOD Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = False

    End Sub

End Class





