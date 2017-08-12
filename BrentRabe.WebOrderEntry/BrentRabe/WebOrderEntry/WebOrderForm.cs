using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BrentRabe.WebOrderEntry
{

    public class WebOrderForm : Page
    {
        private const string ADDRESS2_CITY = "ADR2CITY";
        private const string ADDRESS2_PREFIX = "ADR2";
        private const string ADDRESS2_STATE = "ADR2STATE";
        private const string ADDRESS2_ZIP = "ADR2ZIP";
        protected TextBox ADR2CITYAG701;
        protected TextBox ADR2CITYAG702;
        protected TextBox ADR2CITYBYR1;
        protected TextBox ADR2CITYLEN;
        protected TextBox ADR2CITYMTB;
        protected TextBox ADR2CITYSLR1;
        protected TextBox ADR2STATEAG701;
        protected TextBox ADR2STATEAG702;
        protected TextBox ADR2STATEBYR1;
        protected TextBox ADR2STATELEN;
        protected TextBox ADR2STATEMTB;
        protected TextBox ADR2STATESLR1;
        protected TextBox ADR2ZIPAG701;
        protected TextBox ADR2ZIPAG702;
        protected TextBox ADR2ZIPBYR1;
        protected TextBox ADR2ZIPLEN;
        protected TextBox ADR2ZIPMTB;
        protected TextBox ADR2ZIPSLR1;
        protected TextBox AG701AD1;
        protected TextBox AG701AD2;
        protected TextBox AG701EMAIL;
        protected RegularExpressionValidator AG701EmailValidator;
        protected RegularExpressionValidator AG701FaxValidator;
        protected TextBox AG701FRM;
        protected TextBox AG701FX;
        protected TextBox AG701NAM;
        protected TextBox AG701PH;
        protected RegularExpressionValidator AG701PhoneValidator;
        protected RegularExpressionValidator Ag701ZipValidator;
        protected TextBox AG702AD1;
        protected TextBox AG702AD2;
        protected TextBox AG702EMAIL;
        protected RegularExpressionValidator AG702EmailValidator;
        protected RegularExpressionValidator AG702FaxValidator;
        protected TextBox AG702FRM;
        protected TextBox AG702FX;
        protected TextBox AG702NAM;
        protected TextBox AG702PH;
        protected RegularExpressionValidator AG702PhoneValidator;
        protected RegularExpressionValidator Ag702ZipValidator;
        protected TextBox BLEGAL1;
        protected RegularExpressionValidator BuyerEmailValidator;
        protected RegularExpressionValidator BuyerHomeValidator;
        protected RegularExpressionValidator BuyerMobileVaidator;
        protected TextBox BYR1ADR1;
        protected TextBox BYR1ADR2;
        protected TextBox BYR1CELL1;
        protected TextBox BYR1EMAIL;
        protected TextBox BYR1HOM1;
        protected TextBox BYR1NAM1;
        protected TextBox BYR1NAM2;
        protected TextBox BYR2ADR1;
        protected TextBox BYR2ADR2;
        protected TextBox BYR2CELL1;
        protected TextBox BYR2EMAIL;
        protected TextBox BYR2HOM1;
        protected TextBox BYR2NAM1;
        protected TextBox BYR2NAM2;
        protected RegularExpressionValidator Byr1ZipVaidator;
        protected CheckBox CASHSALE;
        protected DropDownList CLOSING;
        private const string CONTACT_PREFIX = "CONTACT";
        protected TextBox CONTACTADR1;
        protected TextBox CONTACTADR2CITY;
        protected TextBox CONTACTADR2STATE;
        protected TextBox CONTACTADR2ZIP;
        protected TextBox CONTACTEMAIL;
        protected RequiredFieldValidator ContactEmailRequired;
        protected RegularExpressionValidator ContactEmailValidator;
        protected TextBox CONTACTNAM;
        protected TextBox CONTACTPH;
        protected RegularExpressionValidator ContactPhoneValidator;
        protected DropDownList CONTACTTYPE;
        protected RegularExpressionValidator ContactZipValidator;
        protected TextBox COUNTY;
        protected TextBox LENADR1;
        protected TextBox LENADR2;
        protected TextBox LENCONT;
        protected RegularExpressionValidator LenderEmailValidator;
        protected RegularExpressionValidator LenderFaxValidator;
        protected RegularExpressionValidator LenderPhoneValidator;
        protected TextBox LENEMAIL;
        protected TextBox LENFX;
        protected TextBox LENNAM1;
        protected TextBox LENPH;
        protected RegularExpressionValidator LenZipValidator;
        protected RegularExpressionValidator LoanAmountValidator;
        protected TextBox LOANAMT;
        protected TextBox MTBADR1;
        protected TextBox MTBADR2;
        protected TextBox MTBCONT;
        protected TextBox MTBEMAIL;
        protected RegularExpressionValidator MtbEmailValidator;
        protected RegularExpressionValidator MtbFaxValidator;
        protected TextBox MTBFX;
        protected TextBox MTBNAME;
        protected TextBox MTBPH;
        protected RegularExpressionValidator MtbPhoneValidator;
        protected RegularExpressionValidator MtbZipValidator;
        protected TextBox NOTES;
        protected CheckBox CONSTN;
        protected HtmlForm ProFormWebOrderEntry;
        protected TextBox PROPCITY;
        protected TextBox PROPSTRE;
        protected TextBox PROPZIP;
        protected RegularExpressionValidator PropZipValidator;
        protected DropDownList PURCREFI;
        protected HtmlGenericControl result;
        protected TextBox SALEPRIC;
        protected RegularExpressionValidator SalesPriceValidator;
        protected RegularExpressionValidator SellerEmailValidator;
        protected RegularExpressionValidator SellerHomeValidator;
        protected RegularExpressionValidator SellerMobileValidator;
        protected TextBox SETTDATE;
        protected RegularExpressionValidator SettlementDateValidator;
        protected TextBox SLR1ADR1;
        protected TextBox SLR1ADR2;
        protected TextBox SLR1CELL1;
        protected TextBox SLR1EMAIL;
        protected TextBox SLR1HOM1;
        protected TextBox SLR1NAM1;
        protected TextBox SLR1NAM2;
        protected TextBox SLR2ADR1;
        protected TextBox SLR2ADR2;
        protected TextBox SLR2CELL1;
        protected TextBox SLR2EMAIL;
        protected TextBox SLR2HOM1;
        protected TextBox SLR2NAM1;
        protected TextBox SLR2NAM2;
        protected RegularExpressionValidator Slr1ZipValidator;
        protected TextBox STATELET;
        protected Button SubmitOrder;
        protected HtmlGenericControl woe;

        private void AppendContactField(TextBox contactTextBox, StringBuilder contactOutput)
        {
            if (!string.IsNullOrEmpty(contactTextBox.Text))
            {
                if (contactOutput.Length != ConfigurationManager.AppSettings["CONTACT"].Length)
                {
                    contactOutput.Append(", ");
                }
                contactOutput.Append(contactTextBox.Text);
            }
        }

        private void CleanUp(string pxtPath)
        {
            bool flag = false;
            if (flag.ToString().Equals(ConfigurationManager.AppSettings["SavePXT"]))
            {
                try
                {
                    File.Delete(pxtPath);
                }
                catch (Exception)
                {
                }
            }
        }

        private void ConcatAddress2Fields(HiddenField address2)
        {
            string str = address2.ID.Remove(address2.ID.IndexOf("ADR2"));
            string text = ((TextBox)this.FindControlRecursive(this, "ADR2CITY" + str)).Text;
            string str3 = ((TextBox)this.FindControlRecursive(this, "ADR2STATE" + str)).Text;
            string str4 = ((TextBox)this.FindControlRecursive(this, "ADR2ZIP" + str)).Text;
            address2.Value = $"{text}, {str3} {str4}";
        }

        public Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
            {
                return Root;
            }
            foreach (Control control in Root.Controls)
            {
                Control control2 = this.FindControlRecursive(control, Id);
                if (control2 != null)
                {
                    return control2;
                }
            }
            return null;
        }

        private string FormatNotesNameValuePair(string name, string value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ConfigurationManager.AppSettings[name]);
            builder.Append(value);
            return builder.ToString();
        }

        private string FormatOutputItem(string name, string rawValue)
        {
            string str = this.ScrubValue(rawValue);
            if (ConfigurationManager.AppSettings.Get(name) != null)
            {
                return this.FormatNotesNameValuePair(name, str);
            }
            return this.FormatStandardNameValuePair(name, str);
        }

        private string FormatStandardNameValuePair(string name, string value)
        {
            StringBuilder builder = new StringBuilder(name);
            builder.Append('=');
            builder.Append(value);
            return builder.ToString();
        }

        private void HandleContactFields(StreamWriter pxtFile)
        {
            StringBuilder contactOutput = new StringBuilder(ConfigurationManager.AppSettings["CONTACT"]);
            this.AppendContactField(this.CONTACTNAM, contactOutput);
            this.AppendContactField(this.CONTACTADR1, contactOutput);
            this.AppendContactField(this.CONTACTADR2CITY, contactOutput);
            this.AppendContactField(this.CONTACTADR2STATE, contactOutput);
            this.AppendContactField(this.CONTACTADR2ZIP, contactOutput);
            this.AppendContactField(this.CONTACTPH, contactOutput);
            this.AppendContactField(this.CONTACTEMAIL, contactOutput);
            this.AppendContactField(this.NOTES, contactOutput);
            pxtFile.WriteLine(contactOutput.ToString());
        }

        private void HandleStandardFields(StreamWriter pxtFile)
        {
            foreach (Control control in base.Form.Controls)
            {
                if ((((control.ID != null) && !control.ID.StartsWith("CONTACT")) && !control.ID.StartsWith("ADR2")) && !control.ID.StartsWith("CLOSING"))
                {
                    if (control is TextBox)
                    {
                        TextBox box = (TextBox)control;
                        pxtFile.WriteLine(this.FormatOutputItem(box.ID, box.Text));
                    }
                    else if (control is DropDownList)
                    {
                        DropDownList list = (DropDownList)control;
                        pxtFile.WriteLine(this.FormatOutputItem(list.ID, list.SelectedValue));
                    }
                    else if ((control is HiddenField) && control.ID.EndsWith("ADR2"))
                    {
                        HiddenField field = (HiddenField)control;
                        this.ConcatAddress2Fields(field);
                        pxtFile.WriteLine(this.FormatOutputItem(field.ID, field.Value));
                    }
                    else if (control is CheckBox)
                    {
                        CheckBox check = (CheckBox)control;
                        pxtFile.WriteLine(this.FormatOutputItem(check.ID, check.Checked.ToString()));
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private string ScrubValue(string rawValue) =>
            rawValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

        private void SendConfirmation()
        {
            DateTime thisDay = DateTime.Now;

            string thisDayString = thisDay.ToString("g");

            string emailSubject = "We have received your order. - " + thisDayString;

            string EmailTemplate = ConfigurationManager.AppSettings["ConfirmationEmailTemplate"];

            if (this.PURCREFI.Text == "Purchase")
            {
                EmailTemplate = "Confirmation.email.html";
            }

            if (this.PURCREFI.Text == "Refinance")
            {
                EmailTemplate = "Confirmation.email.refi.html";
            }

            if (this.PURCREFI.Text == "Equity")
            {
                EmailTemplate = "Notification.email.equity.html";
            }

            MailAddress fromAddress = new MailAddress("cctc@crawfordtitle.com", "Crawford County Title Company");

            MailDefinition definition = new MailDefinition
            {
                IsBodyHtml = true,
                BodyFileName = EmailTemplate,
                From = ConfigurationManager.AppSettings["EmailSender"],
                Subject = emailSubject,
                Priority = MailPriority.Normal
            };
            ListDictionary replacements = new ListDictionary {
                {
                    "%CONTACT_NAME%",
                    base.Server.HtmlEncode(this.CONTACTNAM.Text)
                },
                {
                    "%CONTACT_ROLE%",
                    base.Server.HtmlEncode(this.CONTACTTYPE.Text)
                },
                {
                    "%CONTACT_ADDRESS%",
                    base.Server.HtmlEncode(this.CONTACTADR1.Text)
                },
                {
                    "%CONTACT_CITY%",
                    base.Server.HtmlEncode(this.CONTACTADR2CITY.Text)
                },
                {
                    "%CONTACT_STATE%",
                    base.Server.HtmlEncode(this.CONTACTADR2STATE.Text)
                },
                {
                    "%CONTACT_ZIP%",
                    base.Server.HtmlEncode(this.CONTACTADR2ZIP.Text)
                },
                {
                    "%CONTACT_PHONE%",
                    base.Server.HtmlEncode(this.CONTACTPH.Text)
                },
                {
                    "%CONTACT_EMAIL%",
                    base.Server.HtmlEncode(this.CONTACTEMAIL.Text)
                },
                {
                    "%TRANSACTION_TYPE%",
                    base.Server.HtmlEncode(this.PURCREFI.Text)
                },
                {
                    "%SETTLEMENT_DATE%",
                    base.Server.HtmlEncode(this.SETTDATE.Text)
                },
                {
                    "%SALE_PRICE%",
                    base.Server.HtmlEncode(this.SALEPRIC.Text)
                },
                {
                    "%LOAN_AMOUNT%",
                    base.Server.HtmlEncode(this.LOANAMT.Text)
                },
                {
                    "%CASH_TRANSACTION%",
                    base.Server.HtmlEncode(this.CASHSALE.Checked.ToString())
                },
                {
                    "%NEW_CONSTRUCTION%",
                    base.Server.HtmlEncode(this.CONSTN.Checked.ToString())
                },
                {
                    "%CLOSING%",
                    base.Server.HtmlEncode(this.CLOSING.Text)
                },
                {
                    "%PROPERTY_ADDRESS%",
                    base.Server.HtmlEncode(this.PROPSTRE.Text)
                },
                {
                    "%PROPERTY_CITY%",
                    base.Server.HtmlEncode(this.PROPCITY.Text)
                },
                {
                    "%PROPERTY_STATE%",
                    base.Server.HtmlEncode(this.STATELET.Text)
                },
                {
                    "%PROPERTY_ZIP%",
                    base.Server.HtmlEncode(this.PROPZIP.Text)
                },
                {
                    "%PROPERTY_COUNTY%",
                    base.Server.HtmlEncode(this.COUNTY.Text)
                },
                {
                    "%BRIEF_LEGAL%",
                    base.Server.HtmlEncode(this.BLEGAL1.Text)
                },
                {
                    "%BUYER1_NAME%",
                    base.Server.HtmlEncode(this.BYR1NAM1.Text)
                },
                {
                    "%BUYER1_ADDRESS1%",
                    base.Server.HtmlEncode(this.BYR1ADR1.Text)
                },
                {
                    "%BUYER1_ADDRESS2%",
                    base.Server.HtmlEncode(this.BYR1ADR2.Text)
                },
                {
                    "%BUYER1_HOMEPH%",
                    base.Server.HtmlEncode(this.BYR1HOM1.Text)
                },
                {
                    "%BUYER1_CELLPH%",
                    base.Server.HtmlEncode(this.BYR1CELL1.Text)
                },
                {
                    "%BUYER1_EMAIL%",
                    base.Server.HtmlEncode(this.BYR1EMAIL.Text)
                },
                {
                    "%BUYER2_NAME%",
                    base.Server.HtmlEncode(this.BYR2NAM1.Text)
                },
                {
                    "%BUYER2_ADDRESS1%",
                    base.Server.HtmlEncode(this.BYR2ADR1.Text)
                },
                {
                    "%BUYER2_ADDRESS2%",
                    base.Server.HtmlEncode(this.BYR2ADR2.Text)
                },
                {
                    "%BUYER2_HOMEPH%",
                    base.Server.HtmlEncode(this.BYR2HOM1.Text)
                },
                {
                    "%BUYER2_CELLPH%",
                    base.Server.HtmlEncode(this.BYR2CELL1.Text)
                },
                {
                    "%BUYER2_EMAIL%",
                    base.Server.HtmlEncode(this.BYR2EMAIL.Text)
                },
                {
                    "%SELLER1_NAME%",
                    base.Server.HtmlEncode(this.SLR1NAM1.Text)
                },
                {
                    "%SELLER1_ADDRESS1%",
                    base.Server.HtmlEncode(this.SLR1ADR1.Text)
                },
                {
                    "%SELLER1_ADDRESS2%",
                    base.Server.HtmlEncode(this.SLR1ADR2.Text)
                },
                {
                    "%SELLER1_HOMEPH%",
                    base.Server.HtmlEncode(this.SLR1HOM1.Text)
                },
                {
                    "%SELLER1_CELLPH%",
                    base.Server.HtmlEncode(this.SLR1CELL1.Text)
                },
                {
                    "%SELLER1_EMAIL%",
                    base.Server.HtmlEncode(this.SLR1EMAIL.Text)
                },
                {
                    "%SELLER2_NAME%",
                    base.Server.HtmlEncode(this.SLR2NAM1.Text)
                },
                {
                    "%SELLER2_ADDRESS1%",
                    base.Server.HtmlEncode(this.SLR2ADR1.Text)
                },
                {
                    "%SELLER2_ADDRESS2%",
                    base.Server.HtmlEncode(this.SLR2ADR2.Text)
                },
                {
                    "%SELLER2_HOMEPH%",
                    base.Server.HtmlEncode(this.SLR2HOM1.Text)
                },
                {
                    "%SELLER2_CELLPH%",
                    base.Server.HtmlEncode(this.SLR2CELL1.Text)
                },
                {
                    "%SELLER2_EMAIL%",
                    base.Server.HtmlEncode(this.SLR2EMAIL.Text)
                },
                {
                    "%LENDER_NAME%",
                    base.Server.HtmlEncode(this.LENNAM1.Text)
                },
                {
                    "%LENDER_ADDRESS1%",
                    base.Server.HtmlEncode(this.LENADR1.Text)
                },
                {
                    "%LENDER_ADDRESS2%",
                    base.Server.HtmlEncode(this.LENADR2.Text)
                },
                {
                    "%LENDERAGENT_NAME%",
                    base.Server.HtmlEncode(this.LENCONT.Text)
                },
                {
                    "%LENDERAGENT_PHONE%",
                    base.Server.HtmlEncode(this.LENPH.Text)
                },
                {
                    "%LENDERAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.LENEMAIL.Text)
                },
                {
                    "%MORTGAGE_NAME%",
                    base.Server.HtmlEncode(this.MTBNAME.Text)
                },
                {
                    "%MORTGAGE_ADDRESS1%",
                    base.Server.HtmlEncode(this.MTBADR1.Text)
                },
                {
                    "%MORTGAGE_ADDRESS2%",
                    base.Server.HtmlEncode(this.MTBADR2.Text)
                },
                {
                    "%MORTGAGEAGENT_NAME%",
                    base.Server.HtmlEncode(this.MTBCONT.Text)
                },
                {
                    "%MORTGAGEAGENT_PHONE%",
                    base.Server.HtmlEncode(this.MTBPH.Text)
                },
                {
                    "%MORTGAGEAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.MTBEMAIL.Text)
                },
                {
                    "%LISTING_NAME%",
                    base.Server.HtmlEncode(this.AG701FRM.Text)
                },
                {
                    "%LISTING_ADDRESS1%",
                    base.Server.HtmlEncode(this.AG701AD1.Text)
                },
                {
                    "%LISTING_ADDRESS2%",
                    base.Server.HtmlEncode(this.AG701AD2.Text)
                },
                {
                    "%LISTINGAGENT_NAME%",
                    base.Server.HtmlEncode(this.AG701NAM.Text)
                },
                {
                    "%LISTINGAGENT_PHONE%",
                    base.Server.HtmlEncode(this.AG701PH.Text)
                },
                {
                    "%LISTINGAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.AG701EMAIL.Text)
                },
                {
                    "%SELLING_NAME%",
                    base.Server.HtmlEncode(this.AG702FRM.Text)
                },
                {
                    "%SELLING_ADDRESS1%",
                    base.Server.HtmlEncode(this.AG702AD1.Text)
                },
                {
                    "%SELLING_ADDRESS2%",
                    base.Server.HtmlEncode(this.AG702AD2.Text)
                },
                {
                    "%SELLINGGAGENT_NAME%",
                    base.Server.HtmlEncode(this.AG702NAM.Text)
                },
                {
                    "%SELLINGAGENT_PHONE%",
                    base.Server.HtmlEncode(this.AG702PH.Text)
                },
                {
                    "%SELLINGAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.AG702EMAIL.Text)
                },
                {
                    "%ADDITIONAL_INFO%",
                    base.Server.HtmlEncode(this.NOTES.Text)
                }
            };
            using (MailMessage message = definition.CreateMailMessage(this.CONTACTEMAIL.Text, replacements, this))
            {
                message.From = fromAddress;
                this.SendEmail(message);
            }
        }

        private void SendEmail(MailMessage email)
        {
            SmtpClient client = new SmtpClient();
            bool flag = true;
            if (flag.ToString().Equals(ConfigurationManager.AppSettings["EmailEnableSSL"]))
            {
                client.EnableSsl = true;
            }
            client.Send(email);
        }

        private void SendNotification(string pxtPath)
        {
            DateTime thisDay = DateTime.Now;

            string thisDayString = thisDay.ToString("g");

            string emailSubject = "New Web Order from " + this.CONTACTNAM.Text + " at " + thisDayString;

            string EmailTemplate = ConfigurationManager.AppSettings["NotificationEmailTemplate"];

            if (this.PURCREFI.Text == "Purchase")
            {
                EmailTemplate = "Notification.email.html";
            }

            if (this.PURCREFI.Text == "Refinance")
            {
                EmailTemplate = "Notification.email.refi.html";
            }

            if (this.PURCREFI.Text == "Equity")
            {
                EmailTemplate = "Notification.email.equity.html";
            }

            MailDefinition definition = new MailDefinition
            {
                IsBodyHtml = true,
                BodyFileName = EmailTemplate,
                From = ConfigurationManager.AppSettings["EmailSender"],
                Subject = emailSubject,
                Priority = MailPriority.Normal
            };
            ListDictionary replacements = new ListDictionary {
                { 
                    "%CONTACT_NAME%",
                    base.Server.HtmlEncode(this.CONTACTNAM.Text)
                },
                {
                    "%CONTACT_ROLE%",
                    base.Server.HtmlEncode(this.CONTACTTYPE.Text)
                },
                {
                    "%CONTACT_ADDRESS%",
                    base.Server.HtmlEncode(this.CONTACTADR1.Text)
                },
                {
                    "%CONTACT_CITY%",
                    base.Server.HtmlEncode(this.CONTACTADR2CITY.Text)
                },
                {
                    "%CONTACT_STATE%",
                    base.Server.HtmlEncode(this.CONTACTADR2STATE.Text)
                },
                {
                    "%CONTACT_ZIP%",
                    base.Server.HtmlEncode(this.CONTACTADR2ZIP.Text)
                },
                {
                    "%CONTACT_PHONE%",
                    base.Server.HtmlEncode(this.CONTACTPH.Text)
                },
                {
                    "%CONTACT_EMAIL%",
                    base.Server.HtmlEncode(this.CONTACTEMAIL.Text)
                },
                {
                    "%TRANSACTION_TYPE%",
                    base.Server.HtmlEncode(this.PURCREFI.Text)
                },
                {
                    "%SETTLEMENT_DATE%",
                    base.Server.HtmlEncode(this.SETTDATE.Text)
                },
                {
                    "%SALE_PRICE%",
                    base.Server.HtmlEncode(this.SALEPRIC.Text)
                },
                {
                    "%LOAN_AMOUNT%",
                    base.Server.HtmlEncode(this.LOANAMT.Text)
                },
                {
                    "%CASH_TRANSACTION%",
                    base.Server.HtmlEncode(this.CASHSALE.Checked.ToString())
                },
                {
                    "%NEW_CONSTRUCTION%",
                    base.Server.HtmlEncode(this.CONSTN.Checked.ToString())
                },
                {
                    "%CLOSING%",
                    base.Server.HtmlEncode(this.CLOSING.Text)
                },
                {
                    "%PROPERTY_ADDRESS%",
                    base.Server.HtmlEncode(this.PROPSTRE.Text)
                },
                {
                    "%PROPERTY_CITY%",
                    base.Server.HtmlEncode(this.PROPCITY.Text)
                },
                {
                    "%PROPERTY_STATE%",
                    base.Server.HtmlEncode(this.STATELET.Text)
                },
                {
                    "%PROPERTY_ZIP%",
                    base.Server.HtmlEncode(this.PROPZIP.Text)
                },
                {
                    "%PROPERTY_COUNTY%",
                    base.Server.HtmlEncode(this.COUNTY.Text)
                },
                {
                    "%BRIEF_LEGAL%",
                    base.Server.HtmlEncode(this.BLEGAL1.Text)
                },
                {
                    "%BUYER1_NAME%",
                    base.Server.HtmlEncode(this.BYR1NAM1.Text)
                },
                {
                    "%BUYER1_ADDRESS1%",
                    base.Server.HtmlEncode(this.BYR1ADR1.Text)
                },
                {
                    "%BUYER1_ADDRESS2%",
                    base.Server.HtmlEncode(this.BYR1ADR2.Text)
                },
                {
                    "%BUYER1_HOMEPH%",
                    base.Server.HtmlEncode(this.BYR1HOM1.Text)
                },
                {
                    "%BUYER1_CELLPH%",
                    base.Server.HtmlEncode(this.BYR1CELL1.Text)
                },
                {
                    "%BUYER1_EMAIL%",
                    base.Server.HtmlEncode(this.BYR1EMAIL.Text)
                },
                {
                    "%BUYER2_NAME%",
                    base.Server.HtmlEncode(this.BYR2NAM1.Text)
                },
                {
                    "%BUYER2_ADDRESS1%",
                    base.Server.HtmlEncode(this.BYR2ADR1.Text)
                },
                {
                    "%BUYER2_ADDRESS2%",
                    base.Server.HtmlEncode(this.BYR2ADR2.Text)
                },
                {
                    "%BUYER2_HOMEPH%",
                    base.Server.HtmlEncode(this.BYR2HOM1.Text)
                },
                {
                    "%BUYER2_CELLPH%",
                    base.Server.HtmlEncode(this.BYR2CELL1.Text)
                },
                {
                    "%BUYER2_EMAIL%",
                    base.Server.HtmlEncode(this.BYR2EMAIL.Text)
                },
                {
                    "%SELLER1_NAME%",
                    base.Server.HtmlEncode(this.SLR1NAM1.Text)
                },
                {
                    "%SELLER1_ADDRESS1%",
                    base.Server.HtmlEncode(this.SLR1ADR1.Text)
                },
                {
                    "%SELLER1_ADDRESS2%",
                    base.Server.HtmlEncode(this.SLR1ADR2.Text)
                },
                {
                    "%SELLER1_HOMEPH%",
                    base.Server.HtmlEncode(this.SLR1HOM1.Text)
                },
                {
                    "%SELLER1_CELLPH%",
                    base.Server.HtmlEncode(this.SLR1CELL1.Text)
                },
                {
                    "%SELLER1_EMAIL%",
                    base.Server.HtmlEncode(this.SLR1EMAIL.Text)
                },
                {
                    "%SELLER2_NAME%",
                    base.Server.HtmlEncode(this.SLR2NAM1.Text)
                },
                {
                    "%SELLER2_ADDRESS1%",
                    base.Server.HtmlEncode(this.SLR2ADR1.Text)
                },
                {
                    "%SELLER2_ADDRESS2%",
                    base.Server.HtmlEncode(this.SLR2ADR2.Text)
                },
                {
                    "%SELLER2_HOMEPH%",
                    base.Server.HtmlEncode(this.SLR2HOM1.Text)
                },
                {
                    "%SELLER2_CELLPH%",
                    base.Server.HtmlEncode(this.SLR2CELL1.Text)
                },
                {
                    "%SELLER2_EMAIL%",
                    base.Server.HtmlEncode(this.SLR2EMAIL.Text)
                },
                {
                    "%LENDER_NAME%",
                    base.Server.HtmlEncode(this.LENNAM1.Text)
                },
                {
                    "%LENDER_ADDRESS1%",
                    base.Server.HtmlEncode(this.LENADR1.Text)
                },
                {
                    "%LENDER_ADDRESS2%",
                    base.Server.HtmlEncode(this.LENADR2.Text)
                },
                {
                    "%LENDERAGENT_NAME%",
                    base.Server.HtmlEncode(this.LENCONT.Text)
                },
                {
                    "%LENDERAGENT_PHONE%",
                    base.Server.HtmlEncode(this.LENPH.Text)
                },
                {
                    "%LENDERAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.LENEMAIL.Text)
                },
                {
                    "%MORTGAGE_NAME%",
                    base.Server.HtmlEncode(this.MTBNAME.Text)
                },
                {
                    "%MORTGAGE_ADDRESS1%",
                    base.Server.HtmlEncode(this.MTBADR1.Text)
                },
                {
                    "%MORTGAGE_ADDRESS2%",
                    base.Server.HtmlEncode(this.MTBADR2.Text)
                },
                {
                    "%MORTGAGEAGENT_NAME%",
                    base.Server.HtmlEncode(this.MTBCONT.Text)
                },
                {
                    "%MORTGAGEAGENT_PHONE%",
                    base.Server.HtmlEncode(this.MTBPH.Text)
                },
                {
                    "%MORTGAGEAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.MTBEMAIL.Text)
                },
                {
                    "%LISTING_NAME%",
                    base.Server.HtmlEncode(this.AG701FRM.Text)
                },
                {
                    "%LISTING_ADDRESS1%",
                    base.Server.HtmlEncode(this.AG701AD1.Text)
                },
                {
                    "%LISTING_ADDRESS2%",
                    base.Server.HtmlEncode(this.AG701AD2.Text)
                },
                {
                    "%LISTINGAGENT_NAME%",
                    base.Server.HtmlEncode(this.AG701NAM.Text)
                },
                {
                    "%LISTINGAGENT_PHONE%",
                    base.Server.HtmlEncode(this.AG701PH.Text)
                },
                {
                    "%LISTINGAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.AG701EMAIL.Text)
                },
                {
                    "%SELLING_NAME%",
                    base.Server.HtmlEncode(this.AG702FRM.Text)
                },
                {
                    "%SELLING_ADDRESS1%",
                    base.Server.HtmlEncode(this.AG702AD1.Text)
                },
                {
                    "%SELLING_ADDRESS2%",
                    base.Server.HtmlEncode(this.AG702AD2.Text)
                },
                {
                    "%SELLINGGAGENT_NAME%",
                    base.Server.HtmlEncode(this.AG702NAM.Text)
                },
                {
                    "%SELLINGAGENT_PHONE%",
                    base.Server.HtmlEncode(this.AG702PH.Text)
                },
                {
                    "%SELLINGAGENT_EMAIL%",
                    base.Server.HtmlEncode(this.AG702EMAIL.Text)
                },
                {
                    "%ADDITIONAL_INFO%",
                    base.Server.HtmlEncode(this.NOTES.Text)
                }
            };
            bool flag = true;
            if (flag.ToString().Equals(ConfigurationManager.AppSettings["SavePXT"]))
            {
                replacements.Add("%PXT_FILENAME%", pxtPath);
            }
            else
            {
                replacements.Add("%PXT_FILENAME%", "attached");
            }

            MailAddress fromAddressNotification = new MailAddress("cctc@crawfordtitle.com", "Web Order Form");

            using (MailMessage message = definition.CreateMailMessage(ConfigurationManager.AppSettings["NotificationRecipient"], replacements, this))
            {
                flag = true;
                if (flag.ToString().Equals(ConfigurationManager.AppSettings["NotificationAttachPXT"]))
                {
                    message.Attachments.Add(new Attachment(pxtPath));
                }
                message.From = fromAddressNotification;
                this.SendEmail(message);
            }
        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.AppSettings["OutputDirectory"];
            string path = Path.Combine(str, Guid.NewGuid().ToString("N") + ".PXT");
            HtmlGenericControl control = (HtmlGenericControl)this.FindControlRecursive(this, "woe");
            control.Visible = false;
            HtmlGenericControl control2 = (HtmlGenericControl)this.FindControlRecursive(this, "result");
            control2.Visible = true;
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    this.HandleStandardFields(writer);
                    this.HandleContactFields(writer);
                }
                this.SendConfirmation();
                this.SendNotification(path);
                control2.Attributes.Add("class", "success");
                control2.InnerText = ConfigurationManager.AppSettings["SuccessMessage"];
                this.CleanUp(path);
            }
            catch (Exception exception)
            {
                control2.Attributes.Add("class", "error");
                StringBuilder builder = new StringBuilder("<p>" + ConfigurationManager.AppSettings["ErrorMessage"] + "</p>");
                bool flag2 = true;
                if (flag2.ToString().Equals(ConfigurationManager.AppSettings["ShowErrorDetails"]))
                {
                    builder.AppendLine("<p>" + exception.Message + "</p>");
                }
                control2.InnerHtml = builder.ToString();
            }
        }
    }
}
