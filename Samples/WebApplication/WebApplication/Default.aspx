<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <script type="text/javascript">
        var PAYMILL_PUBLIC_KEY = 'YOUR PUBLIC KEY';
        var VALIDATE_CVC = true;
    </script>
    <link rel="stylesheet" type="text/css" href="css/paymill_styles.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://bridge.paymill.de/"></script>
    <script type="text/javascript" src="Scripts/creditcardBrandDetection.js"></script>
    <script type="text/javascript">
        $.noConflict();

        jQuery(document).ready(function ($) {
            var doc = document;
            var body = $(doc.body);
            $('.card-number').keyup(function () {
                var brand = paymill.cardType($('.card-number').val());
                brand = brand.toLowerCase();
                $(".card-number")[0].className = $(".card-number")[0].className.replace(/paymill-card-number-.*/g, '');
                if (brand !== 'unknown') {
                    $('#card-number').addClass("paymill-card-number-" + brand);
                }

                if (brand !== 'maestro') {
                    VALIDATE_CVC = true;
                } else {
                    VALIDATE_CVC = false;
                }
            });

            $('.card-expiry').keyup(function () {
                if (/^\d\d$/.test($('.card-expiry').val())) {
                    text = $('.card-expiry').val();
                    $('.card-expiry').val(text += "/");
                }
            });


            function PaymillResponseHandler(error, result) {
                if (error) {
                    $(".payment_errors").text(error.apierror);
                    $(".payment_errors").css("display", "inline-block");
                } else {
                    $(".payment_errors").css("display", "none");
                    $(".payment_errors").text("");
                    var form = $("#payment-form");
                    // Token
                    var token = result.token;
                    var tokenFieldId = '<%= hToken.ClientID %>';
                    $('#' + tokenFieldId).val(token);
                    var _id = $('#' + '<%= btnSubmit.ClientID %>').attr("name");
                    __doPostBack(_id.replace("_", "$"), '');
                }
                $(".submit-button").removeAttr("disabled");
            }

            $('#paymill-submit-button').click(function() {
                $('.submit-button').attr("disabled", "disabled");

                paymenttype = $('.switch_button_active').length ? $('.switch_button_active').val() : 'cc';
                var expiry = $('.card-expiry').val();
                expiry = expiry.split("/");
                if (expiry[1] && (expiry[1].length <= 2)) {
                    expiry[1] = '20' + expiry[1];
                }
                if (false === paymill.validateExpiry(expiry[0], expiry[1])) {
                    $(".payment_errors").text("invalid-card-expiry-date");
                    $(".payment_errors").css("display", "inline-block");
                    $(".submit-button").removeAttr("disabled");
                    return false;
                }
                var params = {
                    amount_int: $('.amount').val() * 100,  // E.g. "15" for 0.15 Eur
                    currency: $('.currency').val(),    // ISO 4217 e.g. "EUR"
                    number: $('.card-number').val(),
                    exp_month: expiry[0],
                    exp_year: expiry[1],
                    cvc: $('.card-cvc').val(),
                    cardholder: $('.card-holdername').val()
                };
    
                paymill.createToken(params, PaymillResponseHandler);
                event.preventDefault();
            });
        });
    </script>
</head>
<body>
    <form id="paymentForm" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" EnablePartialRendering="true">
         </asp:ScriptManager>
            <h1>
                Title stands here
            </h1>
            <div class="payment_errors">&nbsp;</div>
            <fieldset>
                <label for="card-number" class="card-number-label field-left"></label>
                <asp:TextBox Id="tbCardNumber" class="card-number field-left"  runat="server"
                        placeholder="**** **** **** ****" maxlength="19"/>
                <label for="card-expiry" class="card-expiry-label field-right"></label>
                <asp:TextBox Id="tbcardExpiry" class="card-expiry field-right" runat="server"
                          placeholder="MM/YY" maxlength="7"/>
            </fieldset>
            <fieldset>
                <label for="card-holdername" class="card-holdername-label field-left"></label>
                <asp:TextBox Id="tbCcardHoldername" class="card-holdername field-left" placeholder="Card Holder"  runat="server"/>
                <label for="card-cvc" class="field-right"><span class="card-cvc-label"></span>
                    </label>
                <asp:TextBox Id="tbCardCvc" class="card-cvc field-right" runat="server"
                    placeholder="CVC" maxlength="4"/>
            </fieldset>
            <fieldset>
                <label for="amount" class="amount-label field-left"></label>
                <asp:TextBox Id="tbAmount" class="amount field-left"  Text="10"
                     name="amount" runat="server" />
                <label for="currency" class="currency-label field-right"></label>
                <asp:TextBox Id="tbCurrency" class="currency field-right" value="EUR" name="currency" runat="server" />
            </fieldset>
            <fieldset id="buttonWrapper">
                <button id="paymill-submit-button" class="submit-button btn btn-primary" type="button"  >Submit</button>
                <asp:Button id="btnSubmit" runat="server" Text="Submit" style="display:none" OnClick="btnSubmit_Click" />
            </fieldset>
            <asp:HiddenField ID="hToken" runat="server" />

    </form>
</body>
</html>
