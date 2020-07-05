Imports System.IO
Imports InnerLibs
Imports InnerLibs.HtmlParser
Imports Markdig
Imports RestSharp

Public Class UpdateScrcpy

    Private Sub UpdatePy_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If GenyGithubInfo IsNot Nothing Then
            Dim pipeline = New MarkdownPipelineBuilder().UseAdvancedExtensions().Build()
            WebBrowser1.Navigate("about:blank")
            Dim styles = New InnerLibs.HtmlParser.HtmlElement("link").AddAttribute("rel", "stylesheet").AddAttribute("href", "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/4.0.0/github-markdown.min.css")
            Dim doc = New HtmlDocument()
            doc.Body.AddAttribute("class", "markdown-body")
            doc.Body.InnerHTML = "<h1>Update Info</h1><hr>" & Markdown.ToHtml(GenyGithubInfo.body, pipeline)
            doc.Head.AddNode(styles.ToString)
            WebBrowser1.Document.Write(doc.ToString)
        End If


        If ScrcpyVersion = "NOT INSTALLED" Then
            If MsgBox("Scrcpy is not installed. Download and Install latest version?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                CheckForScrcpyUpdate(Nothing)
                goupdate()
            End If
        End If

    End Sub




    Sub goupdate()
        Try
            If GenyGithubInfo IsNot Nothing Then
                Dim v = "32"
                If Environment.Is64BitOperatingSystem Then
                    v = "64"
                End If
                Dim url = GenyGithubInfo.assets.FirstOrDefault(Function(x) x.browser_download_url.ContainsAll(StringComparison.OrdinalIgnoreCase, "win", v, "zip")).browser_download_url
                If url.IsNotBlank() Then
                    Me.UPDATENOWToolStripMenuItem.Text = "Updating Scrcpy... Please Wait"
                    Me.UPDATENOWToolStripMenuItem.Enabled = False
                    Dim zipfile = New FileInfo(My.Computer.FileSystem.SpecialDirectories.Temp.ToDirectoryInfo().FullName & "\" & Path.GetFileName(url))
                    If zipfile.Exists = False Then
                        Notify("Downloading Latest Version")
                        zipfile = New RestClient().DownloadData(New RestRequest(url, Method.GET)).WriteToFile(zipfile.FullName)
                    End If
                    If ScrcpyDirectories.Count > 0 Then
                        Notify("Removing Old Version")
                        For Each item In ScrcpyDirectories
                            item.DeleteIfExist
                        Next
                    End If
                    Notify("Extracting Files")
                    zipfile.ExtractZipFile(CurDir().ToDirectoryInfo())
                    Alert("Update Complete")
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Alert(ex.ToFullExceptionString)
            Me.UPDATENOWToolStripMenuItem.Text = "UPDATE NOW"
            Me.UPDATENOWToolStripMenuItem.Enabled = True
        End Try
    End Sub
    Private Sub UPDATENOWToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UPDATENOWToolStripMenuItem.Click
        goupdate()
    End Sub

    Private Sub UpdatePy_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ScrcpyVersion = "NOT INSTALLED" Then
            Alert("Scrcpy is not Installed!")
            Application.Exit()
        End If
    End Sub
End Class