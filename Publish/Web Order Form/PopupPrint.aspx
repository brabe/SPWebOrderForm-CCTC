<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewOrderForm.aspx.cs" Inherits="BrentRabe.WebOrderEntry.WebOrderForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Order Request</title>
    <link href="Style/OrderForm.css" rel="Stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Oranienbaum|Source+Sans+Pro" rel="stylesheet"/>
    <script language="javascript" src="Scripts/OrderForm.js" type="text/javascript"></script>
</head>
<body>
    <div id="content">
        <div id="woe" runat="server">
            <!-- 
            BEGIN FORM
            --------------------------------------------------------------------
            WARNING: REMOVING OF ANY OF THE FOLLOWING ELEMENTS WILL CAUSE THE APPLICATION TO FAIL: 
            --------------------------------------------------------------------
            PROPSTRE
            CONTACTNAM 
            CONTACTADR1
            CONTACTADR2CITY 
            CONTACTADR2STATE
            CONTACTADR2ZIP
            CONTACTPH
            CONTACTEMAIL 
            -->
            <form id="ProFormWebOrderEntry" runat="server">
            <!-- INPUT FIELDS -->
            <fieldset>
                <legend>Order Contact Information</legend>
                <p>
                    <label class="left" for="CONTACTNAM">
                        Name:
                    </label>
                    <!-- DO NOT REMOVE CONTACTNAM -->
                    <asp:TextBox ID="CONTACTNAM" Columns="40" runat="server" ToolTip="Example: Fred Jones" Width="425px" />
                </p>
                <p>
                    <label class="left" for="CONTACTTYPE">
                        Your Role:
                    </label>
                    <asp:DropDownList ID="CONTACTTYPE" runat="server"  onChange="setContactInformation(this.value)" ToolTip="Select an option to copy information entered above into Contact Information fields">
                        <asp:ListItem Value="NONE">Other</asp:ListItem>
                        <asp:ListItem Value="LEN">Lender</asp:ListItem>
                        <asp:ListItem Value="MTB">Mortgage Broker</asp:ListItem>
                        <asp:ListItem Value="AG701">Listing Agent</asp:ListItem>
                        <asp:ListItem Value="AG702">Selling Agent</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <p>
                    <label class="left" for="CONTACTADR1">
                        Address Line 1:
                    </label>
                    <!-- DO NOT REMOVE CONTACTADR1 -->
                    <asp:TextBox ID="CONTACTADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street" Width="425px" />
                </p>
                <p>
                    <label class="left" for="CONTACTADR2CITY">
                        City:
                    </label>
                    <!-- DO NOT REMOVE CONTACTADR2CITY -->
                    <asp:TextBox ID="CONTACTADR2CITY" runat="server" ToolTip="Example: Chicago" Width="190px"  />
                    <label class="sameline" for="CONTACTADR2STATE">
                        State:
                    </label>
                    <!-- DO NOT REMOVE CONTACTADR2STATE -->
                    <asp:TextBox ID="CONTACTADR2STATE" Columns="2" MaxLength="2" runat="server" ToolTip="Example: IL" Width="44px"  />
                    <label class="sameline" for="CONTACTADR2ZIP">
                        Zip:
                    </label>
                    <!-- DO NOT REMOVE CONTACTADR2ZIP -->
                    <asp:TextBox ID="CONTACTADR2ZIP" Columns="10" MaxLength="10" runat="server" ToolTip="Example: 60622 or 60622-1212" Width="62px" />
                    <asp:RegularExpressionValidator ID="ContactZipValidator" runat="server" ControlToValidate="CONTACTADR2ZIP"
                            CssClass="error" ErrorMessage="Please ender a valid zip code in the format 12345 or 12345-6789."
                            ValidationExpression="^\d{5}(-\d{4})?$" />
                </p>
                <p>
                    <label class="left" for="CONTACTPH">
                        Phone:
                    </label>
                    <!-- DO NOT REMOVE CONTACTPH -->
                    <asp:TextBox ID="CONTACTPH" MaxLength="13" runat="server" ToolTip="Example: (312)555-1234"  Width="190px"/>
                    <asp:RegularExpressionValidator ID="ContactPhoneValidator" runat="server" ControlToValidate="CONTACTPH"
                            CssClass="error" ErrorMessage="Please enter a valid phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                </p>
                <p>
                    <label class="left" for="CONTACTEMAIL">
                        Email:
                    </label>
                    <!-- DO NOT REMOVE CONTACTEMAIL -->
                    <asp:TextBox ID="CONTACTEMAIL" Columns="40" runat="server" ToolTip="Example: fred.jones@jonesrealty.com" Width="425px"  />
                    <asp:RegularExpressionValidator ID="ContactEmailValidator" runat="server" 
                            ControlToValidate="CONTACTEMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    <asp:RequiredFieldValidator ID="ContactEmailRequired" runat="server" ControlToValidate="CONTACTEMAIL" ErrorMessage="Please provide an email address so that we can send you an order confirmation." CssClass="error" />
                </p>
            </fieldset>
            <fieldset>
                <legend>Transaction Information</legend>
                <p>
                    <label class="left" for="PURCREFI">
                        Transaction Type:</label>
                    <asp:DropDownList ID="PURCREFI" runat="server" onChange="customizeForTransactionType()">
                        <asp:ListItem Value="Purchase">Purchase</asp:ListItem>
                        <asp:ListItem Value="Refinance">Refinance</asp:ListItem>
                        <asp:ListItem Value="Equity">Equity</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <p>
                    <label class="left" for="SETTDATE">
                        Settlement Date:
                    </label>
                    <asp:TextBox ID="SETTDATE" runat="server" ToolTip="Example: 10/03/2011"/>
                    <asp:RegularExpressionValidator ID="SettlementDateValidator" runat="server" ControlToValidate="SETTDATE" 
                        CssClass="error" ErrorMessage="Please enter a valid settlement date in the format mm/dd/yyyy" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" />
                </p>
                <p id="salesPriceGroup">
                    <label class="left" for="SALEPRIC">
                        Sales Price:
                    </label>
                    <asp:TextBox ID="SALEPRIC" runat="server" ToolTip="Example: 195000.00"/>
                    <asp:RegularExpressionValidator ID="SalesPriceValidator" runat="server" ControlToValidate="SALEPRIC" 
                        ErrorMessage="Sales Price must be numeric (e.g. 355000.00)" CssClass="error" 
                        ValidationExpression="^\d+(\.\d\d)?$" />
                </p>
                <p>
                    <label class="left" for="LOANAMT">
                        Loan Amount:
                    </label>
                    <asp:TextBox ID="LOANAMT" MaxLength="14" runat="server" ToolTip="Example: 155000.00" />
                    <asp:RegularExpressionValidator ID="LoanAmountValidator" runat="server" ControlToValidate="LOANAMT" ErrorMessage="Loan Amount must be numeric (e.g. 255000.00)" CssClass="error" ValidationExpression="^\d+(\.\d\d)?$" />
                </p>
                <p class="checkbox">
                    <label for="CASHSALE">
                        Check this box if this order is a cash transaction
                    </label>
                    <asp:CheckBox ID="CASHSALE" runat="server" />
                </p>
                <p class="checkbox">
                    <label for="CONSTN">
                        Check this box if this order is for new construction
                    </label>
                    <asp:CheckBox ID="CONSTN" runat="server" />
                </p>
                <p class="checkbox" style="font-weight: bold">
                    Note: We cannot process your order request for construction without underwriter approval for mechanic's lien coverage. Please contact us for terms on how to obtain mechanic's lien coverage.
                </p>
                <p class="checkbox">
                    <label for="CLOSING">
                        Will Crawford County Title Company be closing this transaction?
                    </label>
                    <asp:DropDownList ID="CLOSING" runat="server">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </p>
            </fieldset>
            <fieldset>
                <legend>Property Information</legend>
                <p>
                    <label class="left" for="PROPSTRE">
                        Property Address:
                    </label>
                    <!-- DO NOT REMOVE PROPSTRE -->
                    <asp:TextBox ID="PROPSTRE" Columns="60" runat="server" ToolTip="Example: 1024 Elm Street" Width="425px" />
                </p>
                <p>
                    <label class="left" for="PROPCITY">
                        City:
                    </label>
                    <asp:TextBox ID="PROPCITY" runat="server" ToolTip="Example: Raleigh" Width="190px"/>
                    <label class="sameline" for="STATELET">
                        State:
                    </label>
                    <asp:TextBox ID="STATELET" Columns="2" MaxLength="2" runat="server" ToolTip="Example: NC" Width="44px"/>
                    <label class="sameline" for="PROPZIP">
                        Zip:
                    </label>
                    <asp:TextBox ID="PROPZIP" Columns="10" MaxLength="10" runat="server" ToolTip="Example: 27606" Width="62px"/>
                    <asp:RegularExpressionValidator ID="PropZipValidator" runat="server" ControlToValidate="PROPZIP"
                        CssClass="error" ErrorMessage="Please ender a valid zip code in the format 12345 or 12345-6789."
                        ValidationExpression="^\d{5}(-\d{4})?$" />
                </p>
                <p>
                    <label class="left" for="COUNTY">
                        County:
                    </label>
                    <asp:TextBox ID="COUNTY" runat="server" ToolTip="Example: Cook" Width="190px"/>
                </p>
                <p>
                    <label class="left" for="BLEGAL1">
                        Brief Legal:
                    </label>
                    <asp:TextBox ID="BLEGAL1" Columns="40" runat="server" ToolTip="Example: Lot 4, Block C, Coverd Bridge" Width="425px"/>
                </p>
            </fieldset>
            <fieldset>
                <legend id="buyerLegend">Buyer / Borrower Information</legend>
                <fieldset>
                    <legend id="buyerLegend1">Buyer 1</legend>
                    <p>
                        <label id="buyerOneLabel" class="left" for="BYR1NAM1">
                            Name:
                        </label>
                        <asp:TextBox ID="BYR1NAM1" Columns="40" runat="server" ToolTip="Example: John Q. Smith" />
                    </p>
                    <p>
                        <label class="left">
                            Address Line 1:
                        </label>
                        <asp:TextBox ID="BYR1ADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street"/>
                    </p>
                    <p>
                        <label class="left">
                            Address Line 2:
                        </label>
                        <asp:TextBox ID="BYR1ADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"/>
                    </p>
                    <p>
                        <label class="left" for="BYR1HOM1">
                            Home Phone:
                        </label>
                        <asp:TextBox ID="BYR1HOM1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="BuyerHomeValidator" runat="server" ControlToValidate="BYR1HOM1"
                            CssClass="error" ErrorMessage="Please enter a valid home phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="BYR1CELL1">
                            Mobile Phone:
                        </label>
                        <asp:TextBox ID="BYR1CELL1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="BuyerMobileVaidator" runat="server" ControlToValidate="BYR1CELL1"
                            CssClass="error" ErrorMessage="Please enter a valid mobile phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                    </p>
                    <p>
                        <label class="left" for="BYR1EMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="BYR1EMAIL" Columns="40" runat="server" ToolTip="Example: greg@gmail.com"/>
                        <asp:RegularExpressionValidator ID="BuyerEmailValidator" runat="server" 
                            ControlToValidate="BYR1EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
                <fieldset>
                    <legend id="buyerLegend2">Buyer 2</legend>
                    <p>
                        <label id="buyerTwoLabel" class="left" for="BYR1NAM2">
                            Name:
                        </label>
                        <asp:TextBox ID="BYR2NAM1" Columns="40" runat="server" ToolTip="Example: Sally R. Smith"/>
                    </p>
                    <p>
                        <label class="left">
                            Address Line 1:
                        </label>
                        <asp:TextBox ID="BYR2ADR1" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"/>
                    </p>
                    <p>
                        <label class="left">
                            Address Line 2:
                        </label>
                        <asp:TextBox ID="BYR2ADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"/>
                    </p>
                    <p>
                        <label class="left" for="BYR2HOM1">
                            Home Phone:
                        </label>
                        <asp:TextBox ID="BYR2HOM1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="BYR2HOM1"
                            CssClass="error" ErrorMessage="Please enter a valid home phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="BYR2CELL1">
                            Mobile Phone:
                        </label>
                        <asp:TextBox ID="BYR2CELL1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="BYR2CELL1"
                            CssClass="error" ErrorMessage="Please enter a valid mobile phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                    </p>
                    <p>
                        <label class="left" for="BYR2EMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="BYR2EMAIL" Columns="40" runat="server" ToolTip="Example: greg@gmail.com"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="BYR2EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
                </fieldset>
            <div id="sellerBlock">
                <fieldset>
                    <legend id="sellerLegend">Seller Information</legend>
                <fieldset>
                    <legend id="seller1Legend">Seller 1</legend>
                    <p>
                        <label id="sellerOneLabel" class="left" for="SLR1NAM1">
                            Name:
                        </label>
                        <asp:TextBox ID="SLR1NAM1" Columns="40" runat="server" ToolTip="Example: John Q. Smith" />
                    </p>
                    <p>
                        <label class="left">
                            Address Line 1:
                        </label>
                        <asp:TextBox ID="SLR1ADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street"/>
                    </p>
                    <p>
                        <label class="left">
                            Address Line 2:
                        </label>
                        <asp:TextBox ID="SLR1ADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"/>
                    </p>
                    <p>
                        <label class="left" for="SLR1HOM1">
                            Home Phone:
                        </label>
                        <asp:TextBox ID="SLR1HOM1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="SLR1HOM1"
                            CssClass="error" ErrorMessage="Please enter a valid home phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="SLR1CELL1">
                            Mobile Phone:
                        </label>
                        <asp:TextBox ID="SLR1CELL1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="SLR1CELL1"
                            CssClass="error" ErrorMessage="Please enter a valid mobile phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                    </p>
                    <p>
                        <label class="left" for="SLR1EMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="SLR1EMAIL" Columns="40" runat="server" ToolTip="Example: greg@gmail.com"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                            ControlToValidate="SLR1EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
                <fieldset>
                    <legend id="seller2Legend">Seller 2</legend>
                    <p>
                        <label id="sellerTwoLabel" class="left" for="SLR1NAM1">
                            Name:
                        </label>
                        <asp:TextBox ID="SLR2NAM1" Columns="40" runat="server" ToolTip="Example: John Q. Smith" />
                    </p>
                    <p>
                        <label class="left">
                            Address Line 1:
                        </label>
                        <asp:TextBox ID="SLR2ADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street"/>
                    </p>
                    <p>
                        <label class="left">
                            Address Line 2:
                        </label>
                        <asp:TextBox ID="SLR2ADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"/>
                    </p>
                    <p>
                        <label class="left" for="SLR2HOM1">
                            Home Phone:
                        </label>
                        <asp:TextBox ID="SLR2HOM1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="SLR2HOM1"
                            CssClass="error" ErrorMessage="Please enter a valid home phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="SLR2CELL1">
                            Mobile Phone:
                        </label>
                        <asp:TextBox ID="SLR2CELL1" MaxLength="13" Columns="15" runat="server" ToolTip="Example: (312)555-1234"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="SLR2CELL1"
                            CssClass="error" ErrorMessage="Please enter a valid mobile phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                    </p>
                    <p>
                        <label class="left" for="SLR2EMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="SLR2EMAIL" Columns="40" runat="server" ToolTip="Example: greg@gmail.com"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                            ControlToValidate="SLR2EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
            </fieldset>
            </div>
            <fieldset>
                <legend>Lender</legend>
                <p class="checkbox">
                    <strong>Note: Please provide the lender name and address how it should appear on the Commitment for Title Insurance and the Closing Protection Letter (CPL)
