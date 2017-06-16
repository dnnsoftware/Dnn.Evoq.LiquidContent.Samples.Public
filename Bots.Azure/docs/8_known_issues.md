# Content index
1. [Setting up the recipes](1_setup_recipes.md)
2. [Creating the basic bot](2_creating_basic_bot.md)
3. [Setup continuous integration](3_setup_ci.md)
4. [Debugging the bot on your local environment](4_debugging_locally.md)
5. [Customizing the basic bot](5_customizing_bot.md)
6. [Test your recipes bot](6_testing_bot.md)
7. [Adding a webchat in your site](7_adding_webchat.md)
8. [Known issues](8_known_issues.md)

# Known issues

**Inheritance security rules violated by type: &#39;System.Net.Http.WebRequestHandler&#39;. Derived types must either match the security accessibility of the base type or be less accessible**

Edit the file &quot;%USERPROFILE%\AppData\Roaming\npm\node\_modules\azure-functions-cli\bin\Func.exe.config&quot; and change the binding redirect as follows. More information on [https://github.com/dotnet/corefx/issues/16805](https://github.com/dotnet/corefx/issues/16805)

```
<dependentAssembly>
    <assemblyIdentity name="System.Net.Http" culture="neutral" publicKeyToken="b03f5f7f11d50a3a" />
    <!--Explicitly redirecting back to 4.0.0. See https://github.com/dotnet/corefx/issues/16805 for details-->
    <bindingRedirect oldVersion="0.0.0.0-4.1.1.0" newVersion="4.0.0.0" />
</dependentAssembly>
```