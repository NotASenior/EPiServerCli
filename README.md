# Opti CLI
A Command Line Interface tool to **scaffold pages and blocks** for EPiServer/Optimizely CMS (And **more stuff soon!**)

## How to use it?
You can use the tool easily in an **easy-to-remember** format:

`opti generate page TestPage`

You can even use it **without typing the whole command**:

`opti g page TestPage`
<br>
`opti ge page TestPage`
<br>
`opti gen page TestPage`

This command will **autocomplete** to this:

`opti generate page TestPage`

You can also **skip the type of content** to create, **or the suffix**:

`opti g p Test`

These commands will **autocomplete** to this:

`opti generate page TestPage`

When executed, this command will generate the page **class, controller and view!** 
(And a **view model** if you use a **flag**. **Isn't this great!?**)

Generated files:
- Models/Pages/TestPage.cs
- Controllers/TestPageController.cs
- Views/TestPage/Index.cshtml

Or you can use the **feature** approach (Not supported yet):

`opti generate page TestPage --feature`

This will generate the files like this:
- Features/TestPage/TestPage.cs
- Features/TestPage/TestPageController.cs
- Features/TestPage/Index.cshtml

## Take a look at the commands!
<table>
	<thead>
		<tr>
			<th>Command</th>
			<th>Content Type</th>
			<th>Name</th>
			<th>Flags</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td rowspan="4">opti generate</td>
			<td>page</td>
			<td>TestPage</td>
			<td rowspan="4">
				Take a look at the <a href="#do-we-have-flags-to-customize-the-output">flags</a> section
			</td>
		</tr>
		<tr>
			<td>block</td>
			<td>TestBlock</td>
		</tr>
		<tr>
			<td>selection-factory</td>
			<td>TestSelectionFactory</td>
		</tr>
		<tr>
			<td>initializable-module</td>
			<td>TestInitialization</td>
		</tr>
	</tbody>
</table>

## Do we have flags to customize the output?
This section will be written soon!

## How to install it?
dotnet tool install --global Opti.Cli.Client

## How to update it?
dotnet tool update --global Opti.Cli.Client

## How to uninstall it?
dotnet tool uninstall --global Opti.Cli.Client

## What are we planning to add next?
- Categories
- Path personalization
- Flags to customize the content generation
- Feature architecture approach to generate the files in the same folder
- Social API demo
