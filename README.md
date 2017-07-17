
#  Customer Centric Web Application with SalesForce & ASP.NET MVC

<a href="https://ci.appveyor.com/api/projects/status/y8lqxd7py6ggqne1?svg=true">
<img src="https://ci.appveyor.com/api/projects/status/y8lqxd7py6ggqne1?svg=true" />
</a>

----------
### About the project

This solution represents my implementation of an ASP.NET MVC application with the [SalesForce.NET Toolkit](https://github.com/developerforce/Force.com-Toolkit-for-NET). It's primary purpose is to give developers some more info on how the toolkit can be used in an MVC application using the async/await pattern.
The SalesForce Toolkits for .NET provide native libraries for interacting with SalesForce APIs, including the REST API and Chatter API. These toolkits make it super simple to consume services from SalesForce within .NET applications by packaging them up as NuGet packages, thus handling deployment and versioning. 

**How can this be useful to you or your organization?**

The key feature of Salesforce is its straightforward manner of extending functionality. Business power users can create new data storage objects, pages, views and reports to support their business. As you can imagine, this will leave vital data and business logic in the cloud. So, there will often be times when a company will need to integrate its existing enterprise systems with the Salesforce platform. This is why this sample application will be focusing on the most common way to connect .NET applications to SalesForce for data retrieval and execution of application logic.

Some of the use cases when this kind of project can be used:
- Pre-populate forms and web pages with SalesForce data.
- Push completed forms and web pages into one or more objects in SalesForce.
- Enable third party software to access or update SalesForce data.
- Expose SalesForce data such as documents or reports to selected website users.
- Enable back end functionality to set and get SalesForce data as needed.
- Have an API to pull data and then access third party system e.g. retrieve customer data from SalesForce and use that CustomerID to query the SAP system to retrieve all products for that customer.


----------
### About the author

I am Aleksandar, software developer mostly orientated on the .NET platform. I've been working with the SalesFore API for some time now and I've decided to give my contribution to the developers out there and create this sample aplication that will guide the developers of the process on retrieveing and posting data to the SalesForce CRM using custom made .NET application.

To get in touch you contact me via my [Linkedin profile](https://www.linkedin.com/in/aleksandartashev) or send me an email to my personal email address _aleksandar.tashev [AT] hotmail.com_

----------
## Getting started

### Register on the SalesForce developer platform

First step would be to register in the SalesForce developer platform. You can obtain an account by using  the following [link](https://developer.salesforce.com/signup).

Once you created and confirmed your account you can proceed with the login. These types of developer accounts are pre-filed with sample data for leads, opportunities, accounts and so on. This is a great support from their side so that developers can quickly jump in and start working with existing and real life data.

### Setup your SalesForce application

To create your SalesForce application navigate to Setup -> Build -> Create - > Apps

![](http://i.imgur.com/NljiIAc.png)

- Once you are in the Apps menu, navigate to the bottom and look for the **Connected Apps** tab. Then click the "New" button located right next to it. 
This will take you to another page where you'll need to provide some information about your salesforce application. 
Provide name for the new application you are creating along with a contact email. From the **API (Enable OAuth settings)** tab make sure that "Enable OAuth settings" is selected and in the field below named **Callback URL** provide the following link  https://login.salesforce.com/services/oauth2/token and also add the scope named "Full access (full)" into the "Selected OAuth Scopes". Scroll down to the bottom of the page and then click "Save". The end result should look something like this:
![](http://i.imgur.com/f6o78F6.png)

Next, you will need to create custom maped fields in SalesForce that will later on be used in the application for the lead objects. This can be usefull if your organization has advanced need for some additional data stored in SalesForce that by default do not belong in objects such as Leads, Accounts, Opportunities and so on...

**NOTE:** Before starting with the custom maping procedure, if you don't want to use these fields and you want't to skip this part, then go into the project and simply comment out the properties named "LastEditedBy__c" and "LastEditedOn__c" from the model named "LeadModel" (and later on comment out all code lines where these properties are actually used).

![](http://i.imgur.com/OcJXfG1.png)

If you still want to create these custom fields then navigate to Setup -> Build -> Customize -> Leads -> Fields. Page name "Lead Fields" should be opened now. Navigate to the bottom of the page and find the tab named "Lead Custom Fields & Relationships". There will be several custom maped objects (that SalesForce created for your developer account) but for this demo there will be two more added here.	Click on the "New" button and from the next screen where you need to select the DataType, chose "Text". Then click "Next" and provide the details about the custom field you are creating. Under "Field Label" enter **Last Edited By** and under "Field Name" enter **LastEditedBy**. Add 100 as field length and click "Save". 

Repeat this process one more time only this time in the first step select "Date/Time", under "Field Label" enter **Last Edited On** and under "Field Name" enter **LastEditedOn**. 

The end result should be something like this:
![](http://i.imgur.com/QSEgZFx.png)
The "API Name" information is what we worry about. It should match our model properties because if they are different the JSON serializer will not be able to map them.
More info about SalesForce object fields can be found here:

https://goo.gl/dNvNqT
http://goo.gl/QPKfW6
https://goo.gl/t9qh0b
https://goo.gl/dfkPr2

### Obtain your SalesForce credentials

To start working with the toolkit and make queries over SalesForce you will need to obtain your SalesForce credentials (which later on will be added into the Web.Config file of the solution). You will need the following:

	- Username
	- Password
	- Consumer Key
	- Consumer Secret
    - Security Token

Your username is the email address used for the SalesForce account you just created (same goes for the password). The Consumer Key and the Consumer Secret can be found when previewing the details of the app you just created (Setup -> Build -> Create -> Apps -> click on your app). 

The security token can be obtained from your account settings menu. 

![](http://i.imgur.com/GARlKY6.png)

Once you clicked on "Reset Security Token" you will be able to copy & paste it into the solution (you will also receive an email containing the security token).


### Setting up the solution

Now we can switch back to Visual Studio where we will configure the project for our use. First, we will need to setup the IIS binding for our solution. Instead of using http://localhost and some random port each time the project is started, I've set it up that way so I can access it easily via http://salesforce.api/. 

To configure this navigate to the IIS Manager and add new website named **salesforce.api** and point the physical path to the folder where you've downloaded the project. Next step would be to go into "Application Pools" and right click on the website you just created. Then go to "Advanced Settings" and from here under "Identity" make sure that you have selected "LocalSystem".

![](http://i.imgur.com/4lzOlCU.png)

Finally, go into your hosts file (Windows\System32\drivers\etc) and add the following line:
 - **127.0.0.1 salesforce.api**

**NOTE:** If you are unable to configure the solution to work with Local IIS you might experience some problems loading the project (see image below)
![](http://i.imgur.com/qdm6PSX.png)

If this has happened then do the following:
- Right click on the project that has the "Load Failed" message and click on  "Edit SalesForceIntegration.csproj"
![](http://i.imgur.com/m1kNBN4.png)

- Scroll down in this document untill you find the IISUrl config. Remove these three lines from the .csproj file and then save it (see image below)
![](http://i.imgur.com/G4TpYEO.png)

- Again right click on the project that is unable to load and click on "Reload Project". The project should be loaded now.
![](http://i.imgur.com/LmIM5f7.png)


### The solution in action

If everything is completed then go ahead and build the solution. If there are no build errors we can go in and open the application in a browser and start working with the SalesForce data. Don't forget to add your SalesForce credentials in your web config file (from obvious reasons mine are removed from the image below):

![](http://i.imgur.com/hLjrjui.png)

_**THIS IS VERY IMPORTANT OTHERWISE YOUR APPLICATION WON'T WORK BECAUSE NO AUTHENTICATION WILL BE ESTABLISHED WITH SALESFORCE.**_

Go ahead and start a new instance of the project. The default routing is setup that way so it starts with the Leads controller and the Index method. This will retrieve all of the Leads objects from the SalesForce account you connected. 
The screen you will get will probably be something like this:

![](http://i.imgur.com/TwZb4Pf.png)

This means that we have successfuly connected with our SalesForce application, queried over the Leads objects and received them into our web application.

_Some screenshots showing the application working:_

![](http://i.imgur.com/gIO8rdK.png)
![](http://i.imgur.com/8MoqF14.png)
![](http://i.imgur.com/qIKI3Be.png)
![](http://i.imgur.com/O294zKc.png)
![](http://i.imgur.com/LTECnCq.png)
![](http://i.imgur.com/6QsKCuT.png)
![](http://i.imgur.com/3gE4Am6.png)

---------
## Contribution

- If you want to add your contribution to the project then go ahead and send me a pull request. I will be more than happy to cooperate with anyone out there that would be interested in improving this project (or maybe create new and more advanced SalesForce web solution).
