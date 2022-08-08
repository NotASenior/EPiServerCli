# Opti CLI
A Command Line Interface tool to **scaffold pages and blocks** for EPiServer/Optimizely CMS (And **more stuff soon!**)

## How to use it?
You can use the tool easily in an **easy-to-remember** format:

`opti generate page TestPage`

You can even use it **without typing the whole command**:

`opti g page TestPage`
`opti ge page TestPage`
`opti gen page TestPage`

This command will **autocomplete** to this:

`opti generate page TestPage`

You can also **skip the type of content** to create, **or the suffix**:

`opti g p Test`

These commands will **autocomplete** to this:

`opti generate page TestPage`

When executed, this command will generate the page **class, controller and view!** 
(And a **view model** if you use a **flag**. **Isn't this great!?**)

## Do we have flags to customize the output?
This section will be written soon!

## How to install it?
dotnet tool install --global Opti.Cli.Client

## How to update it?
dotnet tool update --global Opti.Cli.Client

## How to uninstall it?
dotnet tool uninstall --global Opti.Cli.Client
