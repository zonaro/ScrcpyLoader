Imports InnerLibs
Public Class MouseSelector

    Public Property StartBounds As Point
    Public Property EndBounds As Point

    Public Property WindowSize As Size
    Public Property isclicking As Boolean

    Private Sub MouseSelector_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        StartBounds = MousePosition
        isclicking = True
    End Sub

    Private Sub MouseSelector_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        isclicking = False

        EndBounds = MousePosition
        WindowSize = New Size({StartBounds.X, EndBounds.X}.Max() - {StartBounds.X, EndBounds.X}.Min(), {StartBounds.Y, EndBounds.Y}.Max() - {StartBounds.Y, EndBounds.Y}.Min())

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub MouseSelector_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Invalidate()
    End Sub

    Private Sub MouseSelector_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint

        If (isclicking) Then
            Dim r = New Region(Me.ClientRectangle)
            Dim window = New Rectangle(Math.Min(StartBounds.X, MousePosition.X), Math.Min(StartBounds.Y, MousePosition.Y), Math.Abs(StartBounds.X - MousePosition.X), Math.Abs(StartBounds.Y - MousePosition.Y))
            r.Xor(window)
            e.Graphics.FillRegion(Brushes.Gray, r)

        End If
    End Sub

    Private Sub MouseSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = "Select window area".DrawImage(New Font("Arial", "30"))
        Me.BackgroundImageLayout = ImageLayout.Center
    End Sub
End Class