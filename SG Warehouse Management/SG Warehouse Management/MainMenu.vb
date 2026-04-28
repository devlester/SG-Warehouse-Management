Public Class MainMenu

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rcv_btn.Click
        Me.Hide()
        ReceiveOrdersForm.Show()

    End Sub

    Private Sub MainMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub pickingBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pickingBtn.Click

        PickLocationForm.Show()
        Me.Hide()
    End Sub
End Class