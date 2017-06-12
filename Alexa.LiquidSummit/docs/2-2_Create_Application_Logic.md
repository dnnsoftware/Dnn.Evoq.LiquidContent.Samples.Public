# Section Index
1. [Creating the Liquid Summit Website](1_Setup_Liquid_Content.md)
2. [Creating the AWS Lambda Project](2_Create_AWS_Lambda_Project.md)

   1. [Building the Basic Alexa Framework](2-1_Create_Basic_Framework.md)
   2. [Building the Application Logic for Alexa](2-2_Create_Application_Logic.md)
   3. [Using the Liquid Content API](2-3_Use_Liquid_Content_API.md)
   4. [Publishing to AWS](2-4_Publishing_Lambda.md)
   5. [Testing and Troubleshooting](2-5_Testing_Lambda_Function.md)

3. [Configuring the Alexa Skill](3_Configure_Alexa_Skill.md)

# Building the Application Logic for Alexa

Earlier we created generic methods for handling the three (3) basic request types. Now it is time to start filling them in with real logic. Let's start with the easy parts first.

## Handling Launch Requests

When your skill is invoked without specifying an intent, Alexa will send a `LaunchRequest` event to your service.  For skills that just have a single intent you can use this event to perform the action and return an appropriate response.  For skills with multiple intent types, you should return some sort of a prompt that helps guide users on how to use your skill. Let's create a basic prompt for helping the user with Liquid Summit.

```C#
public SkillResponse HandleLaunchRequest(SkillRequest input)
{
    // Log the method type for debugging purposes
    Context.Logger.LogLine("Calling HandleLaunchRequest");

    return ResponseBuilder.Tell(
        new PlainTextOutputSpeech()
        {
            Text = "Welcome to the Liquid Summit. I am here to help you find your way around the conference."
        }
    );

}
```

One of the first things I do in my method is to create an entry in the event log. Since my code is going to run on AWS, and I don't have an easy way to attach a debugger, I am fairly liberal with my logging code. All of the logging output will automatically get stored in AWS CloudWatch. The Context object was previously stored in a local property in our main `FunctionHandler`.

After logging some basic context infomation, I use the Alexa.NET helper `ResponseBuilder` to create a simple response and return. Since my text is pretty simple, I just use the `PlainTextOutputSpeech` object to create my response. For more complex responses where I care about exactly how my response will be spoken, I can use [SSML](https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/speech-synthesis-markup-language-ssml-reference) and the `SsmlOutputSpeech` object.

## Handling SessionEnded Requests
The application service will receive a SessionEnded request whenever a session is unexpectedly ended. This can happen for one of three  reasons:

1. The user says "exit".
2. The user does not respond, or says something that doesn't match an intent, while the device is listening for a response.
3. An error occurs.

The current session is also ended when the service sets the `shouldSessionEnd` flag in a response. In this case, the session is explicitly ended and no SessionEnded event will be triggered.

```C#
public SkillResponse HandleSessionEndRequest(SkillRequest input)
{
    // Log the method type for debugging purposes
    Context.Logger.LogLine("Calling HandleSessionEndRequest");

    return ResponseBuilder.Empty();

}
```  

The return value from a SessionEnded event is not used by Alexa so it is acceptable to just return an empty SkillResponse. Also, for a production system, you should add some additional error handling and logging so that you can easily determine why the session was ended.

## Handling Intents

Now we are finally at the heart of building a service for an Alexa Skill. Every skill will have one or more intents defined and the `IntentRequest` event is used to tell your service which intent was requested by the user.

The logic for handling a single intent can be pretty complex so you will want to have a separate method to handle each intent type that you plan to have in your service.

You can determine what intent the user has triggered by looking at the request object in the incoming input object. Since the input object is of type SkillRequest, you'll first need to typecast it to an IntentRequest type so that you can access the Intent property.

```C#
public SkillResponse HandleIntentRequest(SkillRequest input)
{
    // Log the method type for debugging purposes
    Context.Logger.LogLine("Calling HandleIntentRequest");
    var request = input.Request as IntentRequest;
    return handlers[request.Intent.Name](input);
}

```

I have also added some new entries to the handlers dictionary in the constructor to account for the two intents we are defining for our service.

```C#
handlers = new Dictionary<string, Func<SkillRequest, ResponseBody>>() {
    { "LaunchRequest", HandleLaunchRequest },
    { "IntentRequest", HandleIntentRequest },
    { "SessionEndedRequest", HandleSessionEndRequest },

    { "SendDirections", HandleSendDirectionsIntent },
    { "GetSpeaker", HandleGetSpeakerIntent }
};
```

The `GetSpeaker` intent is fairly straightforward. There are effectively four steps that I use for most intents:

1. Define a default set of responses
2. Get some data from an external service
3. If we have data, update my response
4. Return my ResponseBody

```C#
public SkillResponse HandleGetSpeakerIntent(SkillRequest input)
{
    // Log the method type for debugging purposes
    Context.Logger.LogLine("Calling GetSpeaker Intent");

    // Step 1 - Set some defaults
    string title = "Keynote Speaker";
    string speech = "I'm sorry. The selection committee is still working to identify a keynote speaker.";

    // Step 2 - Get some data
    var response = GetKeynoteSpeakerAsync().Result;

    // Step 3 - Update the response
    if (response != null)
    {
        speech = $"We are pleased to announce that {response.firstName} {response.lastName}, ";
        speech += $"{ response.title} will be the keynote speaker at the Liquid Summit conference. ";
        speech += $"{response.shortBiography}";
    }

    //Step 4 - Return 
    return ResponseBuilder.TellWithCard(
        new PlainTextOutputSpeech() { Text = speech },
        title,
        speech            
    );
}
```

The `GetKeynoteSpeakerAsync` method call on Step 2 will make a call to Evoq Liquid Content to get some data.  We'll look at the details in the next segment. 

The `HandleSendDirectionsIntent` is a little more complex. When creating the response we not only have to account for the text that will get read to the user, we also need to define how we want the response to look in the Alexa application.  The `OutPutSpeech` property is the portion of the response that Alexa actually says. The `Card` property defines how your response will be displayed.

When sending directions we want to show the user a map so they can easily see how to get to the conference. Below is an example from the SendDirections intent handler to show how we can include an image in our response. Unfortunately, the ResponseBuilder helper doesn't handle image cards in responses so we have to build out the full SkillResponse object ourselves.

```C#
return new SkillResponse()
{
    Version = "1.0",
    Response = new ResponseBody()
    {
        Card = new StandardCard()
        {
            Title = $"{title}",
            Content = $"{directions.details.directions}",
            Image = new CardImage
            {
                SmallImageUrl = directions.details.mapImage.First().url,
                LargeImageUrl = directions.details.mapImage.First().url
            }
        },
        OutputSpeech = new PlainTextOutputSpeech()
        {
            Text = speech
        },
        ShouldEndSession = true
    }
};
```

As you can see, building the intent logic is pretty straight forward. At this point our code can handle two simple intents.  In the next section I'll walk through the code to call the Evoq Liquid Content API.

**Previous:** [Building the Basic Alexa Framework](2-1_Create_Basic_Framework.md)

**Next:** [Using the Liquid Content API](2-3_Use_Liquid_Content_API.md)