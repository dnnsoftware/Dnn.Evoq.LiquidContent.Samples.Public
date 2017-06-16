# Liquid Content Mobile
Manage your site's Content Items through your mobile phone.

![Liquid Content Mobile](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Liquid%20Content%20Mobile.png)

## Requirements

* Latest Evoq site setup.
* Visual Studio 2015/2017 with Xamarin tools

    ![Visual Studio Requirements](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Requirements_VisualStudio.png)

## Steps

First we create our Xamarin Forms Project:

* File.. New.. Project.
    * From the Templates, we select Visual C# and then Cross-Platform.
    * The project type will be Cross Platform App ( Xamarin.Forms or Native )
    * Then we give our project a name

    ![Visual Studio Project](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Requirements_VisualStudioProject.png)


    * After that, we can select the UI Technology and Code Sharing. For this case, we're going to select Xamarin.Forms and Portable Class Libray.
    * For information between code sharing technologies go to this link.
    * We can also have the ability to include integration with Azure.

    ![Visual Studio Project](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Requirements_VisualStudioProject1.png)

    * We then select the project targets for Universal Windows applications:

    ![Visual Studio Project](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Requirements_VisualStudioProject2.png)

    * After we're done with these settings, we'll end up with several projects in our solution structured like so:

    ![Visual Studio Project](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Requirements_VisualStudioSolution.png)

    * Portable Class Library:
        * This is where all the code and UI will be built to provide a truly cross-platform solution.

    * Android Project
    * IOS Project
    * Universal Windows
        * In these projects we will have the version created for each device and we could also have specific changes for each platform. For example, device platform specific assets like icons, images, etc.

## Solution description

![Visual Studio Project](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Eickhel/Documentation/images/Solution1.png)

The specifics of this solution:

   * Models:
     * Here we have a set of classes representing the JSON structure for the ContenItems and ContentTypes. These classes have been created by using tools for converting JSON structures to C# classes: [JSON Utils](https://jsonutils.com/)
   * Services:
     * In here we have the logic for maintaining the data structures of our applications. Each Data Store has the methods for requesting data, as well as CRUD operation for each specific type.
   * ViewModels:
     * This view models serve as the link between the views and the data models.
   * Views:
     * This is where we build the unified UI for all platform. In this case, these are all the views that are going to be displayed in our application.
   * App.xaml:
     * The code behind in this file is the starting point of our Application. It prepares all the views to be displayed.