</strong>
                </p>
                <p>
                    <label class="left" for="LENNAM1">
                        Name:
                    </label>
                    <asp:TextBox ID="LENNAM1" Columns="60" runat="server" ToolTip="Example: Wells Fargo"
                        />
                </p>
                <p>
                    <label class="left" for="LENADR1">
                        Address Line 1:
                    </label>
                    <asp:TextBox ID="LENADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street"
                        />
                </p>
                <p>
                    <label class="left" for="LENADR1">
                        Address Line 2:
                    </label>
                     <asp:TextBox ID="LENADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"
                        />
                </p>
                <fieldset>
                    <legend>Lender Main Contact</legend>
                    <p>
                        <label class="left" for="LENCONT">
                            Name:
                        </label>
                        <asp:TextBox ID="LENCONT" Columns="60" runat="server" ToolTip="Example: Gregory Esquire" />
                    </p>
                    <p>
                        <label class="left" for="LENPH">
                            Phone:
                        </label>
                        <asp:TextBox ID="LENPH" MaxLength="13" Columns="15" ToolTip="Example: (312)555-1234" runat="server"
                            />
                        <asp:RegularExpressionValidator ID="LenderPhoneValidator" runat="server" ControlToValidate="LENPH"
                            CssClass="error" ErrorMessage="Please enter a valid phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="LENEMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="LENEMAIL" runat="server" ToolTip="Example: greg@esquirellp.com"
                            />
                        <asp:RegularExpressionValidator ID="LenderEmailValidator" runat="server" 
                            ControlToValidate="LENEMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
            </fieldset>
            <fieldset id="mortgageBrokerBlock">
                <legend>Mortgage Broker</legend>
                <p>
                    <label class="left" for="MTBNAME">
                        Name:
                    </label>
                    <asp:TextBox ID="MTBNAME" Columns="60" runat="server" ToolTip="Example: Wells Fargo" />
                </p>
                <p>
                    <label class="left" for="MTBADR1">
                        Address Line 1:
                    </label>
                    <asp:TextBox ID="MTBADR1" Columns="60" runat="server" ToolTip="Example: 123 Main Street"  />
                </p>
                <p>
                    <label class="left" for="MTBADR2">
                        Address Line 2:
                    </label>
                    <asp:TextBox ID="MTBADR2" Columns="60" runat="server" ToolTip="Example: Cuba, MO 65453"  />
                </p>
                <fieldset>
                    <legend>Mortgage Broker Main Contact</legend>
                    <p>
                        <label class="left" for="MTBCONT">
                            Name:
                        </label>
                        <asp:TextBox ID="MTBCONT" Columns="60" runat="server" ToolTip="Example: Gregory Esquire" />
                    </p>
                    <p>
                        <label class="left" for="MTBPH">
                            Phone:
                        </label>
                        <asp:TextBox ID="MTBPH" MaxLength="13" Columns="15" ToolTip="Example: (312)555-1234" runat="server"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="MTBPH"
                            CssClass="error" ErrorMessage="Please enter a valid phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="MTBEMAIL">
                            Email:
                        </label>
                        <asp:TextBox ID="MTBEMAIL" runat="server" ToolTip="Example: greg@esquirellp.com"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                            ControlToValidate="MTBEMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
            </fieldset>
            <fieldset id="listingBlock">
                <legend>Listing Agent/Broker</legend>
                <p>
                    <label class="left" for="AG701FRM">
                        Name:
                    </label>
                    <asp:TextBox ID="AG701FRM" Columns="60" runat="server" ToolTip="Example: Remax" />
                </p>
                <p>
                    <label class="left" for="AG701AD1">
                        Address Line 1:
                    </label>
                    <asp:TextBox ID="AG701AD1" Columns="60" runat="server" ToolTip="Example: 123 Main Street" />
                </p>
                <p>
                    <label class="left" for="AG701AD2">
                        Address Line 2:
                    </label>
                    <asp:TextBox ID="AG701AD2" Columns="60" runat="server" />
                </p>
                <fieldset>
                    <legend>Listing Broker Main Contact</legend>
                    <p>
                        <label class="left" for="AG701NAM">
                            Agent Name:
                        </label>
                        <asp:TextBox ID="AG701NAM" Columns="60" runat="server" ToolTip="Example: Gregory Esquire" />
                    </p>
                    <p>
                        <label class="left" for="AG701PH">
                            Agent Phone:
                        </label>
                        <asp:TextBox ID="AG701PH" MaxLength="13" Columns="15" ToolTip="Example: (312)555-1234" runat="server"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="AG701PH"
                            CssClass="error" ErrorMessage="Please enter a valid phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="AG701EMAIL">
                            Agent Email:
                        </label>
                        <asp:TextBox ID="AG701EMAIL" runat="server" ToolTip="Example: greg@esquirellp.com"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                            ControlToValidate="AG701EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                </fieldset>
            </fieldset>
            <fieldset id="sellingBlock">
                <legend>Selling Agent/Broker</legend>
                <p>
                    <label class="left" for="AG702FRM">
                        Name:
                    </label>
                    <asp:TextBox ID="AG702FRM" Columns="60" runat="server" ToolTip="Example: Remax" />
                </p>
                <p>
                    <label class="left" for="AG702AD1">
                        Address Line 1:
                    </label>
                    <asp:TextBox ID="AG702AD1" Columns="60" runat="server" ToolTip="Example: 123 Main Street" />
                </p>
                <p>
                    <label class="left" for="AG702AD2">
                        Address Line 2:
                    </label>
                    <asp:TextBox ID="AG702AD2" Columns="60" runat="server" />
                </p>
                <fieldset>
                    <legend>Selling Broker Main Contact</legend>
                    <p>
                        <label class="left" for="AG702NAM">
                            Agent Name:
                        </label>
                        <asp:TextBox ID="AG702NAM" Columns="60" runat="server" ToolTip="Example: Gregory Esquire" />
                    </p>
                    <p>
                        <label class="left" for="AG702PH">
                            Agent Phone:
                        </label>
                        <asp:TextBox ID="AG702PH" MaxLength="13" Columns="15" ToolTip="Example: (312)555-1234" runat="server"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="AG702PH"
                            CssClass="error" ErrorMessage="Please enter a valid phone number with the format (321)231-2315" 
                            ValidationExpression="\(\d{3}\)\d{3}-\d{4}" />
                        <label class="sameline" for="AG702EMAIL">
                            Agent Email:
                        </label>
                        <asp:TextBox ID="AG702EMAIL" runat="server" ToolTip="Example: greg@esquirellp.com"
                            />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                            ControlToValidate="AG702EMAIL" ErrorMessage="Please enter a valid email address" CssClass="error" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </p>
                    </fieldset>
            </fieldset>
            <fieldset>
                <legend>Additional Information</legend>
                <p class="checkbox">
                    <asp:TextBox TextMode="MultiLine" Rows="6" ID="NOTES"  runat="server" Width="100%"/>
                </p>
            </fieldset>

            <p class="checkbox">Please verify that all information entered is correct before submitting your order. Once you click “Submit Order” changes cannot be made without contacting Crawford County Title Company. If you would like to print a copy of this order, please print from your browser window before clicking on the submit button. After you click "submit order", confirmation that your order request was submitted will be displayed on the next screen. We will provide you with an order confirmation to the email listed above once your order request has been processed. Thank you for your order.</p>
            <!-- FORM BUTTONS -->
            <p class="checkbox">
                <asp:Button ID="SubmitOrder" runat="server" Text='Submit Order' OnClick="SubmitOrder_Click" />
                &nbsp;</p>
            </form>
            </div>
        <div id="result" visible="false" runat="server">
            <!-- result of form submission is inserted here dynamically -->
        </div>
    </div>
</body>
</html>
