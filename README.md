# AutoRoute
AutoRoute is a .NET/C# library for building reliable UI automation workflows when an application or service does not expose an API.

Instead of interacting with APIs, AutoRoute allows developers to describe a sequence of actions in code. The library executes these actions using the most appropriate automation backend for the target platform.

Why?

Many desktop and mobile applications expose their functionality only through a graphical interface.

Examples include:

purchasing digital goods;
filling out forms;
configuring applications;
automating repetitive tasks;
interacting with legacy software;
operating services that do not provide a public API.

AutoRoute allows developers to automate such workflows without writing platform-specific automation code.

Philosophy

Developers should focus on what needs to happen, not how it happens.

Instead of dealing with clicks, image recognition, accessibility APIs, ADB commands, or browser automation directly, developers define a high-level route.

Example:

route
    .Wait("Buy")
    .Click("Buy")
    .Wait("Card Number")
    .Type(card.Number)
    .Wait("Pay")
    .Click("Pay");
