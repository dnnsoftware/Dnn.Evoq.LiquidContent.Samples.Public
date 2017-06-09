# Section Index
1. [Creating the Liquid Summit Website](docs/1_Setup_Liquid_Content.md)
2. [Creating the AWS Lambda Project](docs/2_Create_AWS_Lambda_Project.md)
3. [Configuring the Alexa Skill](docs/3_Configure_Alexa_Skill.md)

# Creating the Application Code for an Alexa Skill

The Amazon Alexa engine does a great job of understanding spoken language and mapping that into an intent defined by your Skill. What Alexa cannot do though, is determine what should happen as a result of that intent being triggered. That is where your application code comes in. Alexa is very flexible and you can host your application code on any publicly accessible server. For my demo skill, I chose to host my code on AWS Lambda. Since Lambda and Alexa are both Amazon services, they are able provide a few little hooks to make it easier for them to talk to each other. Also, since I'll be running code on AWS Lambda, I'll use AWS CloudWatch to handle my logging needs. 

The application logic is the heart of any Alexa Skill. In this section I'll show you how to create a new AWS Lambda function in C# using the AWS Toolkit for Visual Studio.

