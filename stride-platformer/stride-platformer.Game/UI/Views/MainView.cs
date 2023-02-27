using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace UI.Views;
public class MainView : Panel
{
	public HorizontalProgressBar HealthBar;

	public MainView()
	{
		HealthBar = new()
		{
			HorizontalAlignment = HorizontalAlignment.Left,
			VerticalAlignment = VerticalAlignment.Bottom,
			Value = 100,
			Filler = new SolidBrush("#4BD961FF"),
			Left = 20,
			Top = -20,
			Width = 300,
			Height = 20,
			Background = new SolidBrush("#202020FF"),
		};

		Widgets.Add(HealthBar);
	}
}
