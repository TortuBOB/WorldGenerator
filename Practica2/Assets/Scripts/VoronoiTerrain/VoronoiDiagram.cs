using UnityEngine;

public class VoronoiDiagram : MonoBehaviour
{
	public Vector2Int mapSize;
	public int regionAmount;

	Texture2D GetDiagram()
	{
		Vector2Int[] centroids = new Vector2Int[regionAmount];
		Color[] regions = new Color[regionAmount];
		for(int i = 0; i < regionAmount; i++)
		{
			centroids[i] = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
			regions[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		}
		Color[] pixelColors = new Color[mapSize.x * mapSize.y];
		for(int x = 0; x < mapSize.x; x++)
		{
			for(int y = 0; y < mapSize.y; y++)
			{
				int index = x * mapSize.x + y;
				pixelColors[index] = regions[GetClosestCentroidIndex(new Vector2Int(x, y), centroids)];
			}
		}
		return GetImageFromColorArray(pixelColors);
	}

	int GetClosestCentroidIndex(Vector2Int pixelPos, Vector2Int[] centroids)
	{
		float smallestDst = float.MaxValue;
		int index = 0;
		for(int i = 0; i < centroids.Length; i++)
		{
			if (Vector2.Distance(pixelPos, centroids[i]) < smallestDst)
			{
				smallestDst = Vector2.Distance(pixelPos, centroids[i]);
				index = i;
			}
		}
		return index;
	}

	Texture2D GetImageFromColorArray(Color[] pixelColors)
	{
		Texture2D tex = new Texture2D(mapSize.x, mapSize.y);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(pixelColors);
		tex.Apply();
		return tex;
	}
}
