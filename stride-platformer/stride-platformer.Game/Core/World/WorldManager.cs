using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Engine;

namespace StridePlatformer.World;

public class WorldManager : StartupScript
{

	public UrlReference<Scene> StarterSceneToLoad { get; set; }

	public override void Start()
	{
		base.Start();

		var childScene = Content.Load(StarterSceneToLoad);
		childScene.Offset = new Vector3(0,-25,0);
		//childScene.Parent = Entity.Scene;

		SceneSystem.SceneInstance.RootScene.Children.Add(childScene);
	}
}