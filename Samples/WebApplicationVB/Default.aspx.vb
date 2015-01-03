Imports PaymillWrapper.Models

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
     Dim amount As String = CType(tbAmount.Text, Decimal) * 100

        Dim paymill As New PaymillWrapper.PaymillContext("YOUR PRIVATE KEY")

        Dim strvalue As String = hToken.Value

        Dim pm As Payment = paymill.PaymentService.CreateWithTokenAsync(strvalue).Result

        Dim am As Integer = Integer.Parse(amount)

        Dim trans As Transaction = paymill.TransactionService.CreateWithPaymentAsync(pm, am, tbCurrency.Text, "Test api vb.net").Result
        'Check Transaction ResponseCode for the result
        ' 

    End Sub
End Class