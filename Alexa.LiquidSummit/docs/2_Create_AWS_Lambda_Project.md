# Section Index
1. [Creating the Liquid Summit Website](1_Setup_Liquid_Content.md)
2. [Creating the AWS Lambda Project](2_Create_AWS_Lambda_Project.md)

   1. [Building the Basic Alexa Framework](2-1_Create_Basic_Framework.md)
   2. [Building the Application Logic for Alexa](2-2_Create_Application_Logic.md)
   3. [Using the Liquid Content API](2-3_Use_Liquid_Content_API.md)
   4. [Publishing to AWS](2-4_Publishing_Lambda.md)
   5. [Testing the Troubleshooting](2-5_Testing_Lambda_Function.md)

3. [Configuring the Alexa Skill](3_Configure_Alexa_Skill.md)

# Creating the Application Code for an Alexa Skill

The Amazon Alexa engine does a great job of understanding spoken language and mapping that into an intent defined by your Skill. What Alexa cannot do though, is determine what should happen as a result of that intent being triggered. That is where your application code comes in. Alexa is very flexible and you can host your application code on any publicly accessible server. For my demo skill, I chose to host my code on AWS Lambda. Since Lambda and Alexa are both Amazon services, they are able provide hooks to make it easier for them to talk to each other. Also, since I'll run code on AWS Lambda, I'll use AWS CloudWatch to handle my logging needs. 

The application logic is the heart of any Alexa Skill. In this section I'll show you how to create a new AWS Lambda function in C# using the AWS Toolkit for Visual Studio.

**Previous:** [Creating the Liquid Summit Website](1_Setup_Liquid_Content.md)

**Next:** [Building the Basic Alexa Framework](2-1_Create_Basic_Framework.md)