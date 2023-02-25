
using Stride.Core.Mathematics;

namespace Stride.Engine;

public static class CustomEntityExtensions
{
	public static Vector3 WorldPosition(this Entity entity)
	{
		return entity.Transform.WorldMatrix.TranslationVector;
	}
}