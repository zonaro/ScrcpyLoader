Imports System.IO
Imports System.Threading
Imports InnerLibs
Imports InnerLibs.HtmlParser
Imports Markdig
Imports RestSharp

Public Class UpdateLoader

    Private Sub UpdatePy_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If ZonaroGithubInfo IsNot Nothing Then
            Dim pipeline = New MarkdownPipelineBuilder().UseAdvancedExtensions().Build()
            WebBrowser1.Navigate("about:blank")
            Dim styles = New InnerLibs.HtmlParser.HtmlElement("link").AddAttribute("rel", "stylesheet").AddAttribute("href", "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/4.0.0/github-markdown.min.css")
            Dim doc = New HtmlDocument()
            doc.Body.AddAttribute("class", "markdown-body")
            doc.Body.InnerHTML = "<h1>Update Info</h1><hr>" & Markdown.ToHtml(ZonaroGithubInfo.body, pipeline)
            doc.Head.AddNode(styles.ToString)
            WebBrowser1.Document.Write(doc.ToString)
        End If

    End Sub
    Sub goupdate()
        Try
            If ZonaroGithubInfo IsNot Nothing Then
                Dim url = ZonaroGithubInfo.assets.FirstOrDefault(Function(x) x.browser_download_url.ContainsAll(StringComparison.OrdinalIgnoreCase, "msi")).browser_download_url
                If url.IsNotBlank() Then
                    Me.UPDATENOWToolStripMenuItem.Text = "Updating Scrcpy Loader... Please Wait"
                    Me.UPDATENOWToolStripMenuItem.Enabled = False
                    Dim setupfile = New FileInfo((My.Computer.FileSystem.SpecialDirectories.Temp & "\" & ZonaroGithubInfo.tag_name.Replace(".", "\")).ToDirectoryInfo().FullName & "\" & Path.GetFileName(url))
                    If setupfile.Exists Then
                        setupfile.DeleteIfExist
                    End If
                    Notify("Downloading Latest Version")
                    setupfile = New RestClient().DownloadData(New RestRequest(url, Method.GET)).WriteToFile(setupfile.FullName)
                    If setupfile.Exists Then

                        Process.Start(setupfile.FullName)
                        Me.Hide()
                        Thread.Sleep(900)

                        Main.ForceClose()

                    End If
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

End Class