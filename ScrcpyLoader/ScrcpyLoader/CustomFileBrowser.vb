Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Design

Friend Class CustomFileBrowser
    Inherits FileNameEditor

    Protected Overrides Sub InitializeDialog(openFileDialog As OpenFileDialog)
        MyBase.InitializeDialog(openFileDialog)
        openFileDialog.CheckFileExists = False
        openFileDialog.Title = "Save Video"
        openFileDialog.Filter = "Video File (*.mkv)|*.mkv"
    End Sub

End Class





