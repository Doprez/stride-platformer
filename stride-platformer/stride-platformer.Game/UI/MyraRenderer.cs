using Myra;
using Myra.Graphics2D.UI;
using Stride.Engine;
using Stride.Games;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;
using UI.Views;

namespace UI;
public class MyraRenderer : SceneRendererBase
{

	private MainView _mainView;
	private Desktop _desktop;

	protected override void InitializeCore()
	{
		base.InitializeCore();

		MyraEnvironment.Game = (Game)Services.GetService<IGame>();

		_mainView = new MainView();
		Services.AddService(_mainView);

		_desktop = new()
		{
			Root = _mainView,
		};
	}

	protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
	{
		// Clear depth buffer
		drawContext.CommandList.Clear(GraphicsDevice.Presenter.DepthStencilBuffer, DepthStencilClearOptions.DepthBuffer);

		// Render UI
		_desktop.Render();
	}
}
