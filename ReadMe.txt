This solution was created to try and get to grips with the basics of Identity Server.
The idea was to create a set of projects that demonstrates how Identity Server could
be used to secure the various elements of an enterprise solution.

The different projects were created in conjunction with reading the Identity Server
documentation found at: http://docs.identityserver.io/en/release/index.html

DotNet Version: 2.1.2
Node Version: v6.11.1
NPM Version: 3.10.10

Running the Solution
--------------------
The solution is set to run IDServer, APIClient and WebClient on start up.
When the browser is displayed navigate to http://localhost:5001/Home/Contact
this action requires authorization so the login page will be displayed for the user to login,
the first time you will need to register but once logged in the contact details will be displayed.
Then navigate to http://localhost:5001/Home/CallAPI and the user details will be displayed.

ConsoleAPITest
Run the ConsoleAPITest project while the IDServer and APIClient projects are running.



IDServer
--------
URL: http://localhost:5000
The IDServer project is a basic Authentication server to be used by the other projects.
The server uses the ASP.NET identity model and entity framework core.

APIClient
---------
URL: http://localhost:5002
The APIClient project is a simple Asp.net core application that contains a web API controller
'IdentityController' that has a single action 'Get' to return current user details.

WebClient
---------
URL: http://localhost:5001
The WebClient project is a simple Asp.net core mvc web application that demonstrates the use
of the ID server both for authorization within the application and also when calling an API.
The HomeController contains an action 'Contact' that has the 'Authorize' attribute, and a
'CallAPI' action that calls an API method within the 'APIClient' project.

ConsoleAPITest
--------------
The ConsoleAPITest project is a simple console application that demonstrates how to call a
web API action by first getting an access token from the ID Server and then using that token
to call the 'Get' action on the 'IdentityController' of the 'APIClient' project.
