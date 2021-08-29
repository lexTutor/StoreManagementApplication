<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authenticate.aspx.cs" Inherits="StoreApp.NetFramework.Authentication.Authenticate" %>

<!DOCTYPE html>
<link href="../Content/AuthStyle.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div class="login-wrap">
	<div class="login-html">
		<input id="SignIn" type="radio" name="tab" class="sign-in" checked  runat="server" onclick="FocusSignIn()"/>
		<label for="SignIn" class="tab">Sign In</label>

		<input id="SignUp" type="radio" name="tab" class="sign-up" runat="server" onclick="FocusSignup()"/>
		<label for="SignUp" class="tab">Sign Up</label>

		<div class="login-form">
			<div class="sign-in-htm">
				<div class="group">
					<label for="UserName" class="label">
						Username
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_UserName" runat="server"
							ErrorMessage="Please enter your username"
							ControlToValidate="UserName" Display="Static"
							Font-Bold="true" ForeColor="Pink" CssClass="label">
						</asp:RequiredFieldValidator>
					</label>
					<input id="UserName" type="text"  runat="server" class="input"/>
				</div>
				<div class="group">
					<label for="Password" class="label">Password
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_Password" runat="server"
							ErrorMessage="Please enter your password"
							ControlToValidate="Password" Display="Static"
							Font-Bold="true" ForeColor="Pink" CssClass="label">
						</asp:RequiredFieldValidator>
					</label>
					<input id="Password" type="password" class="input" data-type="password" runat="server"/>
				</div>
				<div class="group">
					<input id="check" type="checkbox" class="check" checked="True" runat="server"/>
					<label for="check">
						<span class="icon">
						</span> 
						Keep me Signed in
					</label>
				</div>
				<div class="group">
					<button type="submit" class="button" runat="server">Sign In</button>
				</div>
				<div class="hr"></div>
				<div class="foot-lnk">
					<a href="#forgot">Forgot Password?</a>
				</div>
			</div>

			<div class="sign-up-htm">
				<div class="group">
					<label for="NewUsername" class="label">Username
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_NewUserName" runat="server"
							ErrorMessage="Please enter a valid user name" 
							ControlToValidate="NewUserName" Display="Dynamic"
							Font-Bold="true" ForeColor="Pink" CssClass="label" >
						</asp:RequiredFieldValidator>
                    </label>
					&nbsp;<input id="NewUsername" type="text" class="input"  runat="server"/>
				</div>
				<div class="group">
					<label for="NewPassword" class="label">Password
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_NewPassword" runat="server"
							ErrorMessage="Please enter a password"
							ControlToValidate="NewPassword" Display="Dynamic"
							Font-Bold="true" ForeColor="Pink" CssClass="label">
						</asp:RequiredFieldValidator>
                    </label>
					&nbsp;<input id="NewPassword" type="password" class="input" data-type="password" runat="server"/>
				</div>
				<div class="group">
					<label for="Confirm" class="label">Repeat Password
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_ConfirmPassword" runat="server"
							ErrorMessage="Please confirm your password"
							ControlToValidate="Confirm" Display="Dynamic"
							Font-Bold="true" ForeColor="Pink" CssClass="label">
						</asp:RequiredFieldValidator>
						 <asp:CompareValidator ID="CompareValidator_NewPassword" 
							runat="server" ControlToCompare="Confirm" 
							ControlToValidate="NewPassword" Display="Dynamic"
							Font-Bold="true" ForeColor="Pink" CssClass="label"
							ErrorMessage="Passwords do not match">
						</asp:CompareValidator>
					</label>
					<input id="Confirm" type="password" class="input" data-type="password" runat="server"/>
				</div>
				<div class="group">
					<label for="EmailAddress" class="label">Email Address
						<asp:RequiredFieldValidator ID="RequiredFieldValidator_EmailAddress" runat="server"
							ErrorMessage="Please enter a valid email address (name@domain.com)"
							ControlToValidate="EmailAddress" Display="Static"
							Font-Bold="true" ForeColor="Pink" CssClass="label">
						</asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator
							ID="RegularExpressionValidator_Email" runat="server"  ControlToValidate="EmailAddress"
							Font-Bold="true" ForeColor="Tomato" Display="Static"
							ErrorMessage="PleaseEnter a valid email address"
							ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
						</asp:RegularExpressionValidator>
					</label>
					<input id="EmailAddress" type="text" class="input" runat="server"/>
				</div>
				<div class="group">
					<input type="submit" class="button" value="Sign Up" runat="server"/>
				</div>
			</div>
		</div>
	</div>
</div>
    </form>
</body>
	<script type="text/javascript">
		document.getElementById("UserName").focus();

		function FocusSignIn() {
            document.getElementById("UserName").focus();
        }

        //function FocusSignup() {
        //    document.getElementById("NewUsername").focus();
        //}
    </script>
</html>
