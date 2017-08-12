SoftPro Select Web Order Entry README

Please follow these instructions to deploy the website portion of SoftPro Select's web order entry solution:

1) Modify or incorporate the page NewOrderForm.aspx to work with your existing website.  As 
   you change/incorporate the page, make use of the cascading style sheet in Style/OrderForm.css
   to easily change the look and feel.  Beyond look and feel changes, please ensure that all script blocks remain
   entact and present in your updated page, and that you include and do not alter the entire "content" div in the page.

2) Copy SoftPro.Select.WebOrderEntry.dll to your site's "bin" folder

3) Copy the following files into your website's content folder (i.e. wwwroot).  Modify this step as
   appropriate per your changes in #1 above.
   
   - Scripts/OrderForm.js
   - Style/OrderForm.css
   - Confirmation.email.html
   - NewOrderForm.aspx
   - Notification.email.html

4) Merge the base web.config contents with your site's main web.config. The two key configuration blocks you must ensure are 
   copied into your site's web.config are the <appSettings> block (all of it) and the <mailSettings> portion of the <system.net> 
   configuration block.  The mail settings block looks like this:

      <mailSettings>
        <!-- Change from address and network settings below for local SMTP server -->
        <smtp deliveryMethod="Network" from="woe@softprocorp.com">
          <network host="smtp.host.com" port="25" defaultCredentials="true" />
        </smtp>
      </mailSettings>

   You must update the "from" attribute in the smtp element to be the email address from which emails are sent.
   You must update the "host" attribute in the network element to your SMTP server (which must be accessible from
      your webserver).
   You may update the "port" attribute if your SMTP server listens on an alternate port (25 is the default for SMTP).
   If the account that runs your website has access to the SMTP server you can keep the "defaultCredentials" attribute
      as "true", but if it does not you can set it to "false" and add attributes for "userName" and "password" to provide
      a valid user account that can authenticate against the SMTP server.

5) Edit the <appSettings> block in your web.config to alter the behavior of the webpage.  The key configuration items
   are documented in the configuration file iteself.  It is critical at least one of the SavePXT or the NotificationAttachPXT
   settings are set to "True", otherwise no PXT file will be available for importing the order! (It is OK if both are set to
   "True")
   
6) Open and edit the Confirmation.email.html template to change the default text and look and feel of the confirmation
   email which is sent to the customer.  The template contains a simple embedded cascading style sheet which can be used
   to adjust the look and feel.  Static content can be changed and rearranged as desired.  It is critical that any field
   beginning and ending with a "%" sign not be modified (although they can be moved if disired) - as these are the "merge"
   fields used to include order specific information in the email.
   
7) Open and edit the Notification.email.html template to change the default text and look and feel of the notification
   email which is sent to the company.  The template contains a simple embedded cascading style sheet which can be used
   to adjust the look and feel.  Static content can be changed and rearranged as desired.  It is critical that any field
   beginning and ending with a "%" sign not be modified (although they can be moved if disired) - as these are the "merge"
   fields used to include order specific information in the email.   

Once all of these steps have been completed, the iis service will need to be restarted to pick up the new web.config 
changes (mailSettings).