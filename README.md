# animals core and runtime for aiventure-david

This code is a first-draft implementation of United States Utility Patent Application US20180204107A1 dated 14 March 2018. The copyright of this software remains the property of The Cartheur Company but can be used in open-source projects if the license is kept and any modifications to the code are shared with the patent holder. All communications are managed through the email address: cartheur@pm.me

Here is a version of the core and runtime for a david bipedal robot. This code is currently _below_ baseline so it should not be used on the current version of david.

## Features that can be explored

One of the most interesting features of this version of the code is the ability to query the artificial mind. A test file in Core.UnitTests named ConversationalAeonTests.cs gives this idea.

Another interesting feature is the usage of Lua-Torch rather than Py-Torch to leverage open-source machine leanring libaries. It is accomplishe by an attribute in C# where use of the scripts is more transparent and intuitive.
