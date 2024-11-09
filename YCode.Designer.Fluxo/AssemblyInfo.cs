using System.Windows.Markup;

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
												//(used if a resource is not found in the page,
												// or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
												//(used if a resource is not found in the page,
												// app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("http://www.ycode.dev.com/coding", "YCode")]
[assembly: XmlnsDefinition("http://www.ycode.dev.com/coding/fluxo", "YCode.Designer.Fluxo")]