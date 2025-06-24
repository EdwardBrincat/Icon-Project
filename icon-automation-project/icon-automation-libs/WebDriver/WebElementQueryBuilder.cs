using Icon_Automation_Libs.Extensions;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace Icon_Automation_Libs.WebDriver;

public class WebElementQueryBuilder<TComponent> : WebElementQueryBuilder where TComponent : IComponent
{
	private readonly IServiceProvider _serviceProvider;

	public WebElementQueryBuilder(WebElementQueryBuilder parent = null) : base(parent)
	{
	}

	public WebElementQueryBuilder(
		IServiceProvider serviceProvider,
		WebElementQueryBuilder parent = null
	) : base(parent)
	{
		_serviceProvider = serviceProvider;
	}

	public TComponent AsComponent() => ActivatorUtilities.CreateInstance<TComponent>(_serviceProvider, this);

	public T CreateChild<T>() where T : WebElementQueryBuilder, new()
	{
		Query = Parent?.Query + Query;
		return ActivatorUtilities.CreateInstance<T>(_serviceProvider, this);
	}
}

public class WebElementQueryBuilder
{
	public WebElementQueryBuilder(WebElementQueryBuilder parent = null)
	{
		Parent = parent;
	}

	public WebElementQueryBuilder Parent { get; }

	public string Query { get; set; }

	public WebElementQueryBuilder WithCss(string query)
	{
		Query += query;
		return this;
	}

	public WebElementQueryBuilder WithCssClass(string @class) => WithCss($" [class=\"{@class}\"]");

	public WebElementQueryBuilder WithAttributeEqual(string attribute, string value)
		=> WithAttribute(attribute, value, "=");

	public WebElementQueryBuilder WithAttributeContains(string attribute, string value)
		=> WithAttribute(attribute, value, "*=");

	public WebElementQueryBuilder WithChildAttributeEqual(string attribute, string value)
		=> WithChildAttribute(attribute, value, "=");

	public WebElementQueryBuilder WithChildAttributeContains(string attribute, string value)
		=> WithChildAttribute(attribute, value, "*=");

	public WebElementQueryBuilder WithAttribute(string attribute, string value, string @operator)
	{
		if (!Query.IsNullOrEmpty())
			Query = Query.TrimEnd();

		Query += $"[{attribute}{@operator}\"{value}\"]";
		return this;
	}

	public WebElementQueryBuilder WithAttribute(string attribute)
	{
		Query += $"[{attribute}]";
		return this;
	}

	public WebElementQueryBuilder WithChildAttribute(string attribute, string value, string @operator)
	{
		if (!Query.IsNullOrEmpty())
			Query = Query.TrimEnd();

		Query += $" [{attribute}{@operator}\"{value}\"]";
		return this;
	}	

	public WebElementQueryBuilder WithElement(string element)
	{
		Query += $" {element}";
		return this;
	}
	
	public WebElementQueryBuilder Reset()
	{
		Query = string.Empty;
		return this;
	}

	public By Build() => By.CssSelector(Parent?.Query + Query);

	public WebElementQueryBuilder CreateChild() => new(this);	

}