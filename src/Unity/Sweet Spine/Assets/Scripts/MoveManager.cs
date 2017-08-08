using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct Move {
	[XmlAttribute("name")]
	public string name;
	public SerializableVector3 lowerRange;
	public SerializableVector3 upperRange;

	public Move(string name, SerializableVector3 lowerRange, SerializableVector3 upperRange)
	{
		this.name = name;
		this.lowerRange = lowerRange;
		this.upperRange = upperRange;
	}
}

[System.Serializable]
[XmlRoot("MoveCollection")]
public class MoveList {
	[XmlArray("Moves")]
	[XmlArrayItem("Move")]
	public List<Move> moves = new List<Move>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer (typeof(MoveList));
		using (var stream = new FileStream (path, FileMode.Create)) {
			serializer.Serialize (stream, this);
		}
	}

	public static MoveList Load(string path)
	{
		var serializer = new XmlSerializer (typeof(MoveList));
		using (var stream = new FileStream (path, FileMode.Open)) {
			return serializer.Deserialize (stream) as MoveList;
		}
	}

	public static MoveList LoadFromText(string text)
	{
		var serializer = new XmlSerializer (typeof(MoveList));
		return serializer.Deserialize (new StringReader (text)) as MoveList;
	}
}

public class MoveManager : MonoBehaviour {
	private static MoveManager _instance = null;
	public MoveList moveList;

	public const string moveListPath = "Resources/Data/Moves/moves.xml";
	public const string defaultMoveListPath = "Resources/Data/Moves/default.xml";

	public static MoveManager Instance 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = new GameObject ("MoveManager").AddComponent<MoveManager>();
				_instance.Load (Path.Combine(Application.dataPath, moveListPath));
			}
			return _instance;
		}
	}

	public void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.gameObject);
		else {
			DontDestroyOnLoad (this.gameObject);
			_instance = this;
			if (moveList == null || moveList.moves == null || moveList.moves.Count == 0)
				Load (Path.Combine(Application.dataPath, moveListPath));
		}
	}

	public void Save(string path)
	{
		moveList.Save (path);
	}

	public void RestaureDefault()
	{
		Load (Path.Combine (Application.dataPath, defaultMoveListPath));
	}

	public MoveList Load(string path)
	{
		moveList = MoveList.Load (path);
		if (moveList == null)
			moveList = new MoveList ();
		return moveList;
	}
}
