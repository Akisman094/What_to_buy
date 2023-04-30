# WhatToBuy

The application stores your grocery lists. To access them you will need to log in to your account, after that you will be able to access your family’s stored lists. There you can add items or remove them from grocery list. You can send shopping list to the email of choice.


## Setup
You can launch it using docker-compose or even without it
### Docker
To setup this project go to docker-compose project and find file named env.api. There you will find: 

```code
EMAILSENDER__SMTPSERVERURL=
EMAILSENDER__SMTPPORT=
EMAILSENDER__EMAILADDRESS=
EMAILSENDER__EMAILPASSWORD=
```
There you will have to put your SmtpServer settings. I used [ethereal](https://ethereal.email/) for these purposes.  
Once that is done, you are good to go.

### Launching without Docker
If you are willing to use the application without docker, then head to Systems -> Api -> WhatToBuy.Api -> appsettings.json. There correct already familiar lines:
```code
"EmailSender": {
    "SmtpServerUrl": "",
    "SmtpPort": "",
    "EmailAddress": "",
    "EmailPassword": ""
  }
```
After that you will have to insert you own MsSql connection string instead of what you see here:  
```code
"Database": {
    "ConnectionString": "Server=localhost;Database=WhatToBuy;User Id=sa;Password=SomePassw0rd;Trusted_Connection=True;Encrypt=false;Integrated security=false;"
  },
```  
Then copy this section and head to Systems -> Identity -> WhatToBuy.Identity -> appsettings.json and replace the same section with what you just copied.  
To access Api you need IdentityServer to issue you a JWT Token. To launch them both simultaneously right-click on the Solution and go to properties. There in "Startup Project" set "Multipple Startup Proojects" and put actions infront of WhatToBuy.Api and WhatToBuy.Identity to Start.  
After that you are free to launch the project

## Accessing Admin
Application automatically seeds Admin user into database. Here are admin credentials:
```code
UserName: "Admin"
Password: "CoolAdmin"
```